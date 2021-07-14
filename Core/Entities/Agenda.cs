using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Agenda
    {
        public int AgendaID { get; set; }

        public int AccountID { get; set; }

        public string AgendaName { get; set; }

        public string AgendaColor { get; set; }

        public bool IsWorkAgenda { get; set; }
    }
}
