using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DTO
{
    public class AgendaDTO
    {
        private int agendaID;
        private string agendaName;
        private string agendaColor;
        private string notification;

        public int AgendaID { get { return this.agendaID; } set { this.agendaID = value; } }

        public string AgendaName { get { return this.agendaName; } set { this.agendaName = value; } }

        public string AgendaColor { get { return this.agendaColor; } set { this.agendaColor = value; } }

        public string Notification { get { return this.notification; } set { this.notification = value; } }
    }
}
