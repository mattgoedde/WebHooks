using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using WebHook.Class.Entity.Base;

namespace WebHook.Api.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public abstract class ODataControllerBase<TEntity> : ControllerBase, IODataController<TEntity>
    where TEntity : EntityBase
{
    protected readonly DbContext _dbContext;

    protected ODataControllerBase(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected virtual IQueryable<TEntity> GetQueryable() => _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();

    [EnableQuery]
    [HttpGet(nameof(Odata), Name = $"[controller]/{nameof(Odata)}")]
    public ActionResult<IQueryable<TEntity>> Odata() => Ok(GetQueryable());
}
