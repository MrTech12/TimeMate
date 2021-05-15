using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Model.DTO_s;
using DataAccessLayer.Interfaces;

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

        public List<TaskDTO> GetTasks(int appointmentID)
        {
            List<TaskDTO> tasklist = new List<TaskDTO>();
            if (appointmentID == 14)
            {
                TaskDTO taskDTO1 = new TaskDTO();
                taskDTO1.AppointmentID = 14;
                taskDTO1.TaskID = 1;
                taskDTO1.TaskName = "Dit";
                tasklist.Add(taskDTO1);

                TaskDTO taskDTO2 = new TaskDTO();
                taskDTO2.AppointmentID = 14;
                taskDTO2.TaskID = 2;
                taskDTO2.TaskName = "Dat";
                tasklist.Add(taskDTO2);

                TaskDTO taskDTO3 = new TaskDTO();
                taskDTO3.AppointmentID = 14;
                taskDTO3.TaskID = 3;
                taskDTO3.TaskName = "Zo";
                tasklist.Add(taskDTO3);
            }
            return tasklist;
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
