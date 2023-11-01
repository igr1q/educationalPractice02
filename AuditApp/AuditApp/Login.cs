using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using auditLibrary;
using System.Windows.Threading;
using System.Configuration;
using AuditApp.Properties;

namespace AuditApp
{
    public partial class Login : Form
    {
        private string text = String.Empty;
        int countFail = 0;
        int pressBtn = 0;
        bool acceptCaptcha = false;
        private DispatcherTimer timer;

        public Login()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(3); // Устанавливаем интервал в 3 минуты
            timer.Tick += Timer_Tick;
            updateCaptcha.Visible = false;
            pictureBox1.Visible = false;
            textBox3.Visible = false;
            button2.Visible = false;
            checkBox1.Visible = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                updateCaptcha.Visible = true;
                pictureBox1.Visible = true;
                textBox3.Visible = true;
                button2.Visible = true;
            }
            else
            {
                updateCaptcha.Visible = false;
                pictureBox1.Visible = false;
                textBox3.Visible = false;
                button2.Visible = false;
            }
        }

        private Bitmap CreateImage(int Width, int Height)
        {
            Random rnd = new Random();

            //Создадим изображение
            Bitmap result = new Bitmap(Width, Height);

            //Вычислим позицию текста
            int Xpos = rnd.Next(0, Width - 50);
            int Ypos = rnd.Next(15, Height - 15);

            //Добавим различные цвета
            Brush[] colors = { Brushes.Black,
                     Brushes.Red,
                     Brushes.RoyalBlue,
                     Brushes.Green };

            //Укажем где рисовать
            Graphics g = Graphics.FromImage((Image)result);

            //Пусть фон картинки будет серым
            g.Clear(Color.Gray);

            //Сгенерируем текст
            text = String.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];

            //Нарисуем сгенирируемый текст
            g.DrawString(text,
                         new Font("Arial", 15),
                         colors[rnd.Next(colors.Length)],
                         new PointF(Xpos, Ypos));

            //Добавим немного помех
            /////Линии из углов
            g.DrawLine(Pens.Black,
                       new Point(0, 0),
                       new Point(Width - 1, Height - 1));
            g.DrawLine(Pens.Black,
                       new Point(0, Height - 1),
                       new Point(Width - 1, 0));
            ////Белые точки
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);

            return result;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void updateCaptcha_Click(object sender, EventArgs e) 
        {
            acceptCaptcha = false;
            pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == this.text)
            {
                acceptCaptcha = true;
                MessageBox.Show("Верно!");
            }

            else
            {
                acceptCaptcha = false;
                MessageBox.Show("Ошибка!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            switch (countFail)
            {
                case 0:
                    try
                    {
                        if (audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).AuthenticateAdmin(login, password))
                        {
                            this.Hide();
                            new MainMenu(this).Show();
                        }
                        else
                        {
                            countFail++;
                            acceptCaptcha = false;
                            pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
                            MessageBox.Show("Ошибка!");
                            checkBox1.Visible = true;
                        }
                    }
                    catch
                    {
                        countFail++;
                        acceptCaptcha = false;
                        pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
                        MessageBox.Show("Такого пользователя нет");
                        checkBox1.Visible = true;
                    }
                    break;
                case 1:
                    try
                    {
                        if (acceptCaptcha == true)
                        {
                            if (audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).AuthenticateAdmin(login, password))
                            {

                                this.Hide();
                                new MainMenu(this).Show();
                            }
                            else
                            {
                                countFail++;
                                acceptCaptcha = false;
                                pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
                                MessageBox.Show("Ошибка!\nСистема блокируется на 3 минуты");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выполните captcha");
                        }
                    }
                    catch
                    {
                        countFail++;
                        acceptCaptcha = false;
                        pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
                        MessageBox.Show("Такого пользователя нет");
                    }
                    break;
                case 2:
                    try
                    {
                        if (acceptCaptcha == true)
                        {
                            if (audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).AuthenticateAdmin(login, password))
                            {

                                this.Hide();
                                new MainMenu(this).Show();
                            }
                            else
                            {
                                countFail++;
                                acceptCaptcha = false;
                                pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
                                MessageBox.Show("Ошибка!\nСистема блокируется на 3 минуты");
                                block();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выполните captcha");
                        }
                    }
                    catch
                    {
                        countFail++;
                        acceptCaptcha = false;
                        pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
                        MessageBox.Show("Такого пользователя нет\nСистема блокируется на 3 минуты");
                        block();
                    }
                    break;
                case 3:
                    try
                    {
                        if (acceptCaptcha == true)
                        {
                            if (audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).AuthenticateAdmin(login, password))
                            {

                                this.Hide();
                                new MainMenu(this).Show();
                            }
                            else
                            {
                                countFail++;
                                acceptCaptcha = false;
                                pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
                                MessageBox.Show("Ошибка!\nСистема закрывается");
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выполните captcha");
                        }
                    }
                    catch
                    {
                        countFail++;
                        acceptCaptcha = false;
                        pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
                        MessageBox.Show("Такого пользователя нет\nПрограмма закрывается");
                        this.Close();
                    }
                    break;
            }
        }
        private void block()
        {
            this.Hide(); // Скрываем форму
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            acceptCaptcha = false;
            pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
            timer.Stop();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(pressBtn%2==0)
            {
                pressBtn++;
                textBox2.UseSystemPasswordChar = false;
                button3.BackgroundImage = Resources.closeeye;

            }
            else
            {
                pressBtn++;
                textBox2.UseSystemPasswordChar = true;
                button3.BackgroundImage = Resources.openeye;
            }
        }
    }
}
