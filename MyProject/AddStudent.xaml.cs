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
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        public int FillCheck()//פונקציה שבה אני בודק אם כל השדות מולאו
        {//יצרתי שני מערכים, במערך הראשון אני שם את כל השדות בטופס הניתנות למילוי, ובמערך השני אני שם הודעות שגיאה
            string[] input = { userid.Text.ToString(), firstname.Text, lastname.Text, birthday.Text, gruop.Text, teachername.Text, email.Text, gender.Text, extratime.Text, average.Text.ToString() };
            string[] Errors = { "ת.ז", "שם פרטי", "שם משפחה", "תאריך לידה", "קבוצה", "שם מורה", "אימייל", "מגדר", "תוספת זמן", "ממוצע" };

            for (int i = 0; i < input.Length; i++)
            {
                if (string.IsNullOrEmpty(input[i]))
                {
                    MessageBox.Show(" לא מילאת שדה " + Errors[i]);
                    return 0;
                }

            }
            return 1;
        }
        public AddStudent()
        {
            InitializeComponent();
            binddatagrid();
        }

        private void binddatagrid()//פונקציה שבה הטבלה שואבת נתונים מהסרבר
        {
            SqlConnection conn = new SqlConnection();//חיבור חדש
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;

            try
            {
                conn.Open();
                SqlCommand com = new SqlCommand("SELECT *FROM [User]", conn);//שאילתא שבה אני שןאב נתונים של משתמש מהסרבר
                com.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(com);//יוצר מופע חדש ממחלקה השואבת מידע
                System.Data.DataTable dt = new System.Data.DataTable("User");
                da.Fill(dt);
                g1.ItemsSource = dt.DefaultView;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;



            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into [User] (User_Id,First_Name,LastName,BirthDay,[Group],Password,IsTeacher,Email,Gender,Extra_Time,Average) values('" + userid.Text + "','" + firstname.Text + "','" + lastname.Text + "','" + birthday.Text + "','" + gruop.Text + "','" + password.Text + "','" + teachername.Text + "','" + email.Text + "','" + gender.Text + "','" + extratime.Text + "','" + average.Text + "')", conn);//שאילתא שבה אני מכניס נתונים לפי מה שהכנסתי בטופס
                SqlCommand cmd_2= new SqlCommand("insert into User_Login (User_Id,First_Name,Last_Name,StudentOrTeacher,Password) values('" + userid.Text + "','" + firstname.Text + "','"+lastname.Text+"','Student','" + password.Text + "')", conn);//באותו זמן אני מכניס גם לטבלת משתמשים אשר יש הרשאות חיבור
               
                if (FillCheck() == 0)
                {

                    binddatagrid();
                }


                else
                {
                    int result = (int)cmd.ExecuteNonQuery();
                    int result_2 = (int)cmd_2.ExecuteNonQuery();
                    conn.Close();
                    if (result > 0&&result_2>0)
                    {
                        MessageBox.Show("המשתמש הוכנס בהצלחה");
                        binddatagrid();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Login");
                    }
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
            binddatagrid();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//מוחק משתמש
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM [User] WHERE User_Id = '" + rownum.Text + "'", conn);
                SqlCommand cmd_2 = new SqlCommand("DELETE FROM User_Login WHERE User_Id = '" + rownum.Text + "'", conn);

                cmd.ExecuteNonQuery();
                cmd_2.ExecuteNonQuery();
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
            binddatagrid();
        }
    }
}