using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sob213_3
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        sob213Entities db = new sob213Entities();
        private readonly EMPLOYEE emp;
        string code;
        public ChangePassword(EMPLOYEE emp)
        {
            InitializeComponent();
            this.emp = emp;
            code = SendCode(emp.EMAIL);
            lMessage.Content = $"Код отправлен на почту {emp.EMAIL}";
        }

        public string GenerateCode()
        {
            string code = "";
            string pool = "qwertyuiopasdfghjklzxcvbnm1234567890";
            Random rnd = new Random();
            for (int i = 0; i < rnd.Next(8, 12); i++)
            {
                code += pool[rnd.Next(pool.Length)];
            }
            return code;
        }

        public string SendCode(string mail)
        {
            SmtpClient client = new SmtpClient("smtp.mail.ru", 587);
            client.Credentials = new NetworkCredential("bwerfwe@mail.ru", "nbnfybr12");
            client.EnableSsl = true;
            MailMessage m2 = new MailMessage();
            m2.From = new MailAddress("bwerfwe@mail.ru");
            m2.To.Add(mail);
            m2.Subject = "CODE FOR CHANGE YOUR PASSWORD";
            string code = GenerateCode();
            m2.Body = $"Введите этот код в поле в программе: {code}";
            client.Send(m2);
            return code;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbPass.Text == tbPassRe.Text)
            {
                EMPLOYEE emp2 = db.EMPLOYEE.Where(c => c.EMP_ID == emp.EMP_ID).SingleOrDefault();
                emp2.PASSWORD = tbPassRe.Text;
                db.SaveChanges();
                MessageBox.Show("Пароль изменен!");
                Close();
            }
            
            
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            if (tbCode.Text == code)
            {
                tbPass.Visibility = Visibility.Visible;
                tbPassRe.Visibility = Visibility.Visible;
                btAccept.Visibility = Visibility.Visible;
            }
        }
    }
}
