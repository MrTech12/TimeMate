using Core.DTOs;
using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class NormalAppointmentService
    {
        private INormalAppointmentRepository _normalAppointmentRepository;
        private IAppointmentRepository _appointmentRepository;

        public NormalAppointmentService(INormalAppointmentRepository normalAppointmentRepository)
        {
            _normalAppointmentRepository = normalAppointmentRepository;
        }

        public NormalAppointmentService(IAppointmentRepository appointmentRepository, INormalAppointmentRepository normalAppointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
            _normalAppointmentRepository = normalAppointmentRepository;
        }

        public void AddNormalAppointment(AppointmentDTO appointmentDTO)
        {
            Appointment appointment = new Appointment() { AgendaID = appointmentDTO.AgendaID, AgendaName = appointmentDTO.AgendaName, AppointmentName = appointmentDTO.AppointmentName, StartDate = appointmentDTO.StartDate, EndDate = appointmentDTO.EndDate };

            appointmentDTO.AppointmentID = _appointmentRepository.CreateAppointment(appointment);

            if (appointmentDTO.DescriptionDTO.Description != null)
            {
                Description description = new Description() { AppointmentID = appointmentDTO.AppointmentID, DescriptionName = appointmentDTO.DescriptionDTO.Description};
                _normalAppointmentRepository.CreateDescription(description);
            }
        }

        public string RetrieveDescription(int appointmentID)
        {
            return _normalAppointmentRepository.GetDescription(appointmentID);
        }
    }
}
