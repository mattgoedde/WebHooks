using WebHook.Api.Controllers.Base;
using WebHook.Class.Entity;
using WebHook.Class.Event;
using WebHook.Data;
using WebHook.Logic.Base;

namespace WebHook.Api.Controllers;

public class UsersController : CrudControllerBase<User>
{
    public UsersController(WebHookContext dbContext, IEventNotifier<EntityChangedEvent> eventNotifier) : base(dbContext, eventNotifier) { }
}