using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
namespace MyProject
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();


        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            combox1.IsDropDownOpen = true;
            st.Opacity = 100;
        }

        private void st_MouseLeave(object sender, MouseEventArgs e)
        {
            combox1.IsDropDownOpen = false; ;
            st.Opacity = 0.2;

        }

        private void st2_MouseEnter(object sender, MouseEventArgs e)
        {
            combox2.IsDropDownOpen = true;
            st2.Opacity = 100;
        }

        private void st2_MouseLeave(object sender, MouseEventArgs e)
        {
            combox2.IsDropDownOpen = false; ;
            st2.Opacity = 0.2;
        }

        private void st3_MouseEnter(object sender, MouseEventArgs e)
        {
            combox3.IsDropDownOpen = true;
            st3.Opacity = 100;
        }

        private void st3_MouseLeave(object sender, MouseEventArgs e)
        {
            combox3.IsDropDownOpen = false; ;
            st3.Opacity = 0.2;
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            TeachersAdminControl t = new TeachersAdminControl();
            t.Show();
            this.Close();
        }

        private void permissiontablecombox(object sender, RoutedEventArgs e)
        {
            PermissionUsers p = new PermissionUsers();
            p.Show();
            this.Close();

        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            Scedual s = new Scedual();
            s.Show();
            this.Close();

        }
    }
}
