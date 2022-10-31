using Microsoft.EntityFrameworkCore;
using WebHook.Api.Controllers.Base;
using WebHook.Class.Entity;
using WebHook.Data;

namespace WebHook.Api.Controllers;

public sealed class UsersController : CrudControllerBase<User>
{
    public UsersController(WebHookContext dbContext) : base(dbContext) { }
}