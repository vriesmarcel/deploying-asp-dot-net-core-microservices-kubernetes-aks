using System;
using System.Collections.Generic;

namespace GloboTicket.Services.EventCatalog.Entities
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public List<Event> Events { get; set; }
       
        // version 2 of the entity
        public DateTime? FromDate { get; set; }
        public DateTime? UntilDate { get; set; }

    }
}
