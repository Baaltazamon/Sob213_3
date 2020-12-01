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
using System.Windows.Shapes;

namespace Sob213_3
{
    /// <summary>
    /// Логика взаимодействия для OpManager.xaml
    /// </summary>
    public partial class OpManager : Window
    {
        sob213Entities db = new sob213Entities();
        public OpManager(string log, string lastname)
        {
            InitializeComponent();
            lbWelcome.Content = $"Здравствуйте, {log}({lastname})!";
            dpStart.DisplayDateEnd = DateTime.Now;
            dpEnd.DisplayDateStart = DateTime.Now;
            Update();
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
        private void Update()
        {
            dgEmployee.ItemsSource = db.EMPLOYEE.ToList();
            cbSuperrior.ItemsSource = db.EMPLOYEE.ToList();
            cbDepartment.ItemsSource = db.DEPARTMENT.ToList();
            cbBranch.ItemsSource = db.BRANCH.ToList();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }
       

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbFN.Text) || String.IsNullOrEmpty(tbLN.Text) || String.IsNullOrEmpty(tbLog.Text)
                || String.IsNullOrEmpty(dpStart.Text))
                return;
            
            EMPLOYEE emp = new EMPLOYEE
            {
                FIRST_NAME = tbFN.Text,
                LAST_NAME = tbLN.Text,
                DEPT_ID = (cbDepartment.SelectedItem as DEPARTMENT).DEPT_ID,
                ASSIGNED_BRANCH_ID = (cbBranch.SelectedItem as BRANCH).BRANCH_ID,
                TITLE = cbTitle.Text,
                LOGIN = tbLog.Text,
                START_DATE = Convert.ToDateTime(dpStart.Text),
                PASSWORD = "qwe"
            };
            db.EMPLOYEE.Add(emp);
            db.SaveChanges();
            Update();
        }
    
    }
}
