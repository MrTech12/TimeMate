using DataAccessLayer.DTO;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogicLayer.Logic
{
    public class Account
    {
        private readonly IAccountContainer _accountContainer;
        private readonly IAgendaContainer _agendaContainer;
        private readonly ISenderContainer _senderContainer;
        private readonly IJobContainer _jobContainer;

        private AccountDTO accountDTO;

        public Account(AccountDTO accountDTO, IAccountContainer accountContainer, IAgendaContainer agendaContainer, IJobContainer jobContainer, ISenderContainer senderContainer)
        {
            this.accountDTO = accountDTO;
            this._accountContainer = accountContainer;
            this._agendaContainer = agendaContainer;
            this._jobContainer = jobContainer;
            this._senderContainer = senderContainer;
        }

        public Account(AccountDTO accountDTO, IAccountContainer accountContainer, IAgendaContainer agendaContainer)
        {
            this.accountDTO = accountDTO;
            this._accountContainer = accountContainer;
            this._agendaContainer = agendaContainer;
        }

        public Account(AccountDTO accountDTO, IAgendaContainer agendaContainer)
        {
            this.accountDTO = accountDTO;
            this._agendaContainer = agendaContainer;
        }

        public Account(AccountDTO accountDTO, IAgendaContainer agendaContainer, IJobContainer jobContainer)
        {
            this.accountDTO = accountDTO;
            this._agendaContainer = agendaContainer;
            this._jobContainer = jobContainer;
        }

        public Account(AccountDTO accountDTO, IAccountContainer accountContainer)
        {
            this.accountDTO = accountDTO;
            this._accountContainer = accountContainer;
        }

        public string[] LoggingIn()
        {
            string[] returnMessage = new string[2];

            string databaseOutput = _accountContainer.SearchForPasswordHash(accountDTO.Mail);
            if (databaseOutput != null)
            {
                bool passwordValid = BCrypt.Net.BCrypt.Verify(accountDTO.Password, databaseOutput);
                if (!passwordValid)
                {
                    returnMessage = null;
                }
                else
                {
                    returnMessage[0] = Convert.ToString(_accountContainer.GetUserID(accountDTO.Mail));
                    returnMessage[1] = _accountContainer.GetFirstName(accountDTO.Mail);
                }
            }
            else
            {
                returnMessage[0] = null;
            }
            return returnMessage;
        }

        public string[] NewAccountInputValidation()
        {
            string[] returnMessage = new string[2];

            if (accountDTO.Mail == null)
            {
                throw new AccountException("Het mailadres is niet geleverd.");
            }

            int databaseOutput = _accountContainer.GetUserID(accountDTO.Mail);
            if (databaseOutput != -1)
            {
                returnMessage[0] = "Er bestaat al een account met dit mailadres.";
            }
            else
            {
                CreateAccount();
                _senderContainer.SendAccountCreationMessage(accountDTO.Mail);
                returnMessage[0] = Convert.ToString(accountDTO.AccountID);
                returnMessage[1] = _accountContainer.GetFirstName(accountDTO.Mail);
            }
            return returnMessage;
        }

        public void CreateAccount()
        {
            if (accountDTO.Password == null)
            {
                throw new AccountException("Het wachtwoord is niet geleverd.");
            }

            accountDTO.Password = BCrypt.Net.BCrypt.HashPassword(accountDTO.Password, 10);

            if (accountDTO.JobCount == 0)
            {
                accountDTO.AccountID = _accountContainer.CreateAccount(accountDTO);
            }
            else if (accountDTO.JobCount > 0)
            {
                accountDTO.AccountID = _accountContainer.CreateAccount(accountDTO);
                CreateWorkAgenda();
            }
        }

        public void CreateAgenda(AgendaDTO agendaDTO)
        {
            _agendaContainer.AddAgenda(accountDTO.AccountID, agendaDTO);
        }

        public void CreateWorkAgenda()
        {
            AgendaDTO agendaDTO = new AgendaDTO();
            agendaDTO.AgendaName = "Bijbaan";
            agendaDTO.AgendaColor = "#FF0000";
            agendaDTO.NotificationType = "Nee";

            agendaDTO.AgendaID = _agendaContainer.AddAgenda(accountDTO.AccountID, agendaDTO);
            _jobContainer.AddPayDetails(accountDTO);
        }

        public List<AgendaDTO> RetrieveAgendas()
        {
            return _agendaContainer.GetAllAgendas(accountDTO.AccountID);
        }

        public void DeleteAgenda(int agendaID)
        {
            _agendaContainer.DeleteAgenda(accountDTO.AccountID,agendaID);
        }
    }
}
