using System;
using System.Collections.Generic;
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
    /// Interaction logic for Messege.xaml
    /// </summary>
    public partial class Messege : Window
    {
        private string userid;
        private string group;

        public Messege(string Userid, string Group)
        {
            this.userid = Userid;
            this.group = Group;
            InitializeComponent();
            MessesgeDisplay();
            UsersDisplay();
        }

        private void MessesgeDisplay()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            try
            {
               
                conn.Open();
                SqlCommand com = new SqlCommand("SELECT *FROM Messege WHERE User_Id="+userid+"", conn);
                com.Connection = conn;
                SqlDataAdapter datas = new SqlDataAdapter(com);
                System.Data.DataTable dt = new System.Data.DataTable("Messege");
                datas.Fill(dt);
                MessegesTable.ItemsSource = dt.DefaultView;
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

       
    
        private void UsersDisplay()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            List<string> UsersList = new List<string>();
            string name=null;

            string teacher_student=null;

            conn.Open();

            try
            {
                
            SqlCommand cmd_2 = new SqlCommand("SELECT * FROM [User_Login] WHERE User_Id ='" + userid + "'", conn);//יוצר חיבור לסרבר, שבו אני שולף מטבלת משתמשים את מה שהכנסתי בטופס
            SqlDataReader reader = cmd_2.ExecuteReader();//קריאת מידע מהטבלה בסרבר
            if (reader.Read())
            {
                teacher_student = (reader["StudentOrTeacher"].ToString());//קורא אם הוא מורה או תלמיד
            }
            reader.Close();

            if (teacher_student == "Student")
            {
                name = "select First_Name from Teachers Where [Group]= " + group+"";
            }
            else
            {
                name = "select First_Name from Students Where [Group]= "+group+"";
            }

            SqlCommand cmd = new SqlCommand(name, conn);
            SqlDataReader reader2 = cmd.ExecuteReader();

                while (reader2.Read())
            {
                UsersList.Add(reader2.GetString(0));
                  
                }
            foreach (var j in UsersList)
                userscombox.Items.Add(j);

                reader2.Close();
                conn.Close();

                
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("ההודעה לא נשלחה");
            }
            finally
            {
                Console.ReadLine();
            }
           
        }
        private void MessegeSend()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            string getid = null;

           
            try
            {
                conn.Open();
                SqlCommand getiduser = new SqlCommand("SELECT User_Id FROM User_Login WHERE First_Name='" + userscombox.Text + "'", conn);
               

                SqlDataReader reader = getiduser.ExecuteReader();

                if (reader.Read())
                {
                    getid = (reader["User_Id"].ToString());

                }
                reader.Close();

                SqlCommand insertmes = new SqlCommand("INSERT INTO Messege(Messege_Text,User_Id) values('"+messegetext.Text+"',"+getid+")",conn);
                int result = (int)insertmes.ExecuteNonQuery();
                if(result>0)
                MessageBox.Show("ההודעה  נשלחה");
                else
                    MessageBox.Show("ההודעה לא נשלחה");
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("ההודעה לא נשלחה");
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private void messegesend_Click(object sender, RoutedEventArgs e)
        {
            MessegeSend();
        }

        private void Button_Return(object sender, RoutedEventArgs e)
        {
            LoginForm l = new LoginForm();
            l.Show();
            this.Close();
        }
    }
}
