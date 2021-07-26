using Core.DTOs;
using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class AgendaViewService
    {
        private IAppointmentRepository _appointmentRepository;
        private AccountDTO accountDTO = new AccountDTO();

        public AgendaViewService(AccountDTO accountDTO, IAppointmentRepository appointmentRepository)
        {
            this.accountDTO = accountDTO;
            _appointmentRepository = appointmentRepository;
        }

        public List<AppointmentDTO> RetrieveAppointments()
        {
            var appointments = _appointmentRepository.GetAppointments(accountDTO.AccountID);
            return MapEntityToDTO(appointments);
        }

        public List<AppointmentDTO> MapEntityToDTO(List<Appointment> appointments)
        {
            // Because using an ORM is prohibited for the assignment, the below code links appointments with the right task(s).
            // Without the below code, an appointment with multiple tasks would be displayed two times.

            // The below 'for' loop stores every checklist it can find, which has a taskname, in a seperate 'TaskDTO' list.
            List<TaskDTO> taskList = new List<TaskDTO>();
            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].TaskList.Count > 0 && appointments[i].TaskList[0].TaskName != null)
                {
                    TaskDTO taskDTO = new TaskDTO() { TaskID = appointments[i].TaskList[0].TaskID, AppointmentID = appointments[i].TaskList[0].AppointmentID, TaskName = appointments[i].TaskList[0].TaskName, TaskChecked = appointments[i].TaskList[0].TaskChecked };
                    taskList.Add(taskDTO);
                    appointments[i].TaskList.RemoveAt(0);
                }
            }

            // Filling the list of appointment DTOs with the information that is stored in the list of appointment Entities.
            List<AppointmentDTO> appointmentDTOs = new List<AppointmentDTO>();
            foreach (var item in appointments)
            {
                AppointmentDTO appointmentDTO = new AppointmentDTO() { AgendaID = item.AgendaID, AppointmentID = item.AppointmentID, AppointmentName = item.AppointmentName, AgendaName = item.AgendaName, StartDate = item.StartDate, EndDate = item.EndDate };

                if (item.Description.DescriptionName != null)
                {
                    appointmentDTO.DescriptionDTO.Description = item.Description.DescriptionName;
                }
                appointmentDTOs.Add(appointmentDTO);
            }

            // Sorting the list of appointment DTOs, so that duplicate entries would be removed.
            List<AppointmentDTO> sortedAppointmentDTOs = appointmentDTOs.OrderBy(x => x.StartDate).GroupBy(x => x.AppointmentID).Select(g => g.First()).ToList();

            if (taskList.Count != 0)
            {
                foreach (var item in sortedAppointmentDTOs)
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
            return sortedAppointmentDTOs;
        }
    }
}
