namespace AuditApp
{
    partial class MainMenu
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ReportsBtn = new System.Windows.Forms.Button();
            this.JobsBtn = new System.Windows.Forms.Button();
            this.EnterpriseBtn = new System.Windows.Forms.Button();
            this.EmployeeBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(-4, 1);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1069, 554);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(157)))), ((int)(((byte)(136)))));
            this.tabPage1.Controls.Add(this.ReportsBtn);
            this.tabPage1.Controls.Add(this.JobsBtn);
            this.tabPage1.Controls.Add(this.EnterpriseBtn);
            this.tabPage1.Controls.Add(this.EmployeeBtn);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1061, 525);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Админ панель";
            // 
            // ReportsBtn
            // 
            this.ReportsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(209)))), ((int)(((byte)(150)))));
            this.ReportsBtn.Location = new System.Drawing.Point(435, 367);
            this.ReportsBtn.Margin = new System.Windows.Forms.Padding(4);
            this.ReportsBtn.Name = "ReportsBtn";
            this.ReportsBtn.Size = new System.Drawing.Size(137, 57);
            this.ReportsBtn.TabIndex = 3;
            this.ReportsBtn.Text = "Отчёты";
            this.ReportsBtn.UseVisualStyleBackColor = false;
            this.ReportsBtn.Click += new System.EventHandler(this.ReportsBtn_Click);
            // 
            // JobsBtn
            // 
            this.JobsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(209)))), ((int)(((byte)(150)))));
            this.JobsBtn.Location = new System.Drawing.Point(435, 279);
            this.JobsBtn.Margin = new System.Windows.Forms.Padding(4);
            this.JobsBtn.Name = "JobsBtn";
            this.JobsBtn.Size = new System.Drawing.Size(137, 57);
            this.JobsBtn.TabIndex = 2;
            this.JobsBtn.Text = "Работы";
            this.JobsBtn.UseVisualStyleBackColor = false;
            this.JobsBtn.Click += new System.EventHandler(this.JobsBtn_Click);
            // 
            // EnterpriseBtn
            // 
            this.EnterpriseBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(209)))), ((int)(((byte)(150)))));
            this.EnterpriseBtn.Location = new System.Drawing.Point(435, 197);
            this.EnterpriseBtn.Margin = new System.Windows.Forms.Padding(4);
            this.EnterpriseBtn.Name = "EnterpriseBtn";
            this.EnterpriseBtn.Size = new System.Drawing.Size(137, 57);
            this.EnterpriseBtn.TabIndex = 1;
            this.EnterpriseBtn.Text = "Предприятия";
            this.EnterpriseBtn.UseVisualStyleBackColor = false;
            this.EnterpriseBtn.Click += new System.EventHandler(this.EnterpriseBtn_Click);
            // 
            // EmployeeBtn
            // 
            this.EmployeeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(209)))), ((int)(((byte)(150)))));
            this.EmployeeBtn.Location = new System.Drawing.Point(435, 114);
            this.EmployeeBtn.Margin = new System.Windows.Forms.Padding(4);
            this.EmployeeBtn.Name = "EmployeeBtn";
            this.EmployeeBtn.Size = new System.Drawing.Size(137, 57);
            this.EmployeeBtn.TabIndex = 0;
            this.EmployeeBtn.Text = "Сотрудники";
            this.EmployeeBtn.UseVisualStyleBackColor = false;
            this.EmployeeBtn.Click += new System.EventHandler(this.EmployeeBtn_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(157)))), ((int)(((byte)(136)))));
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1061, 525);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Логи входов";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 34);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1035, 422);
            this.dataGridView1.TabIndex = 0;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainMenu_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button ReportsBtn;
        private System.Windows.Forms.Button JobsBtn;
        private System.Windows.Forms.Button EnterpriseBtn;
        private System.Windows.Forms.Button EmployeeBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}