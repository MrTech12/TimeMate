using Model.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IChecklistAppointmentRepository
    {
        void AddTask(AppointmentDTO appointmentDTO);

        List<ChecklistDTO> GetTasks(int appointmentID);

        bool GetTaskStatus(int taskID);

        void CheckOffTask(int taskID, bool status);
    }
}
