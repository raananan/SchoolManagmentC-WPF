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
    /// Interaction logic for Grades.xaml
    /// </summary>
    public partial class Grades : Window
    {
        private string id;

        public Grades(string Id)
        {
            this.id = Id;
           
            InitializeComponent();
            GetGrades();

        }
        private void GetGrades()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
           
                try
                {

                    conn.Open();
                    SqlCommand com = new SqlCommand("SELECT *FROM Grades WHERE User_Id=" + id + "", conn);
                    com.Connection = conn;
                    SqlDataAdapter datas = new SqlDataAdapter(com);
                    System.Data.DataTable dt = new System.Data.DataTable("Grades");
                    datas.Fill(dt);
                    GradesDisplay.ItemsSource = dt.DefaultView;
                    conn.Close();
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
    

