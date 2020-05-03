﻿using System;
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
        private string agendaName;
        private string description;
        private List<string> checklistItemName;
        private List<bool> checklistItemChecked;

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
        public List<string> ChecklistItemName
        {
            get { return this.checklistItemName; }
            set { this.checklistItemName = value; }
        }

        public List<bool> ChecklistItemChecked
        {
            get { return this.checklistItemChecked; }
            set { this.checklistItemChecked = value; }
        }
    }
}