using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class NormalAppointment : Appointment
    {
        private INormalAppointmentContext _nAppointmentContext;
        private string messageToUser;

        public NormalAppointment(IAgendaContext agendaContextInput, AppointmentDTO appointmentDTOInput, INormalAppointmentContext normalAppointmentContextInput) : base(appointmentDTOInput, agendaContextInput)
        {
            this._nAppointmentContext = normalAppointmentContextInput;
        }

        public override void DeleteAppointment(string appointmentName)
        {
            AgendaDTO agendaDTO = new AgendaDTO();
            agendaDTO.AgendaID = _agendaContext.GetAgendaID(appointmentName, accountDTO);
            appointmentDTO.AppointmentID = _nAppointmentContext.GetNormalAppointmentID(appointmentDTO, agendaDTO.AgendaID);
            _nAppointmentContext.DeleteNormalAppointment(appointmentDTO.AppointmentID, agendaDTO.AgendaID);
        }

        public override void RenameAppointment(string appointmentName)
        {
            throw new NotImplementedException();
        }
    }
}
