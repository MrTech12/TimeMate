using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class ChecklistAppointment : Appointment
    {
        private IChecklistAppointmentContainer _cAppointmentContext;

        public ChecklistAppointment(AppointmentDTO appointmentDTO, IChecklistAppointmentContainer checklistAppointmentContext) : base(appointmentDTO)
        {
            this._cAppointmentContext = checklistAppointmentContext;
        }

        public List<string> RetrieveTasks(AppointmentDTO appointmentDTO)
        {
            var checklists = _cAppointmentContext.GetTasks(appointmentDTO);
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
            bool taskStatus = _cAppointmentContext.GetTaskStatus(taskID);
            if (!taskStatus)
            {
                _cAppointmentContext.CheckOffTask(taskID);
            }
            else
            {
                _cAppointmentContext.RevertCheckOffTask(taskID);
            }
        }

        /// <summary>
        /// Rename an appointment.
        /// </summary>
        public override void RenameAppointment(AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }
    }
}
