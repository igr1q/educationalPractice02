using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Media;
using auditLibrary;
using System.Drawing.Printing;

namespace AuditApp
{
    public partial class Reports : Form
    {
        Report report;
        CreateReport createReport;
        public Reports(CreateReport createReport,int idEmployee)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add("CompanyName", "Company Name");
            dataGridView1.Columns.Add("HoursWorked", "Hours Worked");
            this.createReport = createReport;
            report = audit.GetInstance().GenerateEmployeeReport(idEmployee);
            textBox1.Text = report.EmployeeName;
            textBox2.Text = report.CalculateTotalHours().ToString();
            textBox3.Text = report.TotalPayment.ToString();
            foreach(var job in report.Jobs)
            {
                dataGridView1.Rows.Add(audit.GetInstance().GetEnterpriseById(job.EnterpriseID).CompanyName, job.HoursWorked);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            PrintDocument printDocument = new PrintDocument();

            // Устанавливаем обработчик события для печати
            printDocument.PrintPage += new PrintPageEventHandler(PrintPage);

            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            // Создаем Bitmap для сохранения содержимого GroupBox
            Bitmap bmp = new Bitmap(groupBox1.Width, groupBox1.Height);
            groupBox1.DrawToBitmap(bmp, new Rectangle(0, 0, groupBox1.Width, groupBox1.Height));

            // Определяем область для печати
            Rectangle bounds = e.PageBounds;

            // Определяем масштаб для печати (если необходимо)
            float scale = Math.Min((float)bounds.Width / (float)bmp.Width, (float)bounds.Height / (float)bmp.Height);

            // Рисуем содержимое GroupBox на странице
            e.Graphics.DrawImage(bmp, bounds);

            // Убираем из памяти Bitmap
            bmp.Dispose();
        }
    }
}
