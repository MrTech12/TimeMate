using Model.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IChecklistAppointmentRepository
    {
        void CreateTask(AppointmentDTO appointmentDTO);

        Dictionary<int, string> GetTasks(int appointmentID);

        bool GetTaskStatus(int taskID);

        void UpdateTaskStatus(int taskID, bool status);
    }
}
