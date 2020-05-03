using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    abstract public class AppointmentLogic
    {
        public IAgendaContext agendaContext;
        public AccountDTO accountDTO;
        public AppointmentDTO appointmentDTO;

        public AppointmentLogic(AppointmentDTO appointmentDTOInput, IAgendaContext agendaContextInput)
        {
            this.appointmentDTO = appointmentDTOInput;
            this.agendaContext = agendaContextInput;
        }

        public abstract void DeleteAppointment(string appointmentName);

        public abstract void RenameAppointment(string appointmentName);
    }
}
