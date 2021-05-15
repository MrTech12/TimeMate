using Model.DTO_s;
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

        public void AddChecklistAppointment(AppointmentDTO appointmentDTO)
        {
            appointmentDTO.AppointmentID = _appointmentRepository.CreateAppointment(appointmentDTO);

            if (appointmentDTO.TaskList.Count != 0)
            {
                _checklistAppointmentRepository.CreateTask(appointmentDTO);
            }
        }

        public List<string> RetrieveTasks(int appointmentID)
        {
            var tasks = _checklistAppointmentRepository.GetTasks(appointmentID);
            List<string> taskList = new List<string>();
            if (tasks.Count != 0)
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    taskList.Add(Convert.ToString(tasks[i].TaskID));
                    taskList.Add(tasks[i].TaskName);
                }
            }
            return taskList;
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
