using BusinessLogicLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TimeMateTest.Stubs
{
    public class StubSender : ISender
    {
        public void SendAccountCreationMail(string mail)
        {
            using (StreamWriter streamWriter = File.AppendText(@"C:\tmp\CreateAccountTest.txt"))
            {
                streamWriter.WriteLine("Een mail is verstuurd naar " + mail);
            }
        }
    }
}
