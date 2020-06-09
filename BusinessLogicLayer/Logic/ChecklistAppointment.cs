using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class ChecklistAppointment
    {
        private IChecklistAppointmentContainer _checklistAppointmentContext;

        public ChecklistAppointment(IChecklistAppointmentContainer checklistAppointmentContext)
        {
            this._checklistAppointmentContext = checklistAppointmentContext;
        }

        public List<string> RetrieveTasks(int appointmentID)
        {
            var checklists = _checklistAppointmentContext.GetTasks(appointmentID);
            List<string> tasks = new List<string>();
            for (int i = 0; i < checklists.Count; i++)
            {
                tasks.Add(Convert.ToString(checklists[i].TaskID));
                tasks.Add(Convert.ToString(checklists[i].TaskName));
            }
            return tasks;
        }

        public void ChangeTaskStatus(int taskID)
        {
            bool taskStatus = _checklistAppointmentContext.GetTaskStatus(taskID);
            if (!taskStatus)
            {
                _checklistAppointmentContext.CheckOffTask(taskID);
            }
            else
            {
                _checklistAppointmentContext.RevertCheckOffTask(taskID);
            }
        }
    }
}
