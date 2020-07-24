using System.ComponentModel.Composition;
using Samba.Domain.Models.Entities;

namespace Samba.Services.Implementations.PrinterModule.ValueChangers
{
    
    public class EntityValueChanger : AbstractValueChanger<Entity>
    {
        private readonly ICacheService _cacheService;

        
        public EntityValueChanger(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public override string GetTargetTag()
        {
            return "ENTITY";
        }

        protected override string GetModelName(Entity model)
        {
            var entityType = _cacheService.GetEntityTypeById(model.EntityTypeId);
            return entityType == null ? "" : entityType.EntityName;
        }
    }
}
