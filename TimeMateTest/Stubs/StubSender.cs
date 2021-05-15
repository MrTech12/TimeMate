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
        public void CreateAccountCreationMail(string recipient)
        {
            using (StreamWriter streamWriter = File.AppendText(@"C:\tmp\CreateAccountTest.txt"))
            {
                streamWriter.WriteLine("De ontvanger " + recipient);
            }
        }

        public void SendAccountCreationMail(string recipient)
        {
            using (StreamWriter streamWriter = File.AppendText(@"C:\tmp\CreateAccountTest.txt"))
            {
                streamWriter.WriteLine("Een mail is verstuurd naar " + recipient);
            }
        }
    }
}
