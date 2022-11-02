using System;
using System.Threading.Tasks;
using WebHooks.Actions.Base;

namespace WebHooks.Services.Base
{
    public interface IActionNotifier
    {
        public Task Notify<TAction>(TAction action) where TAction : ActionBase;
    }
}
