using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class AgendaDTO
    {
        private int agendaID;
        private string agendaName;
        private string agendaColor;
        private bool isWorkAgenda;

        public int AgendaID { get { return this.agendaID; } set { this.agendaID = value; } }

        public string AgendaName { get { return this.agendaName; } set { this.agendaName = value; } }

        public string AgendaColor { get { return this.agendaColor; } set { this.agendaColor = value; } }

        public bool IsWorkAgenda { get { return this.isWorkAgenda; } set { this.isWorkAgenda = value; } }
    }
}