using System;

namespace WebHooks.Actions.Base
{
    public abstract class ActionBase
    {
        public DateTime EventTimestampUtc { get; set; } = DateTime.UtcNow;
    }
}
