using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    abstract public class Appointment
    {
        public AppointmentDTO appointmentDTO;

        public Appointment(AppointmentDTO appointmentDTO)
        {
            this.appointmentDTO = appointmentDTO;
        }

        public abstract void RenameAppointment(AppointmentDTO appointmentDTO);
    }
}
