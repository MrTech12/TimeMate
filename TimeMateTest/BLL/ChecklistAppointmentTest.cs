using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using System.IO;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class ChecklistAppointmentTest
    {
        private ChecklistAppointment checklistAppointment;

        [Fact]
        public void GetTasksTest()
        {
            int appointmentID = 14;
            checklistAppointment = new ChecklistAppointment(new StubChecklistAppointmentContext());

            var output = checklistAppointment.RetrieveTasks(appointmentID);

            Assert.Equal("Dit", output[1]);
            Assert.Equal("Dat", output[3]);
            Assert.Equal("Zo", output[5]);
            Assert.True(output.Count == 6);
        }

        [Fact]
        public void GetNoTasksTest()
        {
            int appointmentID = 0;
            checklistAppointment = new ChecklistAppointment(new StubChecklistAppointmentContext());

            var output = checklistAppointment.RetrieveTasks(appointmentID);

            Assert.True(output.Count == 0);
        }

        [Fact]
        public void ChangeTaskStatusToDoneTest()
        {
            checklistAppointment = new ChecklistAppointment(new StubChecklistAppointmentContext());
            ChecklistDTO checklistDTO = new ChecklistDTO();
            checklistDTO.TaskID = 62;
            checklistDTO.TaskName = "Get cake";

            using (StreamWriter streamWriter = new StreamWriter(@"C:\tmp\getTaskStatusTest.txt"))
            {
                streamWriter.WriteLine(checklistDTO.TaskID);
                streamWriter.WriteLine(checklistDTO.TaskName);
                streamWriter.WriteLine("False");
            }
            checklistAppointment.ChangeTaskStatus(62);

            string[] file = File.ReadAllLines(@"C:\tmp\getTaskStatusTest.txt");
            File.Delete(@"C:\tmp\getTaskStatusTest.txt");

            Assert.Equal("True", file[2]);
        }

        [Fact]
        public void ChangeTaskStatusToNotDoneTest()
        {
            checklistAppointment = new ChecklistAppointment(new StubChecklistAppointmentContext());
            ChecklistDTO checklistDTO = new ChecklistDTO();
            checklistDTO.TaskID = 74;
            checklistDTO.TaskName = "Buy new monitor";

            using (StreamWriter streamWriter = new StreamWriter(@"C:\tmp\getTaskStatusTest.txt"))
            {
                streamWriter.WriteLine(checklistDTO.TaskID);
                streamWriter.WriteLine(checklistDTO.TaskName);
                streamWriter.WriteLine("True");
            }

            checklistAppointment.ChangeTaskStatus(74);

            string[] file = File.ReadAllLines(@"C:\tmp\getTaskStatusTest.txt");
            File.Delete(@"C:\tmp\getTaskStatusTest.txt");

            Assert.Equal("False", file[2]);
        }
    }
}
