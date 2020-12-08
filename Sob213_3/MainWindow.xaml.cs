using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sob213_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        sob213Entities db = new sob213Entities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            EMPLOYEE emp = db.EMPLOYEE.Where(c => c.LOGIN == tbLogin.Text && c.PASSWORD == pbPassword.Password).SingleOrDefault();
            if (emp == null)
            {
                pbPassword.Clear();
                MessageBox.Show("Такой комбинации логин/пароль не существует", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (emp.END_DATE != null)
            {
                MessageBox.Show("Поздравляю, Вас уволили!", "Давай досвидания!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (emp.PASSWORD == "qwe")
            {
                ChangePassword ChP = new ChangePassword(emp);
                ChP.ShowDialog();
            }
            if (emp.TITLE == "Operations Manager")
            {
                OpManager op = new OpManager(emp.LOGIN, emp.LAST_NAME);
                op.Show();
                Close();
            }
            else 
            {
                EmployeWindow em = new EmployeWindow(emp);
                em.Show();
            }
        }
    }
}
