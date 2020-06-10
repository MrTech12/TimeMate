using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubChecklistAppointmentContext : IChecklistAppointmentContainer
    {
        public void AddTask(AppointmentDTO appointmentDTO)
        {
            if (appointmentDTO.AppointmentID == 60)
            {
                using (StreamWriter streamWriter = File.AppendText(@"C:\tmp\addAppointmentTest.txt"))
                {
                    streamWriter.WriteLine(appointmentDTO.ChecklistDTOs[0].TaskName);
                }
            }
        }

        public List<ChecklistDTO> GetTasks(int appointmentID)
        {
            List<ChecklistDTO> checklists = new List<ChecklistDTO>();

            if (appointmentID == 14)
            {
                ChecklistDTO checklist1 = new ChecklistDTO();
                checklist1.AppointmentID = 14;
                checklist1.TaskID = 1;
                checklist1.TaskName = "Dit";
                checklists.Add(checklist1);

                ChecklistDTO checklist2 = new ChecklistDTO();
                checklist2.AppointmentID = 14;
                checklist2.TaskID = 2;
                checklist2.TaskName = "Dat";
                checklists.Add(checklist2);

                ChecklistDTO checklist3 = new ChecklistDTO();
                checklist3.AppointmentID = 14;
                checklist3.TaskID = 3;
                checklist3.TaskName = "Zo";
                checklists.Add(checklist3);

            }
            return checklists;
        }

        public bool GetTaskStatus(int taskID)
        {
            bool taskstatus = false;
            string[] file = File.ReadAllLines(@"C:\tmp\getTaskStatusTest.txt");
            taskstatus = Convert.ToBoolean(file[2]);
            return taskstatus;
        }

        public void CheckOffTask(int taskID, bool status)
        {
            string[] file = File.ReadAllLines(@"C:\tmp\getTaskStatusTest.txt");
            if (status == true)
            {
                file[2] = "True";
            }
            else
            {
                file[2] = "False";
            }
            File.WriteAllLines(@"C:\tmp\getTaskStatusTest.txt", file);
        }
    }
}
