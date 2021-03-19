using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalenderProj
{
    public class Day
    {
        public DateTime date;
        public List<Thing> eventsList;

        public Day(DateTime date)
        {
            this.date = date.Date;
            eventsList = new List<Thing>();
        }

        public void addThing(Thing thing)
        {
            eventsList.Add(thing);
        }

        public void deleteThing(Thing thing)
        {
            eventsList.Remove(thing);
        }

        public string printEvents()
        {
            Day day = this;
            string str = "";
            for (int i = 0; i < day.eventsList.Count; i++)
            {
                str += day.eventsList[i].ToString() + "\n";
            }
            str += "\n\n";
            return str;
        }
    }
}
