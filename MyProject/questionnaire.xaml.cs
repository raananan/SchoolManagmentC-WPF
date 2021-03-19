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
    /// Interaction logic for questionnaire.xaml
    /// </summary>
    public partial class questionnaire : Window

    {

        string[] Quesions_List = new string[40];       
        static int i = 0;
            static int j = 1;
            static int score = 0;//נקודות
        static int count = 0;//פרמטר שאני שולח לפונקציה כדי לעבור בין השאלות
        static int count_question=0;//אינדקס במערך של שאלות
        static int quesion_number = 1;//מספר השאלה במבחן
        static int count_offered_answer = 0;
        static int count_answer = 0;

        private int subject;
        private string id_user;
        private string name;
        private string lastname;
        private string group;
        private string level;
        

        public questionnaire(int Subject, string Id_User, string Name, string Lastname, string Group, string Level)
            {
            this.subject = Subject;
            this.id_user = Id_User;
            this.name = Name;
            this.lastname = Lastname;
            this.group = Group;
            this.level = Level;
           
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            SqlCommand com = new SqlCommand("SELECT *FROM  Question  WHERE Level_ID="+level+" AND Subject_ID="+subject+"", conn);
            try
            {
                conn.Open();
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Quesions_List[i++] = (reader["Question_ID"].ToString());
                        if (i == 10)
                            break;
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
        


            InitializeComponent();
                Question(i);
                Offered_Answers(i);
            }
       

          
            
            public void Question(int question_id)
            {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            string TheQuestion=null;
           
            question_header.Content = quesion_number++ + " שאלה ";
         
         
                try
                {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT  Question_Text FROM Question WHERE Question_ID="+ Quesions_List[count_question++]+"",conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TheQuestion = (reader["Question_Text"].ToString());                    
                    }
                   
                }
                conn.Close();

                Ques.Text = TheQuestion;
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

        public void Offered_Answers(int subject_id)
        {
            int number;
            
             SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            SqlCommand com2 = new SqlCommand("SELECT *FROM  Offered_Answer  WHERE  Question_ID =" + Quesions_List[count_offered_answer++] + "", conn);
            string[] answers = new string[10];
            int i = 0;

            try
            {
                conn.Open();
                using (SqlDataReader reader = com2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        answers[i++] = (reader["Offered_Answer_Text"].ToString());
                    }
                }
                i = 0;
                var rand = new Random();
                int[] rnd ={ 5,5,5,5 };
                
                while (i<4)
                {
                    number = rand.Next(0, 4);
                    if(rnd.Contains(number))
                    {
                       
                        continue;
                    }
                    rnd[i++] = number;
                }
                i = 0;
                btn1.Content = answers[rnd[i++]];
                btn2.Content = answers[rnd[i++]];
                btn3.Content = answers[rnd[i++]];
                btn4.Content = answers[rnd[i++]];
            
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

        public string iscorrect()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
            SqlCommand com3 = new SqlCommand
            ("select *from Offered_Answer join Answer on Offered_Answer.Offered_Answer_ID=Answer.Offered_Answer_ID where Answer.Question_ID="+Quesions_List[count_answer++]+"", conn);
            string correctans = null;

            int flag = 0;
            try
            {
                conn.Open();
                using (SqlDataReader reader = com3.ExecuteReader())
                {
                    while (reader.Read() && flag < j)
                    {
                        correctans = (reader["Offered_Answer_Text"].ToString());
                        flag++;
                    }
                    j++;
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
            conn.Close();
            return correctans;
        }

        public void ExamEnd(int flag)
            {
            int points=0;
                if (flag == 10)
                {
                    buttons.Visibility = Visibility.Hidden;
                    Ques.Visibility = Visibility.Hidden;
                    question_header.Height = 350;
                    question_header.FontSize = 35;
                    question_header.HorizontalContentAlignment = HorizontalAlignment.Right;
                    question_header.Background = new SolidColorBrush(Colors.Azure);
                    question_header.Content = "סיום מבחן\n :תוצאה\n ענית נכון על\n " + score + '\n' + "שאלות";

                Button btn = new Button();
                btn.Height = 50;
                btn.Width = 150;
                btn.Click += btn_Click;
                Thickness margin = btn.Margin;
                margin.Bottom = -200;
                btn.Margin = margin;
                grid.Children.Add(btn);
                btn.Content = "חזרה למסך הראשי";
                quesion_number = 1;
                j = 1;
                score = 0;
                count = 0;
                i = 0;
                count_question = 0;
                count_offered_answer = 0;
                count_answer = 0;

                SqlConnection cmd = new SqlConnection();
                cmd.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
                cmd.Open();
                SqlCommand gradeinsert = new SqlCommand("INSERT INTO Grades(Grade,User_Id)values(" + score * 10 + "," + id_user + ")", cmd);
               // int res = (int)gradeinsert.ExecuteReader();
                cmd.Close();
            }

          

            if (score >= 8)
                {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyTable"].ConnectionString;
                conn.Open();
                SqlCommand com = new SqlCommand("UPDATE Students SET Score+=1 WHERE Student_ID=" + id_user, conn);
                SqlCommand com2 = new SqlCommand("SELECT Score FROM Students WHERE Student_ID=" + id_user, conn);
                try
                {                   
                    using (SqlDataReader reader = com2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            points = ((int)reader["Score"]);
                        }
                    }
                    SqlCommand com3 = new SqlCommand("UPDATE Students SET Level_ID="+score/3+" WHERE Student_ID=" + id_user, conn);
                    int resultcom3 = (int)com3.ExecuteScalar();
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
            }
       

        private void btn1_MouseEnter(object sender, MouseEventArgs e)
            {
                btn1.Background = new SolidColorBrush(Colors.Blue);
            }

            private void btn1_MouseLeave(object sender, MouseEventArgs e)
            {
                btn1.Background = new SolidColorBrush(Colors.DodgerBlue);
            }
        
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            StudentWindow s = new StudentWindow(id_user, name, lastname, group, level);
            s.Show();
            this.Close();
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
            {
                if (btn1.Content.ToString() == iscorrect())
                {
                    MessageBox.Show("יפה מאוד");
                    score += 1;
                }
                else
                {
                    MessageBox.Show("תשובה שגויה");
                }
            count++;
                Question(count);
                Offered_Answers(count);
                ExamEnd(count);
            }
       
        private void btn2_Click(object sender, RoutedEventArgs e)
            {
                if (btn2.Content.ToString() == iscorrect())
                {
                    MessageBox.Show("יפה מאוד");
                    score += 1;
                }
                else
                {
                    MessageBox.Show("תשובה שגויה");
                }
            count++;
                Question(count);
                Offered_Answers(count);
                ExamEnd(count);
            }

            private void btn3_Click(object sender, RoutedEventArgs e)
            {
                if (btn3.Content.ToString() == iscorrect())
                {
                    MessageBox.Show("יפה מאוד");
                    score += 1;
                }
                else
                {
                    MessageBox.Show("תשובה שגויה");
                }
            count++;
                Question(count);
                Offered_Answers(count);
                ExamEnd(count);
            }

            private void btn4_Click(object sender, RoutedEventArgs e)
            {
                if (btn4.Content.ToString() == iscorrect())
                {
                    MessageBox.Show("יפה מאוד");
                    score += 1;
                }
                else
                {
                    MessageBox.Show("תשובה שגויה");
                }
            count++;
                Question(count);
                Offered_Answers(count);
                ExamEnd(count);
            }
        }
    }
