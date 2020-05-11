using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogicLayer.Logic
{
    public class AccountLogic
    {
        private IAccountContext AccountContext;
        private IAgendaContext AgendaContext;
        private AccountDTO accountDTO;

        private string returnMessage;
        private string databaseOutput;

        public AccountLogic(AccountDTO accountDTO, IAccountContext accountContext, IAgendaContext agendaContext)
        {
            this.accountDTO = accountDTO;
            this.AccountContext = accountContext;
            this.AgendaContext = agendaContext;
        }

        /// <summary>
        /// Letting the user log into their account, with their entered credentials.
        /// </summary>
        public string UserLogsIn()
        {
            databaseOutput = AccountContext.SearchForPasswordHash(accountDTO.MailAddress);
            if (databaseOutput != null)
            {
                bool passwordValid = BCrypt.Net.BCrypt.Verify(accountDTO.Password, databaseOutput);
                if (!passwordValid)
                {
                    returnMessage = "Verkeerd mailadres en/of wachtwoord.";
                }
                else
                {
                    returnMessage = null;
                }
            }
            else
            {
                returnMessage = "Verkeerd mailadres en/of wachtwoord.";
            }

            return returnMessage;
        }

        public int GetActiveAccountID(string mail)
        {
            int accountID = AccountContext.GetUserID(mail);
            return accountID;
        }

        /// <summary>
        /// Checking the input for creating a new account.
        /// </summary>
        public string NewAccountValidation()
        {
            string mailValidate = "^([0-9a-zA-Z-_]([-\\.\\w]*[0-9a-zA-Z-_])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            string specialCharacterValidate = @"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""_-]";
            string nameValidate = @"^[a-zA-Z]+$";

            if (Regex.IsMatch(accountDTO.MailAddress, mailValidate) == false)
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
                databaseOutput = AccountContext.SearchUserByMail(accountDTO.MailAddress);
                if (databaseOutput != null)
                {
                    returnMessage = "Er bestaat al een account met dit mailadres.";
                }
                else
                {
                    CreateUserInDB();
                }
            }
            return returnMessage;
        }

        /// <summary>
        /// Create an account for the user, with their entered input.
        /// </summary>
        public void CreateUserInDB()
        {
            accountDTO.Password = BCrypt.Net.BCrypt.HashPassword(accountDTO.Password, 10);

            if (accountDTO.JobCount == 0)
            {
                AccountContext.RegisterNewUser(accountDTO);
                accountDTO.AccountID = AccountContext.GetUserID(accountDTO.MailAddress);
            }
            else if (accountDTO.JobCount > 0)
            {
                AccountContext.RegisterNewUser(accountDTO);
                accountDTO.AccountID = AccountContext.GetUserID(accountDTO.MailAddress);
                CreateWorkAgenda();
            }
        }

        /// <summary>
        /// Checking the input for the new agenda.
        /// </summary>
        public void CreateAgenda(AgendaDTO agendaDTO)
        {
            AgendaDTO newAgendaDTO = new AgendaDTO();
            newAgendaDTO.AgendaName = agendaDTO.AgendaName;
            newAgendaDTO.AgendaColor = agendaDTO.AgendaColor;
            newAgendaDTO.Notification = agendaDTO.Notification;

            AgendaContext.AddNewAgenda(newAgendaDTO, accountDTO);
        }

        /// <summary>
        /// Create a work agenda.
        /// </summary>
        public void CreateWorkAgenda()
        {
            AgendaDTO newAgendaDTO = new AgendaDTO();
            newAgendaDTO.AgendaName = "Bijbaan";
            newAgendaDTO.AgendaColor = "#FF0000";
            newAgendaDTO.Notification = "Nee";
            AgendaContext.AddNewJobAgenda(newAgendaDTO, accountDTO);
        }
    }
}
