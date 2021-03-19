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

namespace CalenderProj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Dictionary<DateTime, Day> allDays = new Dictionary<DateTime, Day>();

        public MainWindow()
        {
            InitializeComponent();
            myCalender.SelectedDate = DateTime.Today;
        }
        
        private void myCalender_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            rubrikPanel.Visibility = Visibility.Hidden;
            DateTime date = (DateTime)myCalender.SelectedDate;
            if (allDays.ContainsKey(date))
                myTxt.Text = allDays[date].printEvents();   
            else
                myTxt.Text = "No events on this date";
            eventTxt.Text = date.Date.ToString();
            EditTxt.Text = date.Date.ToString();
        }

        private void addEvent_Click(object sender, RoutedEventArgs e)
        {
            rubrikPanel.Visibility = Visibility.Visible;
        }

        private void removeEvent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void eventCommit_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = (DateTime)myCalender.SelectedDate;
            Day day;
            Thing thing = new Thing(studentTB.Text, teacherTB.Text, date, titleTB.Text, partnerTB.Text, locationTB.Text);
            if (!allDays.ContainsKey(date))
            {
                day = new Day(date);
                allDays.Add(date, day);
                day.addThing(thing);
            }
            else
            {
                allDays[date].addThing(thing);
            }
            rubrikPanel.Visibility = Visibility.Hidden;
            myTxt.Text = allDays[date].printEvents();
        }
    }
}
