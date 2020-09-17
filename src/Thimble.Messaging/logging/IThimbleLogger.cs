using System;

namespace Thimble.Messaging.logging
{
    public interface IThimbleLogger
    {
        void Log(string action);
        void Log(string userId, string action);
        void Log(Exception exception, string action);
        void SetTraceId(string traceId);
    }
}