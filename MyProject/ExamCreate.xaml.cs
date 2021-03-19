using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
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
    /// Interaction logic for ExamCreate.xaml
    /// </summary>
    public partial class ExamCreate : Window
    {
        static int flag = 0;
        public ExamCreate()
        {
            InitializeComponent();

        }
        List<string> values = new List<string>();

        private void InsertQuestion()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            string DatalevelInsert = ((ComboBoxItem)level_selected.SelectedItem).Content.ToString();

            string ID = null;
            string OfID = null;
            string[] OfferedArray = new string[4] { offered_answer_1.Text, offered_answer_2.Text, offered_answer_3.Text, offered_answer_4.Text };

            int res1 = 0;
            int res2 = 0;
            int res3 = 0;
            int sub = 0;
            int lev = 0;


            switch (((ComboBoxItem)subject_selected.SelectedItem).Name.ToString())
            {
                case "Math_Question":
                    sub = 1;
                    break;

                case "English_Questions":
                    sub = 2;
                    break;

                case "Bible_Questions":
                    sub = 3;
                    break;
                case "History_Questions":
                    sub = 4;
                    break;
                case "Chemesty_Questions":
                    sub = 5;
                    break;
                case "Programing_Questions":
                    sub = 6;
                    break;

                default:

                    break;
            }

            switch (((ComboBoxItem)level_selected.SelectedItem).Name.ToString())
            {
                case "level_1":
                    lev = 1;
                    break;

                case "level_2":
                    lev = 2;
                    break;
                case "level_3":
                    lev = 3;
                    break;

                case "level_4":
                    lev = 4;
                    break;
                case "level_5":
                    lev = 5;
                    break;

                case "level_6":
                    lev = 6;
                    break;
                default:

                    break;
            }
            try
            {
                conn.Open();/*הכנסת שאלה חדשה ביצירת שאלון*/
                SqlCommand cmd1 = new SqlCommand("insert into Question (Question_Text,Subject_ID,Level_ID) values('" + question.Text.ToString() + "'," + sub + ", " + lev + ")", conn);
                res1 = (int)cmd1.ExecuteNonQuery();
                /*אני רוצה להכניס תשובות מוצעות אבל אני צריך את האיידי הנוכחי לכן אצור פקודה חדשה שמשם אשלוף את האאידי הנוכחי ואשים אותו במתשנה*/
                SqlCommand cmd = new SqlCommand("select *from Question", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())//פה אקרא מידע (ת.ז של שאלה) כדי שהיה לי לשאילתא הבאה
                    {
                        ID = (reader["Question_ID"].ToString());
                    }
                    reader.Close();
                }

                for (int i = 0; i < 4; i++)
                {

                    SqlCommand cmd3 = new SqlCommand("insert into Offered_Answer (Offered_Answer_Text,Subject_ID,Question_ID) values('" + OfferedArray[i] + "'," + sub + "," + ID + ")", conn);
                    res3 = (int)cmd3.ExecuteNonQuery();
                    if (i == 0)
                    {
                        SqlCommand cmd0 = new SqlCommand("select *from Offered_Answer", conn);
                        SqlDataReader reader2 = cmd0.ExecuteReader();
                        {
                            while (reader2.Read())
                            {
                                OfID = (reader2["Offered_Answer_ID"].ToString());
                            }
                            reader2.Close();
                        }
                    }
                }

                SqlCommand cmd2 = new SqlCommand("insert into Answer (Question_ID,Offered_Answer_ID) values(" + ID + "," + OfID + ")", conn);

                res2 = (int)cmd2.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                Console.ReadLine();
                MessageBox.Show("השאלה הוכנסה בהצלחה");
            }
        }
        /// <summary>
        /// </summary>
        private void DisplayQuestion()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            int sub = 0;

            switch (((ComboBoxItem)SubjetToDelete.SelectedItem).Name.ToString())
            {
                case "Question":
                    sub = 1;

                    break;
                case "Enlish_Question":
                    sub = 2;
                    break;

                case "Bible_Question":
                    sub = 3;
                    break;

                case "History_Question":
                    sub = 4;
                    break;

                case "Chemesty_Question":
                    sub = 5;
                    break;

                case "Programing_Question":
                    sub = 6;
                    break;
                default:
                    break;
            }

            try
            {
                conn.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Question WHERE  Subject_ID = " + sub + " and Question_Text LIKE '%" + TexrFind.Text + "%'", conn);
                com.Connection = conn;
                SqlDataAdapter datas = new SqlDataAdapter(com);
                System.Data.DataTable dt = new System.Data.DataTable(((ComboBoxItem)SubjetToDelete.SelectedItem).Name.ToString()); datas.Fill(dt);
                QuestionTableToDelete.ItemsSource = dt.DefaultView;

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
        private void DeleteQuestion()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;

            int res1 = 0;
            int res2 = 0;
            int res3 = 0;

            try
            {
                foreach (var i in values)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Question WHERE Question_ID = " + i + "", conn);
                    SqlCommand cmd1 = new SqlCommand("DELETE FROM Answer WHERE Question_ID = " + i + "", conn);
                    SqlCommand cmd2 = new SqlCommand("DELETE FROM Offered_Answer WHERE Question_ID = " + i + "", conn);
                    res1 = (int)cmd.ExecuteNonQuery();
                    res2 = (int)cmd1.ExecuteNonQuery();
                    res3 = (int)cmd2.ExecuteNonQuery();
                    conn.Close();
                    flag = 0;
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InsertQuestion();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DisplayQuestion();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DeleteQuestion();
        }

        private void Chck_Checked(object sender, RoutedEventArgs e)
        {

            try
            {

                for (int i = flag; i < QuestionTableToDelete.SelectedItems.Count; i++, flag++)
                {
                    DataRowView oDataRowView = QuestionTableToDelete.SelectedItems[i] as DataRowView;
                    string Value = "";


                    if (oDataRowView != null)

                    {
                        //ברגע שאני מוחק מה שבחרתי , צריך לאפס את פלאג

                        Value = oDataRowView.Row[0].ToString();
                        values.Add(Value);

                        Console.WriteLine(Value);

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

        private void Button_Click_Display_Update(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            try
            {
                conn.Open();
                SqlCommand display = new SqlCommand("SELECT Question_Text FROM Question WHERE Question_ID=" + textboxdisplay.Text + "", conn);

                SqlDataReader reader5 = display.ExecuteReader();
                {
                    if (reader5.Read())
                    {
                        quesdisplay.Text = (reader5["Question_Text"].ToString());
                    }

                }
                reader5.Close();
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

        private void Button_Click_Changch(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            int res = 0;

            try
            {
                conn.Open();
                SqlCommand display = new SqlCommand("UPDATE Question SET Question_Text ='"+textboxchangch.Text.ToString()+"' WHERE Question_ID = " + textboxdisplay.Text+"", conn);
                res = (int)display.ExecuteNonQuery();
                conn.Close();

                if (res > 0)
                    MessageBox.Show("השאלה עודכנה");
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

        private void Button_Return(object sender, RoutedEventArgs e)
        {
            LoginForm l = new LoginForm();
            l.Show();
            this.Close();
        }
    }
}
