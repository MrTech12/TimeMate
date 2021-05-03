using DataAccessLayer.DTO;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Logic
{
    public class Account
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISender _senderRepository;
        private AccountDTO accountDTO;
        public AccountDTO AccountDTO { get { return this.accountDTO; } set { this.accountDTO = value; } }

        public Account(AccountDTO accountDTO, IAccountRepository accountRepository)
        {
            this.accountDTO = accountDTO;
            _accountRepository = accountRepository;
        }

        public Account(AccountDTO accountDTO, IAccountRepository accountRepository, ISender senderRepository)
        {
            this.accountDTO = accountDTO;
            _accountRepository = accountRepository;
            _senderRepository = senderRepository;
        }

        public string[] LoggingIn()
        {
            string[] returnMessage = new string[2];

            string databaseOutput = _accountRepository.SearchForPasswordHash(accountDTO.Mail);
            if (databaseOutput != null)
            {
                bool passwordValid = BCrypt.Net.BCrypt.Verify(accountDTO.Password, databaseOutput);
                if (!passwordValid)
                {
                    returnMessage[0] = null;
                }
                else
                {
                    returnMessage[0] = Convert.ToString(_accountRepository.GetUserID(accountDTO.Mail));
                    returnMessage[1] = _accountRepository.GetFirstName(accountDTO.Mail);
                }
            }
            else
            {
                returnMessage[0] = null;
            }
            return returnMessage;
        }

        public string CheckExistingMail()
        {
            if (accountDTO.Mail == null)
            {
                throw new AccountException("Het mailadres is niet geleverd.");
            }

            int databaseOutput = _accountRepository.GetUserID(accountDTO.Mail);
            if (databaseOutput != -1)
            {
                return "Er bestaat al een account met dit mailadres.";
            }
            else
            {
                return null;
            }
        }

        public void CreateAccount()
        {
            if (accountDTO.Password == null)
            {
                throw new AccountException("Het wachtwoord is niet geleverd.");
            }
            else
            {
                accountDTO.Password = BCrypt.Net.BCrypt.HashPassword(accountDTO.Password, 10);
                accountDTO.AccountID = _accountRepository.CreateAccount(accountDTO);
                _senderRepository.SendAccountCreationMail(accountDTO.Mail);
            }
        }
    }
}
