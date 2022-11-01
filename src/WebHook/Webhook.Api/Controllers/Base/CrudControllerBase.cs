using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHook.Class.Entity.Base;
using WebHook.Class.Event;
using WebHook.Logic.Base;

namespace WebHook.Api.Controllers.Base;

[ApiController]
[Route("api/[controller]/")]
public abstract class CrudControllerBase<TEntity> : ODataControllerBase<TEntity>, ICrudController<TEntity>
    where TEntity : EntityBase
{

    private readonly IEventNotifier<EntityChangedEvent> _eventNotifier;

    protected CrudControllerBase(DbContext dbContext, IEventNotifier<EntityChangedEvent> eventNotifier) : base(dbContext)
    {
        _eventNotifier = eventNotifier;
    }

    protected virtual async Task<TEntity> CreateEntityAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    protected virtual async Task<TEntity> ReadEntityAsync(int id)
        => await GetQueryable().FirstAsync(e => e.Id == id);

    protected virtual async Task<IEnumerable<TEntity>> ReadEntitiesAsync(IEnumerable<int> ids)
        => await GetQueryable().Where(e => ids.Contains(e.Id)).ToListAsync();

    protected virtual async Task<IEnumerable<TEntity>> ReadEntitiesAsync()
        => await GetQueryable().ToListAsync();

    protected virtual async Task<TEntity> UpdateEntityAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    protected virtual async Task DeleteEntityAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    [HttpPost]
    public async Task<ActionResult<TEntity>> Create([FromBody] TEntity entity)
    {
        try
        {
            var newEntity = await CreateEntityAsync(entity);
            _ = Task.Run(() => _eventNotifier.Notify(new EntityChangedEvent
            {
                EventTimestampUtc = DateTime.UtcNow,
                EntityLocation = $"{Request.Host}/api/{RouteData.Values["controller"]}/{nameof(Read)}/id?id={newEntity.Id}",
                EntityType = RouteData.Values["controller"]?.ToString() ?? "",
                Action = EntityChangedAction.Created
            }));
            return CreatedAtAction(nameof(Read), new { id = newEntity.Id }, newEntity);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("read")]
    public async Task<ActionResult<IEnumerable<TEntity>>> Read()
    {
        try
        {
            var entity = await ReadEntitiesAsync();
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("read/id")]
    public async Task<ActionResult<TEntity>> Read([Required] int id)
    {
        try
        {
            var entity = await ReadEntityAsync(id);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("read/ids")]
    public async Task<ActionResult<IEnumerable<TEntity>>> Read([Required] IEnumerable<int> ids)
    {
        try
        {
            var entities = await ReadEntitiesAsync(ids);
            return Ok(entities);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<TEntity>> Update([FromBody] TEntity entity)
    {
        try
        {
            var updatedEntity = await UpdateEntityAsync(entity);
            _ = Task.Run(() => _eventNotifier.Notify(new EntityChangedEvent
            {
                EventTimestampUtc = DateTime.UtcNow,
                EntityLocation = $"{Request.Host}/api/{RouteData.Values["controller"]}/{nameof(Read)}/id?id={entity.Id}",
                EntityType = RouteData.Values["controller"]?.ToString() ?? "",
                Action = EntityChangedAction.Updated
            }));
            return Ok(updatedEntity);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] TEntity entity)
    {
        try
        {
            await DeleteEntityAsync(entity);

            _ = Task.Run(() => _eventNotifier.Notify(new EntityChangedEvent
            {
                EventTimestampUtc = DateTime.UtcNow,
                EntityLocation = $"{Request.Host}/api/{RouteData.Values["controller"]}/{nameof(Read)}/id?id={entity.Id}",
                EntityType = RouteData.Values["controller"]?.ToString() ?? "",
                Action = EntityChangedAction.Deleted
            }));

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
