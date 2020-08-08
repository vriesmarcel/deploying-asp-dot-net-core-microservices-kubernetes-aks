using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GloboTicket.Integration.Messages
{
    public class IntegrationBaseMessage
    {
        public Guid Id { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
