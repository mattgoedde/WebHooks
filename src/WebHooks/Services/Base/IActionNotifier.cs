using System.Threading;
using System.Threading.Tasks;
using WebHooks.Actions;

namespace WebHooks.Services.Base
{
    public interface IActionNotifier
    {
        public Task NotifyAsync<TAction>(TAction action) where TAction : BaseAction;
    }
}
