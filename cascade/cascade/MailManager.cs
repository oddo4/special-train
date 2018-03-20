using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cascade
{
    class MailManager
    {
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public MailManager From(string mailFrom)
        {
            MailFrom = mailFrom;
            return this;
        }
        public MailManager To(string mailTo)
        {
            MailTo = mailTo;
            return this;
        }
        public MailManager Subject(string subject)
        {
            MailSubject = subject;
            return this;
        }
        public MailManager Body(string body)
        {
            MailBody = body;
            return this;
        }

        public void Send(MailManager mail)
        {
            Debug.WriteLine("Sent");
        }

        public static void Send(Action<MailManager> action)
        {
            action(new MailManager());

            Debug.WriteLine("Sent");
        }




    }
}
