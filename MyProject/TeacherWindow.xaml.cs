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
using System.Windows.Shapes;

namespace MyProject
{
    /// <summary>
    /// Interaction logic for TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        private string idteacher;
        private string groupteacher;

        public TeacherWindow(string Idteacher,string Groupteacher)
        {
            this.idteacher = Idteacher;
            this.groupteacher = Idteacher;

            InitializeComponent();
        }

        private void Button_Add_Student(object sender, RoutedEventArgs e)
        {
            StudentTeacherControl t = new StudentTeacherControl(idteacher,groupteacher);
            t.Show();
            this.Close();
        }

        private void Creat_Questionire(object sender, RoutedEventArgs e)
        {
            ExamCreate new_ex = new ExamCreate();
            new_ex.Show();
            this.Close();
           
        }

        private void Messege_Click(object sender, RoutedEventArgs e)
        {
            Messege m = new Messege(idteacher,groupteacher);
            m.Show();
            this.Close();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
