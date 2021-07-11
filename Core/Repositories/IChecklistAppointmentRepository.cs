using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
    public interface IChecklistAppointmentRepository
    {
        void CreateTask(AppointmentDTO appointmentDTO);

        Dictionary<int, string> GetTasks(int appointmentID);

        bool GetTaskStatus(int taskID);

        void UpdateTaskStatus(int taskID, bool status);
    }
}