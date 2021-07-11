using Core.DTOs;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class AgendaView
    {
        private IAppointmentRepository _appointmentRepository;
        private AccountDTO accountDTO = new AccountDTO();

        public AgendaView(AccountDTO accountDTO, IAppointmentRepository appointmentRepository)
        {
            this.accountDTO = accountDTO;
            _appointmentRepository = appointmentRepository;
        }

        public List<AppointmentDTO> RetrieveAppointments()
        {
            List<TaskDTO> taskList = new List<TaskDTO>();
            var appointments = _appointmentRepository.GetAppointments(accountDTO.AccountID);

            // Because using an ORM is prohibited, the below code links appointments with the right task(s).
            // Without the below code, an appointment with multiple tasks would be displayed two times.
            // The below 'for' loop stored every checklist it can find, which has a taskname, in a seperate list.
            for (int i = 0; i < appointments.Count; i++)
            {
                TaskDTO taskDTO = new TaskDTO();
                if (appointments[i].TaskList.Count > 0 && appointments[i].TaskList[0].TaskName != null)
                {
                    taskDTO = appointments[i].TaskList[0];
                    taskList.Add(taskDTO);
                    appointments[i].TaskList.RemoveAt(0);
                }
            }
            // Sorting the retrieved appointments, so that duplicate appointments would be removed.
            List<AppointmentDTO> sortedAppointments = appointments.OrderBy(x => x.StartDate).GroupBy(x => x.AppointmentID).Select(g => g.First()).ToList();

            if (taskList.Count != 0)
            {
                foreach (var item in sortedAppointments)
                {
                    for (int i = 0; i < taskList.Count; i++)
                    {
                        if (item.AppointmentID == taskList[i].AppointmentID) // Adding tasks to the right appointment, by looking at the appointmentID.
                        {
                            item.TaskList.Add(taskList[i]);
                        }
                    }
                }
            }
            return sortedAppointments;
        }
    }
}
