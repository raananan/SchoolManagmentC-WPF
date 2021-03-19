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
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.Specialized;

namespace MyProject
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        public class FuncClass
        {
            

            public FuncClass()
            {
                
            }

            public void Print(string id,string password)
            {
                SqlConnection conn = new SqlConnection();//יוצר מחבר
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;//המחבר מתחבר לאסקיואל סרבר בטבלה שיצרתי

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM User_Login WHERE User_Id ="+id+" AND Password="+password+"", conn);//שאילתא שבה אני אומר מתןך טבלת משתמשים איפה ששמתי ת.ז בטופס וגם סיסמא ששמתי בטופס שייכים לאותה שורה      

                    int result = (int)cmd.ExecuteScalar();//מונה שאילתות
                    string name = "SELECT * FROM [User_Login] WHERE User_Id ='" + id + "'";//מכניס לתוך משתנה שאילתא שבה אני אומר שמתוך טבלת משתמשים איפה שהת.ז מה שהכנסתי בטופס
                    string teacher_student = "SELECT * FROM[User_Login]";//משתנה שבתוכו יש שאילתא שאומרת תשלפוף את כל הנתונים מהטבלה של משתמשים
                    string info_for_combox= "SELECT Student_Id,First_Name,Last_Name,[Group],Level_ID FROM Students WHERE Student_Id ='" + id + "'";
                    string GetDetails= "SELECT *FROM Students WHERE Student_Id ='" + id + "'";
                    string iduser = "SELECT Student_Id FROM Students WHERE Student_Id ='" + id + "'";
                    string firstname = "SELECT First_Name FROM Students WHERE Student_Id ='" + id + "'";
                    string lastname = "SELECT Last_Name FROM Students WHERE Student_Id ='" + id + "'";
                    string group = "SELECT Group FROM Students WHERE Student_Id ='" + id + "'";
                    string groupteacher = "SELECT Group FROM Teachers WHERE Student_Id ='" + id + "'";
                    string level = "SELECT Level_ID FROM Students WHERE Student_Id ='" + id + "'";
                    
                    SqlCommand cmd_2 = new SqlCommand(name, conn);//יוצר חיבור לסרבר, שבו אני שולף מטבלת משתמשים את מה שהכנסתי בטופס
                    SqlDataReader reader = cmd_2.ExecuteReader();//קריאת מידע מהטבלה בסרבר
                    if (reader.Read())
                    {
                        name = (reader["First_Name"].ToString());//קורא את השם משתמש בטבלה
                        teacher_student = (reader["StudentOrTeacher"].ToString());//קורא אם הוא מורה או תלמיד
                    }
                    reader.Close();

                    SqlCommand cmd_4 = new SqlCommand(GetDetails, conn);
                    SqlDataReader reader2 = cmd_4.ExecuteReader();
                    if (reader2.Read())
                    {
                         iduser = (reader2["Student_Id"].ToString());
                         firstname = (reader2["First_Name"].ToString());
                         lastname = (reader2["Last_Name"].ToString());
                        group = (reader2["group"].ToString());
                        level = (reader2["Level_ID"].ToString());
                    }
                    reader2.Close();

                    if (teacher_student == "Admin")//אם תלמיד
                    {
                        
                        Admin a = new Admin();//יפתח חלון בהתאם
                        a.Show();                      
                       
                    }

                    else if (teacher_student == "Student")//אם תלמיד
                    {
                        StudentWindow s = new StudentWindow(iduser, name,lastname, group,level);//יפתח חלון בהתאם
                        s.Show();
                        s.Header.Content = name + " ברוך הבא";
                        s.Header.Foreground = Brushes.White;
                       
                        conn.Close();
                    }
                    else
                    {
                        TeacherWindow t = new TeacherWindow(id, groupteacher);
                        t.Show();
                        t.Header.Content = name + " ברוך הבא";
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("הפרטים שהזנת אינם נכונים");
                    Console.WriteLine(ex.Message);
                    LoginForm l = new LoginForm();
                    l.Show();
                }
                finally
                {
                    Console.ReadLine();
                }
                
            }
        }
      
        
        private void btn_connect_Click(object sender, RoutedEventArgs e)
        {
            FuncClass f = new FuncClass();
            f.Print(ID_Connect.Text, Pass_Connect.Text);
            this.Close();
       
         }
        }
    }
