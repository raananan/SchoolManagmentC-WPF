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
    /// Interaction logic for ExamChoose.xaml
    /// </summary>
    public partial class ExamChoose : Window
    {
        private string user_id;
        private string name;
        private string lastname;
        private string group;
        private string level;
       
        public ExamChoose(string User_Id, string Name,string LastName, string Group, string Level)
        {
            this.user_id = User_Id;
            this.name = Name;
            this.lastname = LastName;
            this.group = Group;
            this.level = Level;

            InitializeComponent();
            
        }
   
        
         
private void btn_math_Click(object sender, RoutedEventArgs e)
        {
            questionnaire q = new questionnaire(1,user_id,name,lastname,group,level);         
            q.Show();
            this.Close();           
        }

        private void btn_english_Click(object sender, RoutedEventArgs e)
        {
            questionnaire q = new questionnaire(2, user_id, name, lastname, group, level);
            q.Show();
            this.Close();
        }

        private void btn_bible_Click(object sender, RoutedEventArgs e)
        {
            questionnaire q = new questionnaire(3, user_id, name, lastname, group, level);
            q.Show();
            this.Close();
        }

        private void btn_history_Click(object sender, RoutedEventArgs e)
        {
            questionnaire q = new questionnaire(4, user_id, name, lastname, group, level);
            q.Show();
            this.Close();
        }

        private void btn_programmer_Click(object sender, RoutedEventArgs e)
        {
            questionnaire q = new questionnaire(5, user_id, name, lastname, group, level);
            q.Show();
            this.Close();
        }

        private void btn_chymesty_Click(object sender, RoutedEventArgs e)
        {
            questionnaire q = new questionnaire(6, user_id, name, lastname, group, level);
            q.Show();
            this.Close();
        }
        
    }
}
