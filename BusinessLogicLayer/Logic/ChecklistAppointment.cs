using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
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

        public void CreateChecklistAppointment(AppointmentDTO appointmentDTO)
        {
            appointmentDTO.AppointmentID = _appointmentRepository.AddAppointment(appointmentDTO);

            if (appointmentDTO.ChecklistDTOs.Count != 0)
            {
                _checklistAppointmentRepository.AddTask(appointmentDTO);
            }
        }

        public List<string> RetrieveTasks(int appointmentID)
        {
            var checklists = _checklistAppointmentRepository.GetTasks(appointmentID);
            List<string> tasks = new List<string>();
            if (checklists.Count != 0)
            {
                for (int i = 0; i < checklists.Count; i++)
                {
                    tasks.Add(Convert.ToString(checklists[i].TaskID));
                    tasks.Add(checklists[i].TaskName);
                }
            }
            return tasks;
        }

        public void ChangeTaskStatus(int taskID)
        {
            bool taskStatus = _checklistAppointmentRepository.GetTaskStatus(taskID);
            if (!taskStatus)
            {
                _checklistAppointmentRepository.CheckOffTask(taskID, true);
            }
            else
            {
                _checklistAppointmentRepository.CheckOffTask(taskID, false);
            }
        }
    }
}
