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
        private readonly IAccountContainer _accountContainer;
        private readonly IAgendaContainer _agendaContainer;
        private readonly ISenderContainer _senderContainer;

        private AccountDTO accountDTO;
        private string returnMessage;
        private string databaseOutput;

        public Account(AccountDTO accountDTO, IAccountContainer accountContainer, IAgendaContainer agendaContainer, ISenderContainer senderContainer)
        {
            this.accountDTO = accountDTO;
            this._accountContainer = accountContainer;
            this._agendaContainer = agendaContainer;
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

        public Account(AccountDTO accountDTO, IAccountContainer accountContainer)
        {
            this.accountDTO = accountDTO;
            this._accountContainer = accountContainer;
        }

        public string LoggingIn()
        {
            databaseOutput = _accountContainer.SearchForPasswordHash(accountDTO.Mail);
            if (databaseOutput != null)
            {
                bool passwordValid = BCrypt.Net.BCrypt.Verify(accountDTO.Password, databaseOutput);
                if (!passwordValid)
                {
                    returnMessage = "Verkeerd mailadres en/of wachtwoord.";
                }
                else
                {
                    returnMessage = _accountContainer.GetUserID(accountDTO.Mail);
                }
            }
            else
            {
                returnMessage = "Verkeerd mailadres en/of wachtwoord.";
            }

            return returnMessage;
        }

        public string NewAccountValidation()
        {
            string mailValidate = "^([0-9a-zA-Z-_]([-\\.\\w]*[0-9a-zA-Z-_])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            string specialCharacterValidate = @"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""_-]";
            //string nameValidate = @"^[a-zA-Z]+$";

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
                databaseOutput = _accountContainer.GetUserID(accountDTO.Mail);
                if (databaseOutput != null)
                {
                    returnMessage = "Er bestaat al een account met dit mailadres.";
                }
                else
                {
                    CreateAccount();
                    _senderContainer.SendAccountCreationMessage(accountDTO.Mail);
                    returnMessage = Convert.ToString(accountDTO.AccountID);
                }
            }
            return returnMessage;
        }

        public void CreateAccount()
        {
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
            _agendaContainer.AddAgenda(agendaDTO, accountDTO.AccountID);
        }

        public void CreateWorkAgenda()
        {
            AgendaDTO agendaDTO = new AgendaDTO();
            agendaDTO.AgendaName = "Bijbaan";
            agendaDTO.AgendaColor = "#FF0000";
            agendaDTO.NotificationType = "Nee";

            agendaDTO.AgendaID = _agendaContainer.AddAgenda(agendaDTO, accountDTO.AccountID);
            _agendaContainer.AddPayDetails(agendaDTO.AgendaID, accountDTO);
        }

        public List<AgendaDTO> RetrieveAgendas()
        {
            return _agendaContainer.GetAllAgendas(accountDTO.AccountID);
        }

        public void DeleteAgenda(int agendaID)
        {
            _agendaContainer.DeleteAgenda(agendaID, accountDTO.AccountID);
        }
    }
}
