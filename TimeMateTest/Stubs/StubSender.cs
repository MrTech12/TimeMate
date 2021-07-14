using Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TimeMateTest.Stubs
{
    public class StubSender : ISender
    {
        public void CreateAccountCreationMessage(string recipient)
        {
            using (StreamWriter streamWriter = File.AppendText(@"C:\tmp\CreateAccountTest.txt"))
            {
                streamWriter.WriteLine("De ontvanger " + recipient);
            }
        }

        public void SendAccountCreationMessage(string recipient)
        {
            using (StreamWriter streamWriter = File.AppendText(@"C:\tmp\CreateAccountTest.txt"))
            {
                streamWriter.WriteLine("Een mail is verstuurd naar " + recipient);
            }
        }
    }
}
