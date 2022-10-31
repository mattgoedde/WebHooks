using Microsoft.AspNetCore.Mvc;
using WebHook.Class.Entity.Base;

namespace WebHook.Api.Controllers.Base;

public interface ICrudController<TEntity>
    where TEntity : EntityBase
{
    Task<ActionResult<TEntity>> Create(TEntity entity);

    Task<ActionResult<TEntity>> Read(int id);
    Task<ActionResult<IEnumerable<TEntity>>> Read(IEnumerable<int> ids);
    Task<ActionResult<IEnumerable<TEntity>>> Read();

    Task<ActionResult<TEntity>> Update(TEntity entity);

    Task<ActionResult> Delete(TEntity entity);
}
