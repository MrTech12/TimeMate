using Core.DTOs;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class ChecklistAppointment
    {
        private IChecklistAppointmentRepository _checklistAppointmentRepository;
        private IAppointmentRepository _appointmentRepository;

        public ChecklistAppointment(IChecklistAppointmentRepository checklistAppointmentRepository)
        {
            _checklistAppointmentRepository = checklistAppointmentRepository;
        }

        public ChecklistAppointment(IAppointmentRepository appointmentRepository, IChecklistAppointmentRepository checklistAppointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
            _checklistAppointmentRepository = checklistAppointmentRepository;
        }

        public void AddChecklistAppointment(AppointmentDTO appointmentDTO)
        {
            appointmentDTO.AppointmentID = _appointmentRepository.CreateAppointment(appointmentDTO);

            if (appointmentDTO.TaskList.Count != 0)
            {
                _checklistAppointmentRepository.CreateTask(appointmentDTO);
            }
        }

        public Dictionary<int, string> RetrieveTasks(int appointmentID)
        {
            return _checklistAppointmentRepository.GetTasks(appointmentID);
        }

        public void ChangeTaskStatus(int taskID)
        {
            bool taskStatus = _checklistAppointmentRepository.GetTaskStatus(taskID);
            if (!taskStatus)
            {
                _checklistAppointmentRepository.UpdateTaskStatus(taskID, true);
            }
            else
            {
                _checklistAppointmentRepository.UpdateTaskStatus(taskID, false);
            }
        }
    }
}
