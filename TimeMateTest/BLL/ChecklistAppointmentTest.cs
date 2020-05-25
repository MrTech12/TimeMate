using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class ChecklistAppointmentTest
    {
        private AccountDTO accountDTO;
        private ChecklistAppointment checklistAppointment;

        [Fact]
        public void RetrieveTaskTest()
        {
            AppointmentDTO output = new AppointmentDTO() { AppointmentID = 6};
            checklistAppointment = new ChecklistAppointment(output, new StubChecklistAppointmentContext());

            output = checklistAppointment.RetrieveTask(output.AppointmentID);

            Assert.Contains("Mow the lawn", output.ChecklistItemName[1]);
            Assert.True(output.ChecklistItemName.Count == 3);
        }
    }
}
