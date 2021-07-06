using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO_s
{
    public class AgendaDTO
    {
        private int agendaID;
        private string agendaName;
        private string agendaColor;

        public int AgendaID { get { return this.agendaID; } set { this.agendaID = value; } }

        public string AgendaName { get { return this.agendaName; } set { this.agendaName = value; } }

        public string AgendaColor { get { return this.agendaColor; } set { this.agendaColor = value; } }
    }
}
