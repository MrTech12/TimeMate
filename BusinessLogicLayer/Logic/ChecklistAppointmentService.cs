using Core.DTOs;
using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class ChecklistAppointmentService
    {
        private IChecklistAppointmentRepository _checklistAppointmentRepository;
        private IAppointmentRepository _appointmentRepository;

        public ChecklistAppointmentService(IChecklistAppointmentRepository checklistAppointmentRepository)
        {
            _checklistAppointmentRepository = checklistAppointmentRepository;
        }

        public ChecklistAppointmentService(IAppointmentRepository appointmentRepository, IChecklistAppointmentRepository checklistAppointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
            _checklistAppointmentRepository = checklistAppointmentRepository;
        }

        public void AddChecklistAppointment(AppointmentDTO appointmentDTO)
        {
            Appointment appointment = new Appointment() { AgendaID = appointmentDTO.AgendaID, AgendaName = appointmentDTO.AgendaName, AppointmentName = appointmentDTO.AppointmentName, StartDate = appointmentDTO.StartDate, EndDate = appointmentDTO.EndDate };

            appointmentDTO.AppointmentID = _appointmentRepository.CreateAppointment(appointment);

            if (appointmentDTO.TaskList.Count != 0)
            {
                List<Task> tasks = new List<Task>();
                foreach (var item in appointmentDTO.TaskList)
                {
                    tasks.Add(new Task() { AppointmentID = appointmentDTO.AppointmentID, TaskName = item.TaskName });
                }
                _checklistAppointmentRepository.CreateTask(tasks);
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
