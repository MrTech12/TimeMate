using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class ChecklistAppointment
    {
        private IChecklistAppointmentContainer _checklistAppointmentContainer;

        public ChecklistAppointment(IChecklistAppointmentContainer checklistAppointmentContainer)
        {
            this._checklistAppointmentContainer = checklistAppointmentContainer;
        }

        public List<string> RetrieveTasks(int appointmentID)
        {
            var checklists = _checklistAppointmentContainer.GetTasks(appointmentID);
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
            bool taskStatus = _checklistAppointmentContainer.GetTaskStatus(taskID);
            if (!taskStatus)
            {
                _checklistAppointmentContainer.CheckOffTask(taskID, true);
            }
            else
            {
                _checklistAppointmentContainer.CheckOffTask(taskID, false);
            }
        }
    }
}
