using BusinessLogicLayer.Logic;
using Core.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class ChecklistAppointmentTest
    {
        private ChecklistAppointmentService checklistAppointmentService;
        private AppointmentDTO appointmentDTO;
        string filePathtasks = @"C:\tmp\getTaskStatusTest.txt";

        [Fact]
        public void CreateChecklistAppointment()
        {
            TaskDTO taskDTO = new TaskDTO() { TaskName = "Get inspiration" };
            appointmentDTO = new AppointmentDTO();
            appointmentDTO.AppointmentName = "Create 3D render";
            appointmentDTO.StartDate = DateTime.Now.AddHours(3);
            appointmentDTO.EndDate = DateTime.Now.AddHours(4);
            appointmentDTO.AgendaName = "Firefox";
            appointmentDTO.TaskList.Add(taskDTO);
            checklistAppointmentService = new ChecklistAppointmentService(new StubAppointmentRepository(), new StubChecklistAppointmentRepository());

            checklistAppointmentService.AddChecklistAppointment(appointmentDTO);

            string[] appointmentFile = File.ReadAllLines(@"C:\tmp\addAppointmentTest.txt");
            File.Delete(filePathtasks);

            Assert.Contains("Create 3D render", appointmentFile[0]);
            Assert.Contains("Get inspiration", appointmentFile[4]);
            Assert.True(appointmentFile.Length == 5);
        }

        [Fact]
        public void GetTasks()
        {
            int appointmentID = 14;
            checklistAppointmentService = new ChecklistAppointmentService(new StubChecklistAppointmentRepository());

            Dictionary<int, string> output = checklistAppointmentService.RetrieveTasks(appointmentID);

            Assert.Equal("Dit", output[1]);
            Assert.Equal("Dat", output[2]);
            Assert.Equal("Zo", output[3]);
            Assert.True(output.Count == 3);
        }

        [Fact]
        public void GetNoTasks()
        {
            int appointmentID = 0;
            checklistAppointmentService = new ChecklistAppointmentService(new StubChecklistAppointmentRepository());

            Dictionary<int, string> output = checklistAppointmentService.RetrieveTasks(appointmentID);

            Assert.True(output.Count == 0);
        }

        [Fact]
        public void ChangeTaskStatusToDone()
        {
            TaskDTO taskDTO = new TaskDTO();
            taskDTO.TaskID = 62;
            taskDTO.TaskName = "Get cake";
            checklistAppointmentService = new ChecklistAppointmentService(new StubChecklistAppointmentRepository());

            using (StreamWriter streamWriter = new StreamWriter(filePathtasks))
            {
                streamWriter.WriteLine(taskDTO.TaskID);
                streamWriter.WriteLine(taskDTO.TaskName);
                streamWriter.WriteLine("False");
            }

            checklistAppointmentService.ChangeTaskStatus(62);

            string[] file = File.ReadAllLines(filePathtasks);
            File.Delete(filePathtasks);

            Assert.Equal("True", file[2]);
        }

        [Fact]
        public void ChangeTaskStatusToNotDone()
        {
            TaskDTO taskDTO = new TaskDTO();
            taskDTO.TaskID = 74;
            taskDTO.TaskName = "Buy new monitor";
            checklistAppointmentService = new ChecklistAppointmentService(new StubChecklistAppointmentRepository());

            using (StreamWriter streamWriter = new StreamWriter(filePathtasks))
            {
                streamWriter.WriteLine(taskDTO.TaskID);
                streamWriter.WriteLine(taskDTO.TaskName);
                streamWriter.WriteLine("True");
            }

            checklistAppointmentService.ChangeTaskStatus(74);

            string[] file = File.ReadAllLines(filePathtasks);
            File.Delete(filePathtasks);

            Assert.Equal("False", file[2]);
        }

        [Fact]
        public void NoTaskStatusChange()
        {
            TaskDTO taskDTO = new TaskDTO();
            taskDTO.TaskID = 28;
            taskDTO.TaskName = "Bake a cake";
            checklistAppointmentService = new ChecklistAppointmentService(new StubChecklistAppointmentRepository());

            using (StreamWriter streamWriter = new StreamWriter(filePathtasks))
            {
                streamWriter.WriteLine(taskDTO.TaskID);
                streamWriter.WriteLine(taskDTO.TaskName);
                streamWriter.WriteLine("False");
            }

            checklistAppointmentService.ChangeTaskStatus(-5);

            string[] file = File.ReadAllLines(filePathtasks);
            File.Delete(filePathtasks);

            Assert.Equal("False", file[2]);
        }
    }
}
