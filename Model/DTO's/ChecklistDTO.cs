using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO_s
{
    public class ChecklistDTO
    {
        private int taskID;
        private int appointmentID;
        private string taskName;
        private bool taskChecked;

        public int TaskID
        {
            get { return this.taskID; }
            set { this.taskID = value; }
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
