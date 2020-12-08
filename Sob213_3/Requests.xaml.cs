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
    /// Interaction logic for Requests.xaml
    /// </summary>
    public partial class Requests : Window
    {
        sob213Entities db = new sob213Entities();
        public Requests()
        {
            InitializeComponent();
            dg.ItemsSource = db.REQUEST.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            REQUEST req = dg.SelectedItem as REQUEST;
            if (req == null)
                return;
            EMPLOYEE emp = db.EMPLOYEE.Where(c => c.EMP_ID == req.EMP_ID).SingleOrDefault();
            emp.EMAIL = req.NEW_EMAIL;
            db.REQUEST.Remove(req);
            db.SaveChanges();
            dg.ItemsSource = db.REQUEST.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            REQUEST req = dg.SelectedItem as REQUEST;
            if (req == null)
                return;
            db.REQUEST.Remove(req);
            db.SaveChanges();
            dg.ItemsSource = db.REQUEST.ToList();
        }
    }
}
