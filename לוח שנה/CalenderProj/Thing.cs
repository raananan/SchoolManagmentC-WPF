using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalenderProj
{
    public class Thing
    {
        public string studentID;
        public string teacherID;
        public DateTime time;
        public string title;
        public string partner;
        public string location;

        public Thing(string studentID, string teacherID, DateTime time, string title, string partner, string location)
        {
            this.studentID = studentID;
            this.teacherID = teacherID;
            this.time = time;
            this.title = title;
            this.partner = partner;
            this.location = location;
        }

        public override string ToString()
        {
            string print = "Student ID: " + studentID + "\nTeacher ID: " + teacherID + "\nTime: " + time +
                "\nTitle: " + title + "\nPartner: " + partner + "\nLocation: " + location;
            return print;
        }
    }
    
}
