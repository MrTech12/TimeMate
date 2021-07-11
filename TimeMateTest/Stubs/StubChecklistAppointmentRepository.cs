using Core.DTOs;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TimeMateTest.Stubs
{
    class StubChecklistAppointmentRepository : IChecklistAppointmentRepository
    {
        public void CreateTask(AppointmentDTO appointmentDTO)
        {
            if (appointmentDTO.AppointmentID == 60)
            {
                using (StreamWriter streamWriter = File.AppendText(@"C:\tmp\addAppointmentTest.txt"))
                {
                    streamWriter.WriteLine(appointmentDTO.TaskList[0].TaskName);
                }
            }
        }

        public Dictionary<int, string> GetTasks(int appointmentID)
        {
            Dictionary<int, string> taskDict = new Dictionary<int, string>();
            if (appointmentID == 14)
            {
                taskDict.Add(1, "Dit");
                taskDict.Add(2, "Dat");
                taskDict.Add(3, "Zo");
            }
            return taskDict;
        }

        public bool GetTaskStatus(int taskID)
        {
            bool taskstatus = false;
            string[] file = File.ReadAllLines(@"C:\tmp\getTaskStatusTest.txt");
            taskstatus = Convert.ToBoolean(file[2]);
            return taskstatus;
        }

        public void UpdateTaskStatus(int taskID, bool status)
        {
            string[] file = File.ReadAllLines(@"C:\tmp\getTaskStatusTest.txt");
            if (status == true && taskID != -5)
            {
                file[2] = "True";
            }
            else if (status == false && taskID != -5)
            {
                file[2] = "False";
            }
            File.WriteAllLines(@"C:\tmp\getTaskStatusTest.txt", file);
        }
    }
}
