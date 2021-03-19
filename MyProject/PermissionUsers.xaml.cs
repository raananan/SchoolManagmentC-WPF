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
    /// Interaction logic for PermissionUsers.xaml
    /// </summary>
    public partial class PermissionUsers : Window
    {
        public PermissionUsers()
        {
            InitializeComponent();
            GetPermissionData();
        }
        private void GetPermissionData()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            try
            {
                conn.Open();
                SqlCommand com = new SqlCommand("SELECT *FROM [User_Login]", conn);
                com.Connection = conn;
                SqlDataAdapter datas = new SqlDataAdapter(com);
                System.Data.DataTable dt = new System.Data.DataTable("Tecahers");
                datas.Fill(dt);
                TecherDataTable.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }

        }
    }
}
