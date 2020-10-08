namespace EL2311_Assignment
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbData = new System.Windows.Forms.TextBox();
            this.lblQuick = new System.Windows.Forms.Label();
            this.lblSelectType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSelectVehicle = new System.Windows.Forms.Label();
            this.lblAvgSpeed = new System.Windows.Forms.Label();
            this.btnAvg = new System.Windows.Forms.Button();
            this.btnTop5 = new System.Windows.Forms.Button();
            this.cbVehicleType = new System.Windows.Forms.ComboBox();
            this.cbAttribute = new System.Windows.Forms.ComboBox();
            this.cbVehicles = new System.Windows.Forms.ComboBox();
            this.btnRuntime = new System.Windows.Forms.Button();
            this.lblExpert = new System.Windows.Forms.Label();
            this.lblQuery = new System.Windows.Forms.Label();
            this.tbQuery = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.loadXML = new System.ComponentModel.BackgroundWorker();
            this.pbLoadProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // tbData
            // 
            this.tbData.BackColor = System.Drawing.SystemColors.Control;
            this.tbData.Location = new System.Drawing.Point(289, 12);
            this.tbData.Multiline = true;
            this.tbData.Name = "tbData";
            this.tbData.ReadOnly = true;
            this.tbData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbData.Size = new System.Drawing.Size(814, 498);
            this.tbData.TabIndex = 0;
            // 
            // lblQuick
            // 
            this.lblQuick.AutoSize = true;
            this.lblQuick.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuick.Location = new System.Drawing.Point(12, 132);
            this.lblQuick.Name = "lblQuick";
            this.lblQuick.Size = new System.Drawing.Size(106, 20);
            this.lblQuick.TabIndex = 1;
            this.lblQuick.Text = "Quick Query";
            // 
            // lblSelectType
            // 
            this.lblSelectType.AutoSize = true;
            this.lblSelectType.Location = new System.Drawing.Point(13, 175);
            this.lblSelectType.Name = "lblSelectType";
            this.lblSelectType.Size = new System.Drawing.Size(105, 13);
            this.lblSelectType.TabIndex = 2;
            this.lblSelectType.Text = "Select Vehicle Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Select Category:";
            // 
            // lblSelectVehicle
            // 
            this.lblSelectVehicle.AutoSize = true;
            this.lblSelectVehicle.Location = new System.Drawing.Point(39, 390);
            this.lblSelectVehicle.Name = "lblSelectVehicle";
            this.lblSelectVehicle.Size = new System.Drawing.Size(78, 13);
            this.lblSelectVehicle.TabIndex = 4;
            this.lblSelectVehicle.Text = "Select Vehicle:";
            // 
            // lblAvgSpeed
            // 
            this.lblAvgSpeed.AutoSize = true;
            this.lblAvgSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgSpeed.Location = new System.Drawing.Point(12, 349);
            this.lblAvgSpeed.Name = "lblAvgSpeed";
            this.lblAvgSpeed.Size = new System.Drawing.Size(76, 20);
            this.lblAvgSpeed.TabIndex = 5;
            this.lblAvgSpeed.Text = "Runtime";
            // 
            // btnAvg
            // 
            this.btnAvg.Location = new System.Drawing.Point(124, 291);
            this.btnAvg.Name = "btnAvg";
            this.btnAvg.Size = new System.Drawing.Size(121, 39);
            this.btnAvg.TabIndex = 6;
            this.btnAvg.Text = "Average Top Speed";
            this.btnAvg.UseVisualStyleBackColor = true;
            this.btnAvg.Click += new System.EventHandler(this.btnAvg_Click);
            // 
            // btnTop5
            // 
            this.btnTop5.Location = new System.Drawing.Point(124, 243);
            this.btnTop5.Name = "btnTop5";
            this.btnTop5.Size = new System.Drawing.Size(121, 39);
            this.btnTop5.TabIndex = 7;
            this.btnTop5.Text = "Top 5";
            this.btnTop5.UseVisualStyleBackColor = true;
            this.btnTop5.Click += new System.EventHandler(this.btnTop5_Click);
            // 
            // cbVehicleType
            // 
            this.cbVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVehicleType.FormattingEnabled = true;
            this.cbVehicleType.Items.AddRange(new object[] {
            "All Vehicles",
            "Electric",
            "Petrol"});
            this.cbVehicleType.Location = new System.Drawing.Point(124, 172);
            this.cbVehicleType.Name = "cbVehicleType";
            this.cbVehicleType.Size = new System.Drawing.Size(121, 21);
            this.cbVehicleType.TabIndex = 8;
            // 
            // cbAttribute
            // 
            this.cbAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAttribute.FormattingEnabled = true;
            this.cbAttribute.Items.AddRange(new object[] {
            "Range",
            "Weight",
            "Carry Capacity",
            "Top Speed"});
            this.cbAttribute.Location = new System.Drawing.Point(124, 204);
            this.cbAttribute.Name = "cbAttribute";
            this.cbAttribute.Size = new System.Drawing.Size(121, 21);
            this.cbAttribute.TabIndex = 9;
            // 
            // cbVehicles
            // 
            this.cbVehicles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVehicles.FormattingEnabled = true;
            this.cbVehicles.Location = new System.Drawing.Point(123, 387);
            this.cbVehicles.Name = "cbVehicles";
            this.cbVehicles.Size = new System.Drawing.Size(121, 21);
            this.cbVehicles.TabIndex = 10;
            // 
            // btnRuntime
            // 
            this.btnRuntime.Location = new System.Drawing.Point(123, 425);
            this.btnRuntime.Name = "btnRuntime";
            this.btnRuntime.Size = new System.Drawing.Size(121, 39);
            this.btnRuntime.TabIndex = 11;
            this.btnRuntime.Text = "Get Runtime";
            this.btnRuntime.UseVisualStyleBackColor = true;
            this.btnRuntime.Click += new System.EventHandler(this.btnRuntime_Click);
            // 
            // lblExpert
            // 
            this.lblExpert.AutoSize = true;
            this.lblExpert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpert.Location = new System.Drawing.Point(285, 513);
            this.lblExpert.Name = "lblExpert";
            this.lblExpert.Size = new System.Drawing.Size(153, 20);
            this.lblExpert.TabIndex = 12;
            this.lblExpert.Text = "Expert Users Only";
            // 
            // lblQuery
            // 
            this.lblQuery.AutoSize = true;
            this.lblQuery.Location = new System.Drawing.Point(286, 549);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(38, 13);
            this.lblQuery.TabIndex = 13;
            this.lblQuery.Text = "Query:";
            // 
            // tbQuery
            // 
            this.tbQuery.Location = new System.Drawing.Point(330, 546);
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Size = new System.Drawing.Size(547, 20);
            this.tbQuery.TabIndex = 14;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(902, 536);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(121, 39);
            this.btnQuery.TabIndex = 15;
            this.btnQuery.Text = "Query Database";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(12, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(233, 53);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "Load XML to Database";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // loadXML
            // 
            this.loadXML.WorkerReportsProgress = true;
            this.loadXML.DoWork += new System.ComponentModel.DoWorkEventHandler(this.loadXML_DoWork);
            this.loadXML.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.loadXML_ProgressChanged);
            this.loadXML.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.loadXML_RunWorkerCompleted);
            // 
            // pbLoadProgress
            // 
            this.pbLoadProgress.Location = new System.Drawing.Point(12, 76);
            this.pbLoadProgress.Name = "pbLoadProgress";
            this.pbLoadProgress.Size = new System.Drawing.Size(231, 23);
            this.pbLoadProgress.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 609);
            this.Controls.Add(this.pbLoadProgress);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.tbQuery);
            this.Controls.Add(this.lblQuery);
            this.Controls.Add(this.lblExpert);
            this.Controls.Add(this.btnRuntime);
            this.Controls.Add(this.cbVehicles);
            this.Controls.Add(this.cbAttribute);
            this.Controls.Add(this.cbVehicleType);
            this.Controls.Add(this.btnTop5);
            this.Controls.Add(this.btnAvg);
            this.Controls.Add(this.lblAvgSpeed);
            this.Controls.Add(this.lblSelectVehicle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblSelectType);
            this.Controls.Add(this.lblQuick);
            this.Controls.Add(this.tbData);
            this.Name = "Form1";
            this.Text = "Vehicle Database";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbData;
        private System.Windows.Forms.Label lblQuick;
        private System.Windows.Forms.Label lblSelectType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSelectVehicle;
        private System.Windows.Forms.Label lblAvgSpeed;
        private System.Windows.Forms.Button btnAvg;
        private System.Windows.Forms.Button btnTop5;
        private System.Windows.Forms.ComboBox cbVehicleType;
        private System.Windows.Forms.ComboBox cbAttribute;
        private System.Windows.Forms.ComboBox cbVehicles;
        private System.Windows.Forms.Button btnRuntime;
        private System.Windows.Forms.Label lblExpert;
        private System.Windows.Forms.Label lblQuery;
        private System.Windows.Forms.TextBox tbQuery;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnAdd;
        private System.ComponentModel.BackgroundWorker loadXML;
        private System.Windows.Forms.ProgressBar pbLoadProgress;
    }
}

