
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json.Serialization;
using StaticHttpContextAccessor.Helpers;

namespace GloboTicket.Services.EventCatalog.Models
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        // new values to be added in new schema V2
        public DateTime? FromDate { get; set; }
        public DateTime? UntilDate { get; set; }
  
        // add conditional serialization by convention based methods
        // this is a feature of the newtonsoft serializer, tha tis added in startup

        public bool ShouldSerializeFromDate()
        {
            return ShouldUseSchemaV2();
        }
  
        public bool ShouldSerializeUntilDate()
        {
            return ShouldUseSchemaV2();
        }

        private static bool ShouldUseSchemaV2()
        {
            var configuration = (IConfiguration)HttpContextHelper.Current.RequestServices.GetService(typeof(IConfiguration));
            var version = configuration["CategorySchemaVersion"];
            bool UseSchemaV2 = version.Equals("2.0");
            return UseSchemaV2;
        }

    }

}
