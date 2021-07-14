using Core.DTOs;
using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class AgendaService
    {
        private IAgendaRepository _agendaRepository;
        private AccountDTO accountDTO = new AccountDTO();

        public AgendaService(AccountDTO accountDTO, IAgendaRepository agendaRepository)
        {
            this.accountDTO = accountDTO;
            _agendaRepository = agendaRepository;
        }

        public void AddAgenda(AgendaDTO agendaDTO)
        {
            Agenda agenda = new Agenda() { AccountID = accountDTO.AccountID, AgendaName = agendaDTO.AgendaName, AgendaColor = agendaDTO.AgendaColor, IsWorkAgenda = agendaDTO.IsWorkAgenda };
            _agendaRepository.CreateAgenda(agenda);
        }

        public List<AgendaDTO> RetrieveAgendas()
        {
            var agendas = _agendaRepository.GetAgendas(accountDTO.AccountID);

            List<AgendaDTO> agendaDTOs = new List<AgendaDTO>();
            foreach (var item in agendas)
            {
                agendaDTOs.Add(new AgendaDTO() { AgendaID = item.AgendaID, AgendaName = item.AgendaName, AgendaColor = item.AgendaColor });
            }

            return agendaDTOs;
        }

        public void DeleteAgenda(int agendaID)
        {
            Agenda agenda = new Agenda() { AccountID = accountDTO.AccountID, AgendaID = agendaID };
            _agendaRepository.DeleteAgenda(agenda);
        }
    }
}
