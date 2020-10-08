using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace EL2311_Assignment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set the combo boxes to the first index for initialisation
            cbVehicleType.SelectedIndex = 0;
            cbAttribute.SelectedIndex = 0;

            //This ensures the DB is clear each time the program is run
            string temp1 = "DELETE FROM Vehicle";
            string response1 = dbAccess.sqlChange(temp1);

            string temp2 = "DELETE FROM Petrol";
            string response2 = dbAccess.sqlChange(temp2);

            string temp3 = "DELETE FROM Electric";
            string response3 = dbAccess.sqlChange(temp3);
        }

        bool isComplete = false;    // This bool will be used to tell the program that all the XML has been added to the database

        // This class handles handle the data. It will parse the xml and put the inner text into strings to be used for queries.
        // It also has methods to find the Average Speed and Top 5 Vehicles in a selected category.
        public class DataHandler
        {
            // This method will be used to obtain the information from within the element tags of the XML. The strings are passed to it by reference
            // for alteration from the AddToDB() method
            public void GetVehicleInformation(ref string engine, ref string vehicleID, ref string range, ref string weight,
                ref string carryCapacity, ref string topSpeed, ref string noise, ref string cylinders, ref string voltage, ref string brushed, XmlNode vehicle)
            {
                XmlElement vehicleElement = (XmlElement)vehicle;    // Create an XMLElement object from the XmlNode vehicle

                engine = vehicleElement["engine"].InnerText;    // Set the text of the engine string to the inner text of the engine element
                string engineCheck = engine.ToLower();      // Create a string that will be used in the if/else to check engine type.

                // If/else if/else to check what the engine type is, then find the relevant information from the elements
                if (engineCheck.Equals("petrol"))
                {
                    // These are the strings required for a petrol vehicle
                    vehicleID = vehicleElement["vehicleID"].InnerText;
                    range = vehicleElement["range"].InnerText;
                    weight = vehicleElement["weight"].InnerText;
                    carryCapacity = vehicleElement["carryCapacity"].InnerText;
                    topSpeed = vehicleElement["topSpeed"].InnerText;
                    noise = vehicleElement["noise"].InnerText;
                    cylinders = vehicleElement["cylinders"].InnerText;
                }
                else if (engineCheck.Equals("electric"))
                {
                    // These are the strings required for an electric vehicle
                    vehicleID = vehicleElement["vehicleID"].InnerText;
                    range = vehicleElement["range"].InnerText;
                    weight = vehicleElement["weight"].InnerText;
                    carryCapacity = vehicleElement["carryCapacity"].InnerText;
                    topSpeed = vehicleElement["topSpeed"].InnerText;
                    voltage = vehicleElement["voltage"].InnerText;
                    brushed = vehicleElement["brushed"].InnerText;
                }
                else
                {
                    // If the data is missing or incorrect in the engine tags, display a message box alerting the user of the issue (no data is added for that vehicle)
                    MessageBox.Show(("Incorrect XML data for engine type in " + vehicleElement["vehicleID"].InnerText + Environment.NewLine + "Data was not added"), "XML Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // This method will be used to get the data required to calculate the average speed of all vehicles of a specfied engine type
            public string AverageSpeed(string vehicle)
            {
                // If the user has selected all vehicles, the query should be run against the Vehicle table of the database to search all vehicles
                if (vehicle.Equals("All Vehicles"))
                {
                    string query = "SELECT AVG(topspeed) FROM Vehicle";     // SQL query using the AVG keyword to get the average of all vehicles in the Vehicle table
                    string response = dbAccess.sqlQuery(query);
                    return response;        // Return the response
                }
                else
                {
                    // If the user has specified either electric or petrol, the query must be run against the relevant table in the database
                    string query = "SELECT AVG(topspeed) FROM Vehicle WHERE engine = '" + vehicle + "'";        // Run the query on the table specified by the user
                    string response = dbAccess.sqlQuery(query);
                    return response;        //Return the response
                }
            }

            // This method will be used to get the top 5 vehicles of a chosen engine type and chosen attribute
            public string TopFive(string vehicle, string attr)
            {
                // If the user has selected all vehicles, the query is run on the Vehicle table
                if (vehicle.Equals("All Vehicles"))
                {
                    // The SQL query run on the Vehicle table gets the selected attribute and orders them in descending order. The LIMIT keyword will only return the first 5
                    string query = "SELECT " + attr + " , vehicleID FROM Vehicle ORDER BY " + attr + " desc LIMIT 5";
                    string response = dbAccess.sqlQuery(query);
                    return response;
                }
                else
                {
                    // If the user has selected either electric or petrol, the query is run against the specified table. This works in the same way as the previous query
                    string query = "SELECT " + attr + " , vehicleID FROM Vehicle WHERE engine = '" + vehicle + "' ORDER BY " + attr + " desc LIMIT 5";
                    string response = dbAccess.sqlQuery(query);
                    return response;
                }
            }
        }

        private void AddToDB()
        {
            isComplete = false;     // Set the isComplete bool to false when the method starts

            DataHandler dataHandler = new DataHandler();    // Create an instance of the DataHandler class
            XmlDocument doc = new XmlDocument();        // Create a new XmlDocument object

            try
            {
                doc.Load("vehicleInfo.xml");        // Load the XML document that has the information of the vehicles. This needs to be set manually
                
                // Create empty strings that will be used to store the information about the vehicles.
                string engine = "";
                string vehicleID = "";
                string range = "";
                string weight = "";
                string carryCapacity = "";
                string topSpeed = "";
                string noise = "";
                string cylinders = "";
                string voltage = "";
                string brushed = "";

                int progress = 0;       // Set progress to 0, this will be used to update the progress bar as the data loads

                XmlNodeList nodeList;       // Create a node list object. This will be used as a count for the loop
                XmlNode root = doc.DocumentElement;     // Get the root element of the loaded XML file

                nodeList = root.SelectNodes("vehicle");     // Select all the nodes named vehicle

                // An invoke is used here as the progress bar needs to be updated on the Main Thread. As it is a short action, there is very limited impact
                BeginInvoke(((Action)delegate ()
                {
                    pbLoadProgress.Maximum = nodeList.Count;        // Change the maximum value of the progress bar to the number of entries that will be added
                }));

                // A for loop that will run as many times as there is vehicle nodes in the XML file provided
                for (int i = 0; i < nodeList.Count; i++)
                {
                    // Use the GetVehicleInformation method of the DataHandler class to get the information from the XML elements
                    dataHandler.GetVehicleInformation(ref engine, ref vehicleID, ref range, ref weight, ref carryCapacity, ref topSpeed,
                        ref noise, ref cylinders, ref voltage, ref brushed, nodeList[i]);

                    string engineCheck = engine.ToLower();      // Convert the engine string to lower case for checking which tables to insert the data into

                    if (engineCheck.Equals("petrol"))
                    {
                        // If the engine type is petrol, the data is first loaded into the Vehicle table. First, the order of the attributes is chosen, then the values to be put in
                        string temp1 = "INSERT INTO Vehicle (vehicleID, engine, range, weight, carryCapacity, topspeed) VALUES ('" + vehicleID + "', '" + engine + "', '" + range +
                                "', '" + weight + "', '" + carryCapacity + "', '" + topSpeed + "')";

                        string response1 = dbAccess.sqlChange(temp1);   // Send the command to the database
                        string errorCheck = response1.ToLower();        // Convert the response message to lower case

                        if (errorCheck.StartsWith("constraint"))
                        {
                            // If the response starts with "constraint", there was a unique identifier error and the data will not be added
                            // This displays a message box letting the user know there was an error and which vehicle it was for
                            MessageBox.Show("An entry for " + vehicleID + " already exists and has not been added", "Unique Constraint Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        // This command will insert the relevant data into the petrol table of the database
                        string temp2 = "INSERT INTO Petrol (vehicleID, noise, cylinders) VALUES ('" + vehicleID + "', '" + noise + "', '" + cylinders + "')";

                        string response2 = dbAccess.sqlChange(temp2);

                        progress = i;   // Change progress to i for updating the progress bar
                        loadXML.ReportProgress(i);  // The background worker will report the progress to the GUI so the progress bar can update
                    }
                    else if (engineCheck.Equals("electric"))
                    {
                        // If the engine type is electric, the data is first loaded into the Vehicle table. First, the order of the attributes is chosen, then the values to be put in
                        string temp1 = "INSERT INTO Vehicle (vehicleID, engine, range, weight, carryCapacity, topspeed) VALUES ('" + vehicleID + "', '" + engine + "', '" + range +
                             "', '" + weight + "', '" + carryCapacity + "', '" + topSpeed + "')";

                        string response1 = dbAccess.sqlChange(temp1);   // Send the command to the database
                        string errorCheck = response1.ToLower();        // Cionvert the response to lower case

                        if (errorCheck.StartsWith("constraint"))
                        {
                            // If the response starts with "constraint", there was a unique identifier error and the data will not be added
                            // This displays a message box letting the user know there was an error and which vehicle it was for
                            MessageBox.Show("An entry for " + vehicleID + " already exists and has not been added", "Unique Constraint Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        // This command will insert the relevant data into the electric table of the database
                        string temp2 = "INSERT INTO Electric (vehicleID, voltage, brushed) VALUES ('" + vehicleID + "', '" + voltage + "', '" + brushed + "')";

                        string response2 = dbAccess.sqlChange(temp2);

                        progress = i;   // Change progress to i for updating the progress bar
                        loadXML.ReportProgress(i);   // The background worker will report the progress to the GUI so the progress bar can update
                    }

                    isComplete = true;  // Set is complete to true which will let the program know the XML was succesfully loaded, which will display a messagebox notifying the user                   
                }
            }
            // This will catch a file not found exception, which would arise if the name of the users XML document is incorrect
            catch (FileNotFoundException ex)
            {
                string message = ex.Message;    // Get the message of the exception which will let the user know the error
                MessageBox.Show(message, "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);     // Display the error in a message box
                isComplete = false; // Set is complete to false, so the program will NOT display that the XML was loaded
            }
            // This will catch any errors that are contained within the XML such as incorrect tags
            catch (XmlException ex)
            {
                string message = ex.Message;    // Get the message of the exception which will let the user know the error
                MessageBox.Show(message, "Badly Formed XML", MessageBoxButtons.OK, MessageBoxIcon.Error);   // Display the error in a message box
                isComplete = false;     // Set is complete to false, so the program will NOT display that the XML was loaded

                BeginInvoke(((Action)delegate ()
                {
                    btnAdd.Enabled = true;        // Use a delegate to re-enable the add button
                }));                
            }
        }

        // This method gets all the vehicle IDs in the database to populate the vehicles combo box
        private void GetVehicles()
        {
            string temp = "SELECT vehicleID FROM Vehicle";  // Create a SQL query that will get all the vehicle IDs in the database
            string response = dbAccess.sqlQuery(temp);      // Store the response in a string

            string[] vehicles = response.Split('\n');       // Split the response where there is a new line character and store them in an array

            // A foreach loop that will loop for as many vehicle IDs as there are in the array, then add each ID to the vehicle combo box
            foreach (string name in vehicles)
            {
                cbVehicles.Items.Add(name);
            }

            cbVehicles.SelectedIndex = 0;       // Set the selected index of the vehicles combo box to the first item
        }

        // This method will caclulate the runtime of a vehicle selected by the user
        public string GetRuntime(string vehicleName)
        {
            try
            {
                string vehicle = vehicleName.Replace("\t\r", String.Empty);     // Remove the tab and newline character from the string (combo box adds "\t\n"
                // Set up two floats for the calculation and an empty string to store the runtime in
                float speed;
                float range;
                string runtime = "";

                // Query the database to get the topspeed of the seected vehicle
                string query1 = "SELECT topspeed FROM Vehicle WHERE vehicleID = '" + vehicle + "'";
                string response1 = dbAccess.sqlQuery(query1);

                speed = float.Parse(response1);     // Convert the response from a string into a float to be used in the calculation

                // Query the database to get the range of the selected vehicle
                string query2 = "SELECT range FROM Vehicle WHERE vehicleID = '" + vehicle + "'";
                string response2 = dbAccess.sqlQuery(query2);

                range = float.Parse(response2);     // Convert the response from a string into a float to be used in the calculation

                runtime = (range / speed).ToString();   // Calculate the runtime and convert the result into a string

                return runtime;     // Return the runtime string for display
            }
            // This will catch a format exception, as if the strings supplied for conversion are not in the correct format an exception will be thrown
            catch (FormatException ex)
            {
                // Display a message box informing the user that the supplied strings could not be used
                MessageBox.Show("The vehicle name supplied was invalid", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;    // Do not return a result
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;     // Disable the add button so it can't be clicked twice
            loadXML.RunWorkerAsync();   // When the user clicks the Add XML to Database button, the loadXML background worker is called
        }

        private void loadXML_DoWork(object sender, DoWorkEventArgs e)
        {
            AddToDB();  // The AddToDB() method is run by the background worker so that the GUI remains responsive during loading
        }

        private void loadXML_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbLoadProgress.Value = e.ProgressPercentage;    // The background worker will report the progress of loading to the progress bar
        }

        private void loadXML_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Once the background worker has finished loading the XML, the GetVehicles() method is called to populate the vehicle combo box
            // and the progress bar is returned to 0;
            GetVehicles();
            pbLoadProgress.Value = 0;            

            // If the loading was successful, the isComplete boolean will be set to true and the user will be notified that the load is complete
            if (isComplete)
            {
                MessageBox.Show("XML successfully added to the DB!");
                btnAdd.Enabled = false;     //Disable the add button once the XML is laoded
            }
        }

        // The query button handles the expert user custom queries
        private void btnQuery_Click(object sender, EventArgs e)
        {
            string userQuery = tbQuery.Text;    // Get the query from the query textbox
            string selectCheck = userQuery.ToUpper();   // Create a new string from the query which is converted to upper case to check it starts with select
            // If the selectCheck strings starts with SELECT, the query is valid and will be run against the database
            if (selectCheck.StartsWith("SELECT"))
            {
                string temp = tbQuery.Text;
                string response = dbAccess.sqlQuery(temp);

                tbData.Text = response;
            }
            // If the user clicks the button without entering a query, a messagebox will inform them to write something in the textbox
            // This stops a null reference exception
            else if (String.IsNullOrEmpty(userQuery))
            {
                MessageBox.Show("Please enter a command in the textbox", "Text Box Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // If the query did not start with SELECT, the user will be notified that they need to enter a valid command
            else
            {
                MessageBox.Show("Only SELECT commands can be used. Please try again", "Illegal Command", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // The Average button handles the novice user query for getting the average top speeds of vehicles in a selected engine category
        private void btnAvg_Click(object sender, EventArgs e)
        {
            DataHandler query = new DataHandler();  // Create a new instance of the DataHandler class named query
            string getAverage = cbVehicleType.SelectedItem.ToString();  // Get the name of the selected engine type from the vehicle type combobox

            try
            {
                float result = Convert.ToSingle(query.AverageSpeed(getAverage));    // Query the database to get the average of all vehicles in the selected category and convert the result to a float
                string number = String.Format("{0:0.00}", result);

                tbData.Text = "The average top speed for '" + getAverage + "' is: " + number;   // Display the result in the data textbox
            }
            // If the result of the query is not in the correct format, this will catch the exception and display a textbox informing the user of the error
            catch(FormatException ex)
            {
                MessageBox.Show("No data could be found", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // The Top 5 button handles the novice user query to get the top 5 vehicles of a selected engine type and attribute
        private void btnTop5_Click(object sender, EventArgs e)
        {
            DataHandler query = new DataHandler();      // Create a new instance of the DataHandler class
            string vehicleType = cbVehicleType.SelectedItem.ToString();     // Get the name of the selected engine type from the vehilce type combobox
            string attribute = String.Empty;    // Create an empty string named attribute which will contain the users selected attribute

            // If/else if statement to get the name of the attribute selected by the user in the atrribute combo box
            // Since the list is prepopulated, the selected index can be used to determine the attribute type
            if (cbAttribute.SelectedIndex == 0)
            {
                attribute = "range";
            }
            else if (cbAttribute.SelectedIndex == 1)
            {
                attribute = "weight";
            }
            else if (cbAttribute.SelectedIndex == 2)
            {
                attribute = "carryCapacity";
            }
            else if (cbAttribute.SelectedIndex == 3)
            {
                attribute = "topspeed";
            }

            // Update the text in the data textbox using the DataHandler to query the database
            tbData.Text = "The top 5 vehicles by " + attribute + " in " + vehicleType + Environment.NewLine +
                Environment.NewLine + query.TopFive(vehicleType, attribute);
        }

        // The runtime button gets the runtime of a user selected vehicle
        private void btnRuntime_Click(object sender, EventArgs e)
        {
            try
            {
                // Use the GetRuntime method to get the runtime of the selected vehicle in the vehicles combo box
                tbData.Text = "Runtime for " +  cbVehicles.SelectedItem.ToString() + Environment.NewLine + Environment.NewLine +
                    GetRuntime(cbVehicles.SelectedItem.ToString());
            }
            // If nothing is selected in the vehicle combo box, notify the user that they must select a vehicle
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Please choose a vehicle", "Vehicle Not Chosen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}