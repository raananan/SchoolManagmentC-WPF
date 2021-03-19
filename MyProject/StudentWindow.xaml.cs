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
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        private string iduser;
        private string name;
        private string lastname;
        private string group;
        private string level;

        public StudentWindow(string IdUser,string Name,string Lastname ,string Group,string Level)
        {
            this.iduser = IdUser;
            this.name = Name;
            this.lastname = Lastname;
            this.group = Group;
            this.level = Level;

            InitializeComponent();

            combox_info_id.Content = "ת.ז".ToString() + ':' + ' ' + iduser;
            combox_info_name.Content = "שם".ToString() + ':' + ' ' + name+' '+lastname;
            combox_info_class.Content = "כיתה".ToString() + ':' + ' ' + group;
            combox_info_level.Content = "רמה".ToString() + ':' + ' ' + level;
            Header.Content = name + " ברוך שובך ";
            Header.Foreground = new SolidColorBrush(Colors.White);

        }

        public StudentWindow()
        {
                
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
           messeges.Foreground = Brushes.Yellow;
            
        }

        private void messeges_MouseLeave(object sender, MouseEventArgs e)
        {
            messeges.Foreground = Brushes.White;          
        }

        private void events_MouseEnter(object sender, MouseEventArgs e)
        {
            events.Foreground = Brushes.Yellow;
        }

        private void events_MouseLeave(object sender, MouseEventArgs e)
        {
            events.Foreground = Brushes.White;
        }

        private void exams_MouseEnter(object sender, MouseEventArgs e)
        {
            exams.Foreground = Brushes.Yellow;
        }

        private void exams_MouseLeave(object sender, MouseEventArgs e)
        {
            exams.Foreground = Brushes.White;
        }

        private void grades_MouseEnter(object sender, MouseEventArgs e)
        {
            grades.Foreground = Brushes.Yellow;
        }

        private void grades_MouseLeave(object sender, MouseEventArgs e)
        {
            grades.Foreground = Brushes.White;
        }

        private void exams_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ExamChoose exam = new ExamChoose(iduser,name,lastname,group,level);
            this.Close();
            exam.Show();

        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            info_combox.IsDropDownOpen = true;
        }

        private void messeges_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Messege m = new Messege(iduser, group);
            m.Show();
            this.Close();
        }

        private void grades_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Grades g = new Grades(iduser);
            g.Show();
            this.Close();
        }

        private void events_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Scedual s = new Scedual();
            s.Show();
            this.Close();
              
        }
    }
}
