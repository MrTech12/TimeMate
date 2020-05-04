using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class NormalAppointmentLogic : AppointmentLogic
    {
        private INormalAppointmentContext NAppointmentContext;
        private string messageToUser;

        public NormalAppointmentLogic(IAgendaContext agendaContextInput, AppointmentDTO appointmentDTOInput, INormalAppointmentContext normalAppointmentContextInput) : base(appointmentDTOInput, agendaContextInput)
        {
            this.NAppointmentContext = normalAppointmentContextInput;
        }

        public override void DeleteAppointment(string appointmentName)
        {
            AgendaDTO agendaDTO = new AgendaDTO();
            agendaDTO.AgendaID = AgendaContext.GetAgendaID(appointmentName, accountDTO);
            appointmentDTO.AppointmentID = NAppointmentContext.GetNormalAppointmentID(appointmentDTO, agendaDTO.AgendaID);
            NAppointmentContext.DeleteNormalAppointment(appointmentDTO.AppointmentID, agendaDTO.AgendaID);
        }

        public override void RenameAppointment(string appointmentName)
        {
            throw new NotImplementedException();
        }
    }
}
