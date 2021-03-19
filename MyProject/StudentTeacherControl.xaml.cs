using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for StudentTeacherControl.xaml
    /// </summary>
    public partial class StudentTeacherControl : Window
    {
        //Function that checking if all fields are not empties 
        public int FillCheck()
        {
            string[] input = { id.Text.ToString(), firstname.Text.ToString(), lastname.Text.ToString(), ToString(), email.Text.ToString(), gender.Text.ToString(), birthday.Text.ToString(), phonenumber.Text.ToString() };
            string[] Errors = { "ת.ז", "שם פרטי", "שם משפחה", "תאריך לידה", "אימייל", "מין", "מספר טלפון", "סיסצא", "קבוצה" };

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

        //Function that checking the email insert
        static bool IsValidEmail(string email)
        {
            return Regex.Match(email, @"^[a-zA-Z0-9_]+@[a-zA-Z0-9]+\.[a-zA-Z]+$").Success;
        }


        //Function that checking if the texet insert is legel 
        public int LegelTextInput()
        {
            string[] input = { id.Text.ToString(), phonenumber.Text.ToString() };
            string[] Errors = { "ת.ז", "טלפון" };
            int val;
            for (int i = 0; i < input.Length; i++)
            {

                if (!Int32.TryParse(input[i], out val))
                {
                    MessageBox.Show("   שדה לא חוקי ב" + Errors[i]);
                    return 0;
                }
                else if (IsValidEmail(email.Text.ToString()) == false)
                {
                    MessageBox.Show("דואר אלקטרוני לא חוקי");
                    return 0;
                }
                else if (phonenumber.Text.Length != 7)
                {
                    MessageBox.Show("מספר נייד לא תקין");
                    return 0;
                }
            }
            return 1;
        }

        //check if user exist in system
        static bool IdExist(string id)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;

            conn.Open();
            SqlCommand cmd1 = new SqlCommand(@"SELECT COUNT(*) FROM [Students] WHERE Student_Id ='" + id + "'", conn);
            SqlCommand cmd2 = new SqlCommand(@"SELECT COUNT(*) FROM [Teachers] WHERE Teacher_Id ='" + id + "'", conn);

            if (((int)cmd1.ExecuteScalar() > 0)|| ((int)cmd2.ExecuteScalar() > 0))
            {
                return true;
            }

            else
            {

                return false;
            }
        }
        private string idteacher;
        private string groupteacher;

        public StudentTeacherControl(string Idteacher, string Groupteacher)
        {
            this.idteacher = Idteacher;
            this.groupteacher = groupteacher;

            InitializeComponent();
            GetStudentData();


        }
       

        //Display datas of users as table
        private void GetStudentData()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            
            List<string> TeachersList = new List<string>();
            string name;
            conn.Open();
            name = "select First_Name from Teachers";
            SqlCommand cmd = new SqlCommand(name, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TeachersList.Add(reader.GetString(0));
            }
            foreach (var j in TeachersList)
                techerscombox.Items.Add(j);
            conn.Close();
            try
            {
                conn.Open();
                SqlCommand com = new SqlCommand("SELECT *FROM Students", conn);
                com.Connection = conn;
                SqlDataAdapter datas = new SqlDataAdapter(com);
                System.Data.DataTable dt = new System.Data.DataTable("Students");
                datas.Fill(dt);
                StudentDataTable.ItemsSource = dt.DefaultView;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
            conn.Close();
        }
        //Add Student
        private void AddStudent(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            
           
           

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("insert into Students (Student_Id,First_Name,Last_Name,BirthDay,Email,[Group],ExtraTime,Gender,[Password],Average,Phone_Number,TeacherName,Level_ID,Score) values('" + id.Text + "','" + firstname.Text + "','" + lastname.Text + "','" + birthday.Text + "','" + email.Text + "','" + group.Text + "','" + extra.IsChecked + "','" + gender.Text + "','" + averge.Text + "','" + password.Text + "','" + areacode.Text+"-"+phonenumber.Text + "','"+techerscombox.Text+ "','" + 1 + "',"+0+")", conn);
                if (IdExist(id.Text) == true)
                {
                    MessageBox.Show("מספר תעודת זהות קיים במערכת");
                    GetStudentData();
                }
                SqlCommand cmd2 = new SqlCommand("insert into User_Login (User_Id,First_Name,Last_Name,StudentOrTeacher,Password) values('" + id.Text + "','" + firstname.Text + "','" + lastname.Text + "','Student','" + password.Text + "')", conn);

                int res1 = cmd.ExecuteNonQuery();
                int res2 = cmd2.ExecuteNonQuery();

                if (FillCheck() == 0)
                {

                    GetStudentData();
                }
                else if (LegelTextInput() == 0)
                {
                    GetStudentData();
                }

             

                else 
                {
                   
                    if (res1>0 &&res2> 0 /*&& result2 > 0*/)
                    {
                        MessageBox.Show("המשתמש הוכנס בהצלחה");
                        GetStudentData();
                    }
                    else
                    {
                        MessageBox.Show("לא קיים חיבור למערכת");
                    }
                }
                conn.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("לא קיים חיבור למערכת");
            }
            finally
            {
                GetStudentData();
            }

        }
        //Find user as field
        private void Student_Search(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            try
            {
                if (comboxsearch.Text == "בחר/י")
                {
                    textboxusersearch = null;
                    GetStudentData();
                }
                conn.Open();
                SqlCommand com = new SqlCommand("SELECT *FROM Students WHERE " + ((ComboBoxItem)comboxsearch.SelectedItem).Name + "='" + textboxusersearch.Text + "'", conn);
                com.Connection = conn;
                SqlDataAdapter datas = new SqlDataAdapter(com);
                System.Data.DataTable dt = new System.Data.DataTable("Students");
                datas.Fill(dt);
                StudentDataTable.ItemsSource = dt.DefaultView;
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

        //Display user
        private void StudentDisplay(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            try
            {
                conn.Open();
                SqlCommand com = new SqlCommand("SELECT *FROM [Students] WHERE Student_Id='" + Id_Show.Text + "'", conn);
                com.Connection = conn;
                SqlDataAdapter datas = new SqlDataAdapter(com);
                System.Data.DataTable dt = new System.Data.DataTable("Student");
                datas.Fill(dt);
                StudentApper.ItemsSource = dt.DefaultView;
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
        //Delete user
        private void Student_Delete(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            int resual = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Students WHERE Student_Id = '" + Id_Show.Text + "'", conn);
                resual = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Id_Show.Text = null;
                StudentApper.ItemsSource = null;
                MessageBox.Show("המשתמש הוסר מהמערכת");
            }
            GetStudentData();
        }
        //Display user on update screen
        private void User_Display_Update(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            try
            {
                conn.Open();
                SqlCommand com = new SqlCommand("SELECT *FROM [Students] WHERE Student_Id='" + Id_Show_Update.Text + "'", conn);

                SqlDataAdapter datas = new SqlDataAdapter(com);
                System.Data.DataTable dt = new System.Data.DataTable("Student");
                datas.Fill(dt);
                StudentApearUpdate.ItemsSource = dt.DefaultView;
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        firstnameupdate.Text = (reader["First_Name"].ToString());
                        lastnameupdate.Text = (reader["Last_Name"].ToString());
                        birthdayupdate.Text = (reader["BirthDay"].ToString());
                        passwordupdate.Text = (reader["Password"].ToString());
                        emailupdate.Text = (reader["Email"].ToString());
                        groupupdate.Text = (reader["Group"].ToString());
                        genderupdate.Text = (reader["Gender"].ToString());
                        phonenumberupdate.Text = (reader["Phone_Number"].ToString());
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

        }
        //Update user
        private void Student_Update(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE [Student] SET First_Name = @firstname,Last_Name=@lastname,BirthDay=@birthday,Email=@email,Gender=@gender,Phone_Number=@phonenumber WHERE Teacher_Id='" + Id_Show_Update.Text + "'", conn);

                cmd.Parameters.AddWithValue("@firstname", firstnameupdate.Text);
                cmd.Parameters.AddWithValue("@lastname", lastnameupdate.Text);
                cmd.Parameters.AddWithValue("@birthday", birthdayupdate.Text);
                cmd.Parameters.AddWithValue("@Email", emailupdate.Text);
                cmd.Parameters.AddWithValue("@gender", genderupdate.Text);
                cmd.Parameters.AddWithValue("@phonenumber", phonenumberupdate.Text);

                cmd.ExecuteNonQuery();

                conn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                Id_Show_Update.Text = null;
                StudentApearUpdate.ItemsSource = null;
                MessageBox.Show("המשתמש עודכן בהצלחה");
            }
            GetStudentData();
        }

        private void Button_Return(object sender, RoutedEventArgs e)
        {
            TeacherWindow t = new TeacherWindow(idteacher, groupteacher);
            t.Show();
            this.Close();
        }
    }
}
