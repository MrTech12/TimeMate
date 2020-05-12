using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    abstract public class Appointment
    {
        public IAgendaContext _agendaContext;
        public AccountDTO accountDTO;
        public AppointmentDTO appointmentDTO;

        public Appointment(AppointmentDTO appointmentDTO, IAgendaContext agendaContext)
        {
            this.appointmentDTO = appointmentDTO;
            this._agendaContext = agendaContext;
        }

        public abstract void DeleteAppointment(string appointmentName);

        public abstract void RenameAppointment(string appointmentName);
    }
}
