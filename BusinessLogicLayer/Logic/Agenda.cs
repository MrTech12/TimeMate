using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class Agenda
    {
        private IAgendaRepository _agendaRepository;

        private AccountDTO accountDTO = new AccountDTO();

        public Agenda(AccountDTO accountDTO, IAgendaRepository agendaRepository)
        {
            this.accountDTO = accountDTO;
            _agendaRepository = agendaRepository;
        }

        public void CreateAgenda(AgendaDTO agendaDTO)
        {
            _agendaRepository.AddAgenda(accountDTO.AccountID, agendaDTO);
        }

        public List<AgendaDTO> RetrieveAgendas()
        {
            return _agendaRepository.GetAllAgendas(accountDTO.AccountID);
        }

        public void DeleteAgenda(int agendaID)
        {
            _agendaRepository.DeleteAgenda(accountDTO.AccountID, agendaID);
        }
    }
}
