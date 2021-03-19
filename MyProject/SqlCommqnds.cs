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

namespace MyProject
{


    class SqlCommqnds:MainWindow
    {
        static string connectionString = "SERVER= DESKTOP-6FOIV3G;DATABASE= GetBetter;Trusted_Connection=True";
        static SqlConnection sqlconn = new SqlConnection(connectionString);

        public void CheckConnection()
        {
            string firstname;
            Console.WriteLine("Plase enter your name");
            firstname = Console.ReadLine();
            using (sqlconn)
            {

                try
                {
                    sqlconn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [User] WHERE First_Name=@name;", sqlconn);
                    cmd.Parameters.AddWithValue("@name", firstname);

                    int result = (int)cmd.ExecuteScalar();
                    Console.WriteLine(result);
                    sqlconn.Close();
                    if (result > 0)
                    {
                        Console.WriteLine("Seccessfull Login");
                    }
                    else
                    {
                        Console.WriteLine("Incorrect Login");
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
        }
        public void Insert()
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [User] WHERE First_Name=@name;", conn);
                cmd.CommandText = "insert into [User] (User_Id,First_Name,LastName,BirthDay,[Group],IsTeacher,Email,Gender,Extra_Time,Average) values('" + userid.Text + "','" + firstname.Text + "','" + lastname.Text + "','" + birthday.Text + "','" + gruop.Text + "','" + teachername.Text + "','" + email.Text + "','" + gender.Text +"','" + extratime.Text + "','" + average.Text + "')";

                int result = (int)cmd.ExecuteScalar();
                Console.WriteLine(result);
                conn.Close();
                if (result > 0)
                {
                    Console.WriteLine("Seccessfull Login");
                }
                else
                {
                    Console.WriteLine("Incorrect Login");
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
      
    }
        }
        
    

