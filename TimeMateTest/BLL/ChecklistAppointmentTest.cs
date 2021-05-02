using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class ChecklistAppointmentTest
    {
        private ChecklistAppointment checklistAppointment;
        string filePath = @"C:\tmp\getTaskStatusTest.txt";

        [Fact]
        public void GetTasks()
        {
            int appointmentID = 14;
            checklistAppointment = new ChecklistAppointment(new StubChecklistAppointmentRepository());

            List<string> output = checklistAppointment.RetrieveTasks(appointmentID);

            Assert.Equal("Dit", output[1]);
            Assert.Equal("Dat", output[3]);
            Assert.Equal("Zo", output[5]);
            Assert.True(output.Count == 6);
        }

        [Fact]
        public void GetNoTasks()
        {
            int appointmentID = 0;
            checklistAppointment = new ChecklistAppointment(new StubChecklistAppointmentRepository());

            List<string> output = checklistAppointment.RetrieveTasks(appointmentID);

            Assert.True(output.Count == 0);
        }

        [Fact]
        public void ChangeTaskStatusToDone()
        {
            ChecklistDTO checklistDTO = new ChecklistDTO();
            checklistDTO.TaskID = 62;
            checklistDTO.TaskName = "Get cake";
            checklistAppointment = new ChecklistAppointment(new StubChecklistAppointmentRepository());

            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(checklistDTO.TaskID);
                streamWriter.WriteLine(checklistDTO.TaskName);
                streamWriter.WriteLine("False");
            }

            checklistAppointment.ChangeTaskStatus(62);

            string[] file = File.ReadAllLines(filePath);
            File.Delete(filePath);

            Assert.Equal("True", file[2]);
        }

        [Fact]
        public void ChangeTaskStatusToNotDone()
        {
            ChecklistDTO checklistDTO = new ChecklistDTO();
            checklistDTO.TaskID = 74;
            checklistDTO.TaskName = "Buy new monitor";
            checklistAppointment = new ChecklistAppointment(new StubChecklistAppointmentRepository());

            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(checklistDTO.TaskID);
                streamWriter.WriteLine(checklistDTO.TaskName);
                streamWriter.WriteLine("True");
            }

            checklistAppointment.ChangeTaskStatus(74);

            string[] file = File.ReadAllLines(filePath);
            File.Delete(filePath);

            Assert.Equal("False", file[2]);
        }

        [Fact]
        public void NoTaskStatusChange()
        {
            ChecklistDTO checklistDTO = new ChecklistDTO();
            checklistDTO.TaskID = 28;
            checklistDTO.TaskName = "Bake a cake";
            checklistAppointment = new ChecklistAppointment(new StubChecklistAppointmentRepository());

            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(checklistDTO.TaskID);
                streamWriter.WriteLine(checklistDTO.TaskName);
                streamWriter.WriteLine("False");
            }

            checklistAppointment.ChangeTaskStatus(-5);

            string[] file = File.ReadAllLines(filePath);
            File.Delete(filePath);

            Assert.Equal("False", file[2]);
        }
    }
}
