
namespace WiFiPass
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing ) {
            if ( disposing && ( components != null ) ) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ( ) {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.listBox_NetworkList = new System.Windows.Forms.ListBox();
            this.menuStrip_Main = new System.Windows.Forms.MenuStrip();
            this.menustripitem_Application = new System.Windows.Forms.ToolStripMenuItem();
            this.menustripitem_Application_About = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menustripitem_Application_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.label_NetworkList = new System.Windows.Forms.Label();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.label_Password = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button_CopyPassword = new System.Windows.Forms.Button();
            this.label_SecurityType = new System.Windows.Forms.Label();
            this.label_SecurityTypeText = new System.Windows.Forms.Label();
            this.menuStrip_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox_NetworkList
            // 
            this.listBox_NetworkList.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listBox_NetworkList.FormattingEnabled = true;
            this.listBox_NetworkList.ItemHeight = 16;
            this.listBox_NetworkList.Items.AddRange(new object[] {
            "No saved networks"});
            this.listBox_NetworkList.Location = new System.Drawing.Point(12, 69);
            this.listBox_NetworkList.Name = "listBox_NetworkList";
            this.listBox_NetworkList.Size = new System.Drawing.Size(343, 164);
            this.listBox_NetworkList.TabIndex = 1;
            this.listBox_NetworkList.SelectedIndexChanged += new System.EventHandler(this.onSelectedNetworkChanged);
            // 
            // menuStrip_Main
            // 
            this.menuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menustripitem_Application});
            this.menuStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_Main.Name = "menuStrip_Main";
            this.menuStrip_Main.Size = new System.Drawing.Size(694, 24);
            this.menuStrip_Main.TabIndex = 2;
            this.menuStrip_Main.Text = "menuStrip1";
            this.menuStrip_Main.MouseEnter += new System.EventHandler(this.onMenuStripMouseEntering);
            this.menuStrip_Main.MouseLeave += new System.EventHandler(this.onMenuStripMouseLeaving);
            // 
            // menustripitem_Application
            // 
            this.menustripitem_Application.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menustripitem_Application_About,
            this.toolStripSeparator1,
            this.menustripitem_Application_Exit});
            this.menustripitem_Application.Name = "menustripitem_Application";
            this.menustripitem_Application.Size = new System.Drawing.Size(80, 20);
            this.menustripitem_Application.Text = "Application";
            // 
            // menustripitem_Application_About
            // 
            this.menustripitem_Application_About.Name = "menustripitem_Application_About";
            this.menustripitem_Application_About.Size = new System.Drawing.Size(107, 22);
            this.menustripitem_Application_About.Text = "About";
            this.menustripitem_Application_About.Click += new System.EventHandler(this.menustripitem_Application_About_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(104, 6);
            // 
            // menustripitem_Application_Exit
            // 
            this.menustripitem_Application_Exit.Name = "menustripitem_Application_Exit";
            this.menustripitem_Application_Exit.Size = new System.Drawing.Size(107, 22);
            this.menustripitem_Application_Exit.Text = "Exit";
            this.menustripitem_Application_Exit.Click += new System.EventHandler(this.menustripitem_Application_Exit_Click);
            // 
            // label_NetworkList
            // 
            this.label_NetworkList.AutoSize = true;
            this.label_NetworkList.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_NetworkList.Location = new System.Drawing.Point(12, 44);
            this.label_NetworkList.Name = "label_NetworkList";
            this.label_NetworkList.Size = new System.Drawing.Size(181, 22);
            this.label_NetworkList.TabIndex = 3;
            this.label_NetworkList.Text = "Saved Wi-Fi Networks:";
            // 
            // textBox_Password
            // 
            this.textBox_Password.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox_Password.Location = new System.Drawing.Point(422, 208);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.ReadOnly = true;
            this.textBox_Password.Size = new System.Drawing.Size(256, 25);
            this.textBox_Password.TabIndex = 4;
            this.textBox_Password.Text = "None";
            // 
            // label_Password
            // 
            this.label_Password.AutoSize = true;
            this.label_Password.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_Password.Location = new System.Drawing.Point(422, 183);
            this.label_Password.Name = "label_Password";
            this.label_Password.Size = new System.Drawing.Size(85, 22);
            this.label_Password.TabIndex = 5;
            this.label_Password.Text = "Password:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button1.Location = new System.Drawing.Point(12, 239);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(343, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Refresh List";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.onRefreshButtonClicked);
            // 
            // button_CopyPassword
            // 
            this.button_CopyPassword.Enabled = false;
            this.button_CopyPassword.Location = new System.Drawing.Point(422, 239);
            this.button_CopyPassword.Name = "button_CopyPassword";
            this.button_CopyPassword.Size = new System.Drawing.Size(256, 23);
            this.button_CopyPassword.TabIndex = 7;
            this.button_CopyPassword.Text = "Copy to Clipboard";
            this.button_CopyPassword.UseVisualStyleBackColor = true;
            this.button_CopyPassword.Click += new System.EventHandler(this.onCopyPasswordClicked);
            // 
            // label_SecurityType
            // 
            this.label_SecurityType.AutoSize = true;
            this.label_SecurityType.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_SecurityType.Location = new System.Drawing.Point(422, 64);
            this.label_SecurityType.Name = "label_SecurityType";
            this.label_SecurityType.Size = new System.Drawing.Size(118, 22);
            this.label_SecurityType.TabIndex = 8;
            this.label_SecurityType.Text = "Security Type:";
            // 
            // label_SecurityTypeText
            // 
            this.label_SecurityTypeText.AutoSize = true;
            this.label_SecurityTypeText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_SecurityTypeText.Location = new System.Drawing.Point(422, 85);
            this.label_SecurityTypeText.Name = "label_SecurityTypeText";
            this.label_SecurityTypeText.Size = new System.Drawing.Size(16, 21);
            this.label_SecurityTypeText.TabIndex = 9;
            this.label_SecurityTypeText.Text = "-";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 274);
            this.Controls.Add(this.label_SecurityTypeText);
            this.Controls.Add(this.label_SecurityType);
            this.Controls.Add(this.button_CopyPassword);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label_Password);
            this.Controls.Add(this.textBox_Password);
            this.Controls.Add(this.label_NetworkList);
            this.Controls.Add(this.listBox_NetworkList);
            this.Controls.Add(this.menuStrip_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip_Main;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(768, 512);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Wi-Fi Pass";
            this.menuStrip_Main.ResumeLayout(false);
            this.menuStrip_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listBox_NetworkList;
        private System.Windows.Forms.MenuStrip menuStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem menustripitem_Application;
        private System.Windows.Forms.ToolStripMenuItem menustripitem_Application_About;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menustripitem_Application_Exit;
        private System.Windows.Forms.Label label_NetworkList;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.Label label_Password;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_CopyPassword;
        private System.Windows.Forms.Label label_SecurityType;
        private System.Windows.Forms.Label label_SecurityTypeText;
    }
}

