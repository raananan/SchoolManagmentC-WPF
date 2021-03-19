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

namespace MyProject
{
    /// <summary>
    /// Interaction logic for Scedual.xaml
    /// </summary>
    public partial class Scedual : Window
    {
        public Scedual()
        {
            InitializeComponent();
        }

        private void eventCommit_Click(object sender, RoutedEventArgs e)
        {
            LoginForm l = new LoginForm();
            l.Show();
            this.Close();
        }
    }
}
