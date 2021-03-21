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
            cbVehicleType.SelectedIndex = 0;
            cbAttribute.SelectedIndex = 0;

            string temp1 = "DELETE FROM Vehicle";
            string response1 = dbAccess.sqlChange(temp1);

            string temp2 = "DELETE FROM Petrol";
            string response2 = dbAccess.sqlChange(temp2);

            string temp3 = "DELETE FROM Electric";
            string response3 = dbAccess.sqlChange(temp3);
        }

        bool isComplete = false;

        public class DataHandler
        {
            public void GetVehicleInformation(ref string engine, ref string vehicleID, ref string range, ref string weight,
                ref string carryCapacity, ref string topSpeed, ref string noise, ref string cylinders, ref string voltage, ref string brushed, XmlNode vehicle)
            {
                XmlElement vehicleElement = (XmlElement)vehicle;

                engine = vehicleElement["engine"].InnerText;
                string engineCheck = engine.ToLower();

                if (engineCheck.Equals("petrol"))
                {
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
                    MessageBox.Show(("Incorrect XML data for engine type in " + vehicleElement["vehicleID"].InnerText + Environment.NewLine + "Data was not added"), "XML Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            public string AverageSpeed(string vehicle)
            {
                if (vehicle.Equals("All Vehicles"))
                {
                    string query = "SELECT AVG(topspeed) FROM Vehicle";
                    string response = dbAccess.sqlQuery(query);
                    return response;
                }
                else
                {
                    string query = "SELECT AVG(topspeed) FROM Vehicle WHERE engine = '" + vehicle + "'";
                    string response = dbAccess.sqlQuery(query);
                    return response;
                }
            }

            public string TopFive(string vehicle, string attr)
            {
                if (vehicle.Equals("All Vehicles"))
                {
                    string query = "SELECT " + attr + " , vehicleID FROM Vehicle ORDER BY " + attr + " desc LIMIT 5";
                    string response = dbAccess.sqlQuery(query);
                    return response;
                }
                else
                {
                    string query = "SELECT " + attr + " , vehicleID FROM Vehicle WHERE engine = '" + vehicle + "' ORDER BY " + attr + " desc LIMIT 5";
                    string response = dbAccess.sqlQuery(query);
                    return response;
                }
            }
        }

        private void AddToDB()
        {
            isComplete = false;

            DataHandler dataHandler = new DataHandler();
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load("vehicleInfo.xml");
                
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

                int progress = 0;

                XmlNodeList nodeList;
                XmlNode root = doc.DocumentElement;

                nodeList = root.SelectNodes("vehicle");

                BeginInvoke(((Action)delegate ()
                {
                    pbLoadProgress.Maximum = nodeList.Count;
                }));

                for (int i = 0; i < nodeList.Count; i++)
                {
                    dataHandler.GetVehicleInformation(ref engine, ref vehicleID, ref range, ref weight, ref carryCapacity, ref topSpeed,
                        ref noise, ref cylinders, ref voltage, ref brushed, nodeList[i]);

                    string engineCheck = engine.ToLower();

                    if (engineCheck.Equals("petrol"))
                    {
                        string temp1 = "INSERT INTO Vehicle (vehicleID, engine, range, weight, carryCapacity, topspeed) VALUES ('" + vehicleID + "', '" + engine + "', '" + range +
                                "', '" + weight + "', '" + carryCapacity + "', '" + topSpeed + "')";

                        string response1 = dbAccess.sqlChange(temp1);
                        string errorCheck = response1.ToLower();

                        if (errorCheck.StartsWith("constraint"))
                        {
                            MessageBox.Show("An entry for " + vehicleID + " already exists and has not been added", "Unique Constraint Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        string temp2 = "INSERT INTO Petrol (vehicleID, noise, cylinders) VALUES ('" + vehicleID + "', '" + noise + "', '" + cylinders + "')";

                        string response2 = dbAccess.sqlChange(temp2);

                        progress = i;
                        loadXML.ReportProgress(i);
                    }
                    else if (engineCheck.Equals("electric"))
                    {
                        string temp1 = "INSERT INTO Vehicle (vehicleID, engine, range, weight, carryCapacity, topspeed) VALUES ('" + vehicleID + "', '" + engine + "', '" + range +
                             "', '" + weight + "', '" + carryCapacity + "', '" + topSpeed + "')";

                        string response1 = dbAccess.sqlChange(temp1);
                        string errorCheck = response1.ToLower();

                        if (errorCheck.StartsWith("constraint"))
                        {
                            MessageBox.Show("An entry for " + vehicleID + " already exists and has not been added", "Unique Constraint Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        string temp2 = "INSERT INTO Electric (vehicleID, voltage, brushed) VALUES ('" + vehicleID + "', '" + voltage + "', '" + brushed + "')";

                        string response2 = dbAccess.sqlChange(temp2);

                        progress = i;
                        loadXML.ReportProgress(i);
                    }

                    isComplete = true;                   
                }
            }
            catch (FileNotFoundException ex)
            {
                string message = ex.Message;
                MessageBox.Show(message, "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isComplete = false;
            }
            catch (XmlException ex)
            {
                string message = ex.Message;
                MessageBox.Show(message, "Badly Formed XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isComplete = false;

                BeginInvoke(((Action)delegate ()
                {
                    btnAdd.Enabled = true;
                }));                
            }
        }

        private void GetVehicles()
        {
            string temp = "SELECT vehicleID FROM Vehicle";
            string response = dbAccess.sqlQuery(temp);

            string[] vehicles = response.Split('\n');

            foreach (string name in vehicles)
            {
                cbVehicles.Items.Add(name);
            }

            cbVehicles.SelectedIndex = 0;
        }

        public string GetRuntime(string vehicleName)
        {
            try
            {
                string vehicle = vehicleName.Replace("\t\r", String.Empty);
                float speed;
                float range;
                string runtime = "";
                string query1 = "SELECT topspeed FROM Vehicle WHERE vehicleID = '" + vehicle + "'";
                string query2 = "SELECT range FROM Vehicle WHERE vehicleID = '" + vehicle + "'";
                string response1 = dbAccess.sqlQuery(query1);
                speed = float.Parse(response1);
                string response2 = dbAccess.sqlQuery(query2);

                range = float.Parse(response2);

                runtime = (range / speed).ToString();

                return runtime;
            }
            catch (FormatException ex)
            {
                MessageBox.Show("The vehicle name supplied was invalid", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            loadXML.RunWorkerAsync();
        }

        private void loadXML_DoWork(object sender, DoWorkEventArgs e)
        {
            AddToDB();
        }

        private void loadXML_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbLoadProgress.Value = e.ProgressPercentage;
        }

        private void loadXML_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GetVehicles();
            pbLoadProgress.Value = 0;            

            if (isComplete)
            {
                MessageBox.Show("XML successfully added to the DB!");
                btnAdd.Enabled = false;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string userQuery = tbQuery.Text;
            string selectCheck = userQuery.ToUpper();

            if (selectCheck.StartsWith("SELECT"))
            {
                string temp = tbQuery.Text;
                string response = dbAccess.sqlQuery(temp);

                tbData.Text = response;
            }
            else if (String.IsNullOrEmpty(userQuery))
            {
                MessageBox.Show("Please enter a command in the textbox", "Text Box Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Only SELECT commands can be used. Please try again", "Illegal Command", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAvg_Click(object sender, EventArgs e)
        {
            DataHandler query = new DataHandler();
            string getAverage = cbVehicleType.SelectedItem.ToString();

            try
            {
                float result = Convert.ToSingle(query.AverageSpeed(getAverage));
                string number = String.Format("{0:0.00}", result);

                tbData.Text = "The average top speed for '" + getAverage + "' is: " + number;
            }
            catch(FormatException ex)
            {
                MessageBox.Show("No data could be found", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTop5_Click(object sender, EventArgs e)
        {
            DataHandler query = new DataHandler();
            string vehicleType = cbVehicleType.SelectedItem.ToString();
            string attribute = String.Empty;

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

            tbData.Text = "The top 5 vehicles by " + attribute + " in " + vehicleType + Environment.NewLine +
                Environment.NewLine + query.TopFive(vehicleType, attribute);
        }

        private void btnRuntime_Click(object sender, EventArgs e)
        {
            try
            {
                tbData.Text = "Runtime for " +  cbVehicles.SelectedItem.ToString() + Environment.NewLine + Environment.NewLine +
                    GetRuntime(cbVehicles.SelectedItem.ToString());
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Please choose a vehicle", "Vehicle Not Chosen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}