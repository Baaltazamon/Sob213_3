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
                PASSWORD = "qwe",
                EMAIL = tbMail.Text
            };
            db.EMPLOYEE.Add(emp);
            db.SaveChanges();
            Update();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            EMPLOYEE emp = dgEmployee.SelectedItem as EMPLOYEE;
            if (emp == null)
                return;
            emp.FIRST_NAME = tbFN.Text;
            emp.LAST_NAME = tbLN.Text;
            emp.LOGIN = tbLog.Text;
            emp.ASSIGNED_BRANCH_ID = (cbBranch.SelectedItem as BRANCH).BRANCH_ID;
            emp.DEPT_ID = (cbDepartment.SelectedItem as DEPARTMENT).DEPT_ID;
            emp.TITLE = cbTitle.Text;
            emp.EMAIL = tbMail.Text;
            if (cbSuperrior.SelectedIndex != -1)
            {
                emp.SUPERIOR_EMP_ID = (cbSuperrior.SelectedItem as EMPLOYEE).EMP_ID;
            }
            emp.START_DATE = DateTime.Parse(dpStart.Text);
            if (dpEnd.Text != "")
            {
                emp.END_DATE = DateTime.Parse(dpEnd.Text);
            }
            db.SaveChanges();
            Update();
        }

        private void dgEmployee_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            EMPLOYEE emp = dgEmployee.SelectedItem as EMPLOYEE;
            if (emp == null)
                return;
            tbFN.Text = emp.FIRST_NAME;
            tbLN.Text = emp.LAST_NAME;
            tbLog.Text = emp.LOGIN;
            cbBranch.SelectedItem = db.BRANCH.Where(c => c.BRANCH_ID == emp.ASSIGNED_BRANCH_ID).FirstOrDefault();
            cbTitle.Text = emp.TITLE;
            cbSuperrior.SelectedItem = (emp.SUPERIOR_EMP_ID == null) ? null : db.EMPLOYEE.Where(c => c.EMP_ID == emp.SUPERIOR_EMP_ID).FirstOrDefault();
            cbDepartment.SelectedItem = db.DEPARTMENT.Where(c => c.DEPT_ID == emp.DEPT_ID).SingleOrDefault();
            dpStart.Text = emp.START_DATE.ToString();
            dpEnd.Text = emp.END_DATE.ToString();
            tbMail.Text = emp.EMAIL;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            tbFN.Clear();
            tbLN.Clear();
            tbLog.Clear();
            cbBranch.SelectedIndex = 0;
            cbDepartment.SelectedIndex = 0;
            cbSuperrior.SelectedIndex = -1;
            cbTitle.SelectedIndex = 0;
            dpStart.SelectedDate = DateTime.Now;
            dpEnd.SelectedDate = DateTime.Now;
            tbMail.Clear();
            
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            EMPLOYEE emp = dgEmployee.SelectedItem as EMPLOYEE;
            if (emp == null)
                return;
            db.EMPLOYEE.Remove(emp);
            db.SaveChanges();
            Update();
        }
    }
}
