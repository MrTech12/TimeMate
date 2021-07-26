using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Appointment
    {
        public int AppointmentID { get; set;}
        public string AppointmentName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AgendaID { get; set; }
        public string AgendaName { get; set; }

        public Description Description { get; set; } = new Description();

        public List<Task> TaskList { get; set; } = new List<Task>();
    }
}
