using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DTO
{
    public class AppointmentDTO
    {
        private int appointmentID;
        private string appointmentName;
        private DateTime startDate;
        private DateTime endDate;
        private int agendaID;
        private string agendaName;
        private string description;

        public int AppointmentID
        {
            get { return this.appointmentID; }
            set { this.appointmentID = value; }
        }

        public string AppointmentName
        {
            get { return this.appointmentName; }
            set { this.appointmentName = value; }
        }
        public DateTime StartDate
        {
            get { return this.startDate; }
            set { this.startDate = value; }
        }
        public DateTime EndDate
        {
            get { return this.endDate; }
            set { this.endDate = value; }
        }
        public int AgendaID
        {
            get { return this.agendaID; }
            set { this.agendaID = value; }
        }
        public string AgendaName
        {
            get { return this.agendaName; }
            set { this.agendaName = value; }
        }
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public List<ChecklistDTO> ChecklistDTOs { get; set; } = new List<ChecklistDTO>();
    }
}
