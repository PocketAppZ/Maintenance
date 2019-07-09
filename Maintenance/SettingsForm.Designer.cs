namespace Maintenance
{
    partial class SettingsForm
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
            this.LoggingBox = new System.Windows.Forms.CheckBox();
            this.DiskCheckBox = new System.Windows.Forms.CheckBox();
            this.HelpButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FilesHideBrowse = new System.Windows.Forms.Button();
            this.FilesDelBrowse = new System.Windows.Forms.Button();
            this.FilesHideRemove = new System.Windows.Forms.Button();
            this.FilesDelRemove = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FilesHideBox = new System.Windows.Forms.ComboBox();
            this.FilesToDelBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DirDelBrowse = new System.Windows.Forms.Button();
            this.DirDelRemove = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DirToDelBox = new System.Windows.Forms.ComboBox();
            this.PathFilesDelOldBrowse = new System.Windows.Forms.Button();
            this.PathFilesDelBrowse = new System.Windows.Forms.Button();
            this.PathFilesDelOldRemove = new System.Windows.Forms.Button();
            this.PathFilesDelRemove = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PathFilesDelOldBox = new System.Windows.Forms.ComboBox();
            this.PathFilesDelBox = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ServicesTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ServicesManualButton = new System.Windows.Forms.Button();
            this.ServicesDisableButton = new System.Windows.Forms.Button();
            this.ServicesManualRemove = new System.Windows.Forms.Button();
            this.ServicesDisableRemove = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ServicesManualBox = new System.Windows.Forms.ComboBox();
            this.ServicsDisableBox = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TasksTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TasksDisableButton = new System.Windows.Forms.Button();
            this.TasksDisableRemove = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.TasksDisableBox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoggingBox
            // 
            this.LoggingBox.AutoSize = true;
            this.LoggingBox.Location = new System.Drawing.Point(478, 468);
            this.LoggingBox.Name = "LoggingBox";
            this.LoggingBox.Size = new System.Drawing.Size(106, 17);
            this.LoggingBox.TabIndex = 9;
            this.LoggingBox.Text = "Logging Enabled";
            this.LoggingBox.UseVisualStyleBackColor = true;
            this.LoggingBox.CheckedChanged += new System.EventHandler(this.LoggingBox_CheckedChanged);
            // 
            // DiskCheckBox
            // 
            this.DiskCheckBox.AutoSize = true;
            this.DiskCheckBox.Location = new System.Drawing.Point(315, 468);
            this.DiskCheckBox.Name = "DiskCheckBox";
            this.DiskCheckBox.Size = new System.Drawing.Size(144, 17);
            this.DiskCheckBox.TabIndex = 10;
            this.DiskCheckBox.Text = "Run Disk Check Monthly";
            this.DiskCheckBox.UseVisualStyleBackColor = true;
            this.DiskCheckBox.CheckedChanged += new System.EventHandler(this.DiskCheckBox_CheckedChanged);
            // 
            // HelpButton
            // 
            this.HelpButton.Location = new System.Drawing.Point(15, 462);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(48, 23);
            this.HelpButton.TabIndex = 19;
            this.HelpButton.Text = "?";
            this.HelpButton.UseVisualStyleBackColor = true;
            this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.FilesHideBrowse);
            this.groupBox1.Controls.Add(this.FilesDelBrowse);
            this.groupBox1.Controls.Add(this.FilesHideRemove);
            this.groupBox1.Controls.Add(this.FilesDelRemove);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.FilesHideBox);
            this.groupBox1.Controls.Add(this.FilesToDelBox);
            this.groupBox1.Location = new System.Drawing.Point(0, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(596, 94);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File";
            // 
            // FilesHideBrowse
            // 
            this.FilesHideBrowse.Location = new System.Drawing.Point(500, 57);
            this.FilesHideBrowse.Name = "FilesHideBrowse";
            this.FilesHideBrowse.Size = new System.Drawing.Size(82, 23);
            this.FilesHideBrowse.TabIndex = 46;
            this.FilesHideBrowse.Text = "Browse";
            this.FilesHideBrowse.UseVisualStyleBackColor = true;
            this.FilesHideBrowse.Click += new System.EventHandler(this.FilesHideBrowse_Click);
            // 
            // FilesDelBrowse
            // 
            this.FilesDelBrowse.Location = new System.Drawing.Point(500, 26);
            this.FilesDelBrowse.Name = "FilesDelBrowse";
            this.FilesDelBrowse.Size = new System.Drawing.Size(82, 23);
            this.FilesDelBrowse.TabIndex = 45;
            this.FilesDelBrowse.Text = "Browse";
            this.FilesDelBrowse.UseVisualStyleBackColor = true;
            this.FilesDelBrowse.Click += new System.EventHandler(this.FilesDelBrowse_Click);
            // 
            // FilesHideRemove
            // 
            this.FilesHideRemove.Location = new System.Drawing.Point(412, 57);
            this.FilesHideRemove.Name = "FilesHideRemove";
            this.FilesHideRemove.Size = new System.Drawing.Size(82, 23);
            this.FilesHideRemove.TabIndex = 44;
            this.FilesHideRemove.Text = "Remove";
            this.FilesHideRemove.UseVisualStyleBackColor = true;
            this.FilesHideRemove.Click += new System.EventHandler(this.FilesHideRemove_Click);
            // 
            // FilesDelRemove
            // 
            this.FilesDelRemove.Location = new System.Drawing.Point(412, 26);
            this.FilesDelRemove.Name = "FilesDelRemove";
            this.FilesDelRemove.Size = new System.Drawing.Size(82, 23);
            this.FilesDelRemove.TabIndex = 43;
            this.FilesDelRemove.Text = "Remove";
            this.FilesDelRemove.UseVisualStyleBackColor = true;
            this.FilesDelRemove.Click += new System.EventHandler(this.FilesDelRemove_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "Files To Hide:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Files To Delete:";
            // 
            // FilesHideBox
            // 
            this.FilesHideBox.FormattingEnabled = true;
            this.FilesHideBox.Location = new System.Drawing.Point(160, 58);
            this.FilesHideBox.Name = "FilesHideBox";
            this.FilesHideBox.Size = new System.Drawing.Size(246, 21);
            this.FilesHideBox.TabIndex = 40;
            // 
            // FilesToDelBox
            // 
            this.FilesToDelBox.FormattingEnabled = true;
            this.FilesToDelBox.Location = new System.Drawing.Point(160, 27);
            this.FilesToDelBox.Name = "FilesToDelBox";
            this.FilesToDelBox.Size = new System.Drawing.Size(246, 21);
            this.FilesToDelBox.TabIndex = 39;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox2.Controls.Add(this.DirDelBrowse);
            this.groupBox2.Controls.Add(this.DirDelRemove);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.DirToDelBox);
            this.groupBox2.Controls.Add(this.PathFilesDelOldBrowse);
            this.groupBox2.Controls.Add(this.PathFilesDelBrowse);
            this.groupBox2.Controls.Add(this.PathFilesDelOldRemove);
            this.groupBox2.Controls.Add(this.PathFilesDelRemove);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.PathFilesDelOldBox);
            this.groupBox2.Controls.Add(this.PathFilesDelBox);
            this.groupBox2.Location = new System.Drawing.Point(0, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(596, 131);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Path";
            // 
            // DirDelBrowse
            // 
            this.DirDelBrowse.Location = new System.Drawing.Point(500, 31);
            this.DirDelBrowse.Name = "DirDelBrowse";
            this.DirDelBrowse.Size = new System.Drawing.Size(82, 23);
            this.DirDelBrowse.TabIndex = 57;
            this.DirDelBrowse.Text = "Browse";
            this.DirDelBrowse.UseVisualStyleBackColor = true;
            this.DirDelBrowse.Click += new System.EventHandler(this.DirDelBrowse_Click);
            // 
            // DirDelRemove
            // 
            this.DirDelRemove.Location = new System.Drawing.Point(412, 31);
            this.DirDelRemove.Name = "DirDelRemove";
            this.DirDelRemove.Size = new System.Drawing.Size(82, 23);
            this.DirDelRemove.TabIndex = 56;
            this.DirDelRemove.Text = "Remove";
            this.DirDelRemove.UseVisualStyleBackColor = true;
            this.DirDelRemove.Click += new System.EventHandler(this.DirDelRemove_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 55;
            this.label1.Text = "Directories To Delete:";
            // 
            // DirToDelBox
            // 
            this.DirToDelBox.FormattingEnabled = true;
            this.DirToDelBox.Location = new System.Drawing.Point(160, 32);
            this.DirToDelBox.Name = "DirToDelBox";
            this.DirToDelBox.Size = new System.Drawing.Size(246, 21);
            this.DirToDelBox.TabIndex = 54;
            // 
            // PathFilesDelOldBrowse
            // 
            this.PathFilesDelOldBrowse.Location = new System.Drawing.Point(500, 92);
            this.PathFilesDelOldBrowse.Name = "PathFilesDelOldBrowse";
            this.PathFilesDelOldBrowse.Size = new System.Drawing.Size(82, 23);
            this.PathFilesDelOldBrowse.TabIndex = 53;
            this.PathFilesDelOldBrowse.Text = "Browse";
            this.PathFilesDelOldBrowse.UseVisualStyleBackColor = true;
            this.PathFilesDelOldBrowse.Click += new System.EventHandler(this.PathFilesDelOldBrowse_Click);
            // 
            // PathFilesDelBrowse
            // 
            this.PathFilesDelBrowse.Location = new System.Drawing.Point(500, 61);
            this.PathFilesDelBrowse.Name = "PathFilesDelBrowse";
            this.PathFilesDelBrowse.Size = new System.Drawing.Size(82, 23);
            this.PathFilesDelBrowse.TabIndex = 52;
            this.PathFilesDelBrowse.Text = "Browse";
            this.PathFilesDelBrowse.UseVisualStyleBackColor = true;
            this.PathFilesDelBrowse.Click += new System.EventHandler(this.PathFilesDelBrowse_Click);
            // 
            // PathFilesDelOldRemove
            // 
            this.PathFilesDelOldRemove.Location = new System.Drawing.Point(412, 92);
            this.PathFilesDelOldRemove.Name = "PathFilesDelOldRemove";
            this.PathFilesDelOldRemove.Size = new System.Drawing.Size(82, 23);
            this.PathFilesDelOldRemove.TabIndex = 51;
            this.PathFilesDelOldRemove.Text = "Remove";
            this.PathFilesDelOldRemove.UseVisualStyleBackColor = true;
            this.PathFilesDelOldRemove.Click += new System.EventHandler(this.PathFilesDelOldRemove_Click);
            // 
            // PathFilesDelRemove
            // 
            this.PathFilesDelRemove.Location = new System.Drawing.Point(412, 61);
            this.PathFilesDelRemove.Name = "PathFilesDelRemove";
            this.PathFilesDelRemove.Size = new System.Drawing.Size(82, 23);
            this.PathFilesDelRemove.TabIndex = 50;
            this.PathFilesDelRemove.Text = "Remove";
            this.PathFilesDelRemove.UseVisualStyleBackColor = true;
            this.PathFilesDelRemove.Click += new System.EventHandler(this.PathFilesDelRemove_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 13);
            this.label5.TabIndex = 49;
            this.label5.Text = "Path Files To Delete Older:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "Path Files To Delete:";
            // 
            // PathFilesDelOldBox
            // 
            this.PathFilesDelOldBox.FormattingEnabled = true;
            this.PathFilesDelOldBox.Location = new System.Drawing.Point(160, 93);
            this.PathFilesDelOldBox.Name = "PathFilesDelOldBox";
            this.PathFilesDelOldBox.Size = new System.Drawing.Size(246, 21);
            this.PathFilesDelOldBox.TabIndex = 47;
            // 
            // PathFilesDelBox
            // 
            this.PathFilesDelBox.FormattingEnabled = true;
            this.PathFilesDelBox.Location = new System.Drawing.Point(160, 62);
            this.PathFilesDelBox.Name = "PathFilesDelBox";
            this.PathFilesDelBox.Size = new System.Drawing.Size(246, 21);
            this.PathFilesDelBox.TabIndex = 46;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox3.Controls.Add(this.ServicesTextBox);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.ServicesManualButton);
            this.groupBox3.Controls.Add(this.ServicesDisableButton);
            this.groupBox3.Controls.Add(this.ServicesManualRemove);
            this.groupBox3.Controls.Add(this.ServicesDisableRemove);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.ServicesManualBox);
            this.groupBox3.Controls.Add(this.ServicsDisableBox);
            this.groupBox3.Location = new System.Drawing.Point(0, 229);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(596, 127);
            this.groupBox3.TabIndex = 48;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Service";
            // 
            // ServicesTextBox
            // 
            this.ServicesTextBox.Location = new System.Drawing.Point(160, 92);
            this.ServicesTextBox.Name = "ServicesTextBox";
            this.ServicesTextBox.Size = new System.Drawing.Size(246, 20);
            this.ServicesTextBox.TabIndex = 60;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(51, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 13);
            this.label9.TabIndex = 59;
            this.label9.Text = "Service Name:";
            // 
            // ServicesManualButton
            // 
            this.ServicesManualButton.Location = new System.Drawing.Point(500, 90);
            this.ServicesManualButton.Name = "ServicesManualButton";
            this.ServicesManualButton.Size = new System.Drawing.Size(82, 23);
            this.ServicesManualButton.TabIndex = 58;
            this.ServicesManualButton.Text = "Manual";
            this.ServicesManualButton.UseVisualStyleBackColor = true;
            this.ServicesManualButton.Click += new System.EventHandler(this.ServicesManualButton_Click);
            // 
            // ServicesDisableButton
            // 
            this.ServicesDisableButton.Location = new System.Drawing.Point(412, 90);
            this.ServicesDisableButton.Name = "ServicesDisableButton";
            this.ServicesDisableButton.Size = new System.Drawing.Size(82, 23);
            this.ServicesDisableButton.TabIndex = 57;
            this.ServicesDisableButton.Text = "Disabled";
            this.ServicesDisableButton.UseVisualStyleBackColor = true;
            this.ServicesDisableButton.Click += new System.EventHandler(this.ServicesDisableButton_Click);
            // 
            // ServicesManualRemove
            // 
            this.ServicesManualRemove.Location = new System.Drawing.Point(412, 57);
            this.ServicesManualRemove.Name = "ServicesManualRemove";
            this.ServicesManualRemove.Size = new System.Drawing.Size(82, 23);
            this.ServicesManualRemove.TabIndex = 56;
            this.ServicesManualRemove.Text = "Remove";
            this.ServicesManualRemove.UseVisualStyleBackColor = true;
            this.ServicesManualRemove.Click += new System.EventHandler(this.ServicesManualRemove_Click);
            // 
            // ServicesDisableRemove
            // 
            this.ServicesDisableRemove.Location = new System.Drawing.Point(412, 26);
            this.ServicesDisableRemove.Name = "ServicesDisableRemove";
            this.ServicesDisableRemove.Size = new System.Drawing.Size(82, 23);
            this.ServicesDisableRemove.TabIndex = 55;
            this.ServicesDisableRemove.Text = "Remove";
            this.ServicesDisableRemove.UseVisualStyleBackColor = true;
            this.ServicesDisableRemove.Click += new System.EventHandler(this.ServicesDisableRemove_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 13);
            this.label7.TabIndex = 54;
            this.label7.Text = "Services To Manual:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 53;
            this.label6.Text = "Services To Disable:";
            // 
            // ServicesManualBox
            // 
            this.ServicesManualBox.FormattingEnabled = true;
            this.ServicesManualBox.Location = new System.Drawing.Point(160, 58);
            this.ServicesManualBox.Name = "ServicesManualBox";
            this.ServicesManualBox.Size = new System.Drawing.Size(246, 21);
            this.ServicesManualBox.TabIndex = 52;
            // 
            // ServicsDisableBox
            // 
            this.ServicsDisableBox.FormattingEnabled = true;
            this.ServicsDisableBox.Location = new System.Drawing.Point(160, 27);
            this.ServicsDisableBox.Name = "ServicsDisableBox";
            this.ServicsDisableBox.Size = new System.Drawing.Size(246, 21);
            this.ServicsDisableBox.TabIndex = 51;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox4.Controls.Add(this.TasksTextBox);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.TasksDisableButton);
            this.groupBox4.Controls.Add(this.TasksDisableRemove);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.TasksDisableBox);
            this.groupBox4.Location = new System.Drawing.Point(0, 358);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(596, 104);
            this.groupBox4.TabIndex = 49;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Task";
            // 
            // TasksTextBox
            // 
            this.TasksTextBox.Location = new System.Drawing.Point(160, 63);
            this.TasksTextBox.Name = "TasksTextBox";
            this.TasksTextBox.Size = new System.Drawing.Size(246, 20);
            this.TasksTextBox.TabIndex = 47;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(51, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 46;
            this.label10.Text = "Task Name:";
            // 
            // TasksDisableButton
            // 
            this.TasksDisableButton.Location = new System.Drawing.Point(412, 61);
            this.TasksDisableButton.Name = "TasksDisableButton";
            this.TasksDisableButton.Size = new System.Drawing.Size(82, 23);
            this.TasksDisableButton.TabIndex = 45;
            this.TasksDisableButton.Text = "Disabled";
            this.TasksDisableButton.UseVisualStyleBackColor = true;
            this.TasksDisableButton.Click += new System.EventHandler(this.TasksDisableButton_Click);
            // 
            // TasksDisableRemove
            // 
            this.TasksDisableRemove.Location = new System.Drawing.Point(412, 27);
            this.TasksDisableRemove.Name = "TasksDisableRemove";
            this.TasksDisableRemove.Size = new System.Drawing.Size(82, 23);
            this.TasksDisableRemove.TabIndex = 44;
            this.TasksDisableRemove.Text = "Remove";
            this.TasksDisableRemove.UseVisualStyleBackColor = true;
            this.TasksDisableRemove.Click += new System.EventHandler(this.TasksDisableRemove_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 43;
            this.label8.Text = "Tasks To Disable:";
            // 
            // TasksDisableBox
            // 
            this.TasksDisableBox.FormattingEnabled = true;
            this.TasksDisableBox.Location = new System.Drawing.Point(160, 28);
            this.TasksDisableBox.Name = "TasksDisableBox";
            this.TasksDisableBox.Size = new System.Drawing.Size(246, 21);
            this.TasksDisableBox.TabIndex = 42;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 495);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.HelpButton);
            this.Controls.Add(this.DiskCheckBox);
            this.Controls.Add(this.LoggingBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(612, 534);
            this.MinimumSize = new System.Drawing.Size(612, 534);
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox LoggingBox;
        private System.Windows.Forms.CheckBox DiskCheckBox;
        private System.Windows.Forms.Button HelpButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button FilesHideBrowse;
        private System.Windows.Forms.Button FilesDelBrowse;
        private System.Windows.Forms.Button FilesHideRemove;
        private System.Windows.Forms.Button FilesDelRemove;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox FilesHideBox;
        private System.Windows.Forms.ComboBox FilesToDelBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button DirDelBrowse;
        private System.Windows.Forms.Button DirDelRemove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DirToDelBox;
        private System.Windows.Forms.Button PathFilesDelOldBrowse;
        private System.Windows.Forms.Button PathFilesDelBrowse;
        private System.Windows.Forms.Button PathFilesDelOldRemove;
        private System.Windows.Forms.Button PathFilesDelRemove;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox PathFilesDelOldBox;
        private System.Windows.Forms.ComboBox PathFilesDelBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox ServicesTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button ServicesManualButton;
        private System.Windows.Forms.Button ServicesDisableButton;
        private System.Windows.Forms.Button ServicesManualRemove;
        private System.Windows.Forms.Button ServicesDisableRemove;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox ServicesManualBox;
        private System.Windows.Forms.ComboBox ServicsDisableBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox TasksTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button TasksDisableButton;
        private System.Windows.Forms.Button TasksDisableRemove;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox TasksDisableBox;
    }
}