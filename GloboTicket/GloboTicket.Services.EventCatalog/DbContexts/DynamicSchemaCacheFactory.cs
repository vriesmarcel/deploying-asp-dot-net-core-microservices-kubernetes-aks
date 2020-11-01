using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace GloboTicket.Services.EventCatalog.DbContexts
{
    public class DynamicSchemaCacheFactory : IModelCacheKeyFactory
    {
        public bool UseSchemaV2 { get; set; }
   

        public object Create(DbContext context)
        {
            if (context is EventCatalogDbContext eventCatalogDBContext)
                eventCatalogDBContext.UseSchemaV2 = UseSchemaV2;
            return context.GetType();
        }
    }
}