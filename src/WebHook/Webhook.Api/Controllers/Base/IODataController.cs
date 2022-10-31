using Microsoft.AspNetCore.Mvc;
using WebHook.Class.Entity.Base;

namespace WebHook.Api.Controllers.Base;

public interface IODataController<TEntity>
    where TEntity : EntityBase
{
    ActionResult<IQueryable<TEntity>> Odata();
}
