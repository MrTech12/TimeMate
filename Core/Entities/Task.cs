using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Task
    {
        public int TaskID { get; set; }

        public int AppointmentID { get; set; }

        public string TaskName { get; set; }

        public bool TaskChecked { get; set; }
    }
}
