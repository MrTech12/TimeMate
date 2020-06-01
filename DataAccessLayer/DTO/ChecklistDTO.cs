using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DTO
{
    public class ChecklistDTO
    {
        private int id;
        private int appointmentID;
        private string taskName;
        private bool taskChecked;

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public int AppointmentID
        {
            get { return this.appointmentID; }
            set { this.appointmentID = value; }
        }

        public string TaskName
        {
            get { return this.taskName; }
            set { this.taskName = value; }
        }

        public bool TaskChecked
        {
            get { return this.taskChecked; }
            set { this.taskChecked = value; }
        }
    }
}
