using DataAccessLayer.DTO;
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
        private readonly IAccountContainer _accountContext;
        private readonly IAgendaContainer _agendaContext;
        private readonly ISenderContainer _senderContext;
        private AccountDTO accountDTO;

        private string returnMessage;
        private string databaseOutput;

        public Account(AccountDTO accountDTO, IAccountContainer accountContext, IAgendaContainer agendaContext, ISenderContainer senderContext)
        {
            this.accountDTO = accountDTO;
            this._accountContext = accountContext;
            this._agendaContext = agendaContext;
            this._senderContext = senderContext;
        }

        public Account(AccountDTO accountDTO, IAccountContainer accountContext, IAgendaContainer agendaContext)
        {
            this.accountDTO = accountDTO;
            this._accountContext = accountContext;
            this._agendaContext = agendaContext;
        }

        public Account(AccountDTO accountDTO, IAgendaContainer agendaContext)
        {
            this.accountDTO = accountDTO;
            this._agendaContext = agendaContext;
        }

        /// <summary>
        /// Checking the entered credentials to give an actor access to a specific account.
        /// </summary>
        public string LoggingIn()
        {
            databaseOutput = _accountContext.SearchForPasswordHash(accountDTO.Mail);
            if (databaseOutput != null)
            {
                bool passwordValid = BCrypt.Net.BCrypt.Verify(accountDTO.Password, databaseOutput);
                if (!passwordValid)
                {
                    returnMessage = "Verkeerd mailadres en/of wachtwoord.";
                }
                else
                {
                    returnMessage = _accountContext.GetUserID(accountDTO.Mail);
                }
            }
            else
            {
                returnMessage = "Verkeerd mailadres en/of wachtwoord.";
            }

            return returnMessage;
        }

        /// <summary>
        /// Checking the input for creating a new account.
        /// </summary>
        public string NewAccountValidation()
        {
            string mailValidate = "^([0-9a-zA-Z-_]([-\\.\\w]*[0-9a-zA-Z-_])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            string specialCharacterValidate = @"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""_-]";
            string nameValidate = @"^[a-zA-Z]+$";

            if (Regex.IsMatch(accountDTO.Mail, mailValidate) == false)
            {
                returnMessage = "Het emailadres is niet geldig.";
            }
            else if (accountDTO.Password.Any(char.IsUpper) == false)
            {
                returnMessage = "Het wachtwoord moet een hoofdletter bevatten.";
            }
            else if (Regex.IsMatch(accountDTO.Password, specialCharacterValidate) == false)
            {
                returnMessage = "Het wachtwoord moet een speciale karakter bevatten.";
            }
            else if (accountDTO.Password.Any(char.IsDigit) == false)
            {
                returnMessage = "Het wachtwoord moet een cijfer bevatten.";
            }
            else if (returnMessage == null)
            {
                databaseOutput = _accountContext.GetUserID(accountDTO.Mail);
                if (databaseOutput != null)
                {
                    returnMessage = "Er bestaat al een account met dit mailadres.";
                }
                else
                {
                    CreateAccount();
                    _senderContext.SendAccountCreationMessage(accountDTO.Mail);
                    returnMessage = Convert.ToString(accountDTO.AccountID);
                }
            }
            return returnMessage;
        }

        /// <summary>
        /// Create a new account for the actor, with their entered input.
        /// </summary>
        public void CreateAccount()
        {
            accountDTO.Password = BCrypt.Net.BCrypt.HashPassword(accountDTO.Password, 10);

            if (accountDTO.JobCount == 0)
            {
                accountDTO.AccountID = _accountContext.CreateAccount(accountDTO);
            }
            else if (accountDTO.JobCount > 0)
            {
                accountDTO.AccountID = _accountContext.CreateAccount(accountDTO);
                CreateWorkAgenda();
            }
        }

        /// <summary>
        /// Create an agenda for the actor.
        /// </summary>
        public void CreateAgenda(AgendaDTO agendaDTO)
        {
            _agendaContext.AddAgenda(agendaDTO, accountDTO);
        }

        /// <summary>
        /// Create a work agenda for the actor.
        /// </summary>
        public void CreateWorkAgenda()
        {
            AgendaDTO newAgendaDTO = new AgendaDTO();
            newAgendaDTO.AgendaName = "Bijbaan";
            newAgendaDTO.AgendaColor = "#FF0000";
            newAgendaDTO.NotificationType = "Nee";

            newAgendaDTO.AgendaID = _agendaContext.AddAgenda(newAgendaDTO, accountDTO);

            _agendaContext.AddPayDetails(newAgendaDTO,accountDTO);
        }

        /// <summary>
        /// Get the info of all the agenda's that belong to the current active actor.
        /// </summary>
        /// <returns></returns>
        public List<AgendaDTO> RetrieveAgendas()
        {
            List<AgendaDTO> agendasFromUser = new List<AgendaDTO>();
            agendasFromUser = _agendaContext.GetAllAgendas(accountDTO);
            return agendasFromUser;
        }

        /// <summary>
        /// Delete an agenda from the account of the current active actor.
        /// </summary>
        public void DeleteAgenda(int agendaID)
        {
            _agendaContext.DeleteAgenda(agendaID, accountDTO);
        }
    }
}
