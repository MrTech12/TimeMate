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

        void RevertCheckOffTask(int taskID);

        List<ChecklistDTO> GetTasks(AppointmentDTO appointmentDTO);

        void CheckOffTask(int taskID);
    }
}
