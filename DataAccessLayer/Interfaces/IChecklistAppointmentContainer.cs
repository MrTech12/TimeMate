using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IChecklistAppointmentContainer
    {
        void AddTask(AppointmentDTO appointmentDTO);

        bool GetTaskStatus(int taskID);

        List<ChecklistDTO> GetTasks(int appointmentID);

        void CheckOffTask(int taskID, bool status);
    }
}
