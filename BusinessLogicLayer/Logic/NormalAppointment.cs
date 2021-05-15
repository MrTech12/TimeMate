using Model.DTO_s;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class NormalAppointment
    {
        private INormalAppointmentRepository _normalAppointmentRepository;
        private IAppointmentRepository _appointmentRepository;

        public NormalAppointment(INormalAppointmentRepository normalAppointmentRepository)
        {
            _normalAppointmentRepository = normalAppointmentRepository;
        }

        public NormalAppointment(IAppointmentRepository appointmentRepository, INormalAppointmentRepository normalAppointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
            _normalAppointmentRepository = normalAppointmentRepository;
        }

        public void AddNormalAppointment(AppointmentDTO appointmentDTO)
        {
            appointmentDTO.AppointmentID = _appointmentRepository.CreateAppointment(appointmentDTO);

            if (appointmentDTO.DescriptionDTO.Description != null)
            {
                _normalAppointmentRepository.CreateDescription(appointmentDTO);
            }
        }

        public string RetrieveDescription(int appointmentID)
        {
            return _normalAppointmentRepository.GetDescription(appointmentID);
        }
    }
}
