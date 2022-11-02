using System;

namespace WebHooks.Actions
{
    public abstract class BaseAction
    {
        public DateTime EventTimestampUtc { get; set; } = DateTime.UtcNow;
    }
}
