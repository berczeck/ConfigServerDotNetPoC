using System;

namespace ConfigServerHost.Core.Domain
{
    public class Error
    {
        public string Code { get; }
        public string Message { get; }
        public ErrorTrace Trace { get; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public Error(string code, Exception exception) : this(code, exception.Message)
        {
            Trace = new ErrorTrace(exception);
        }

        public Error(string code, string message, Exception exception) : this(code, message)
        {
            Trace = new ErrorTrace(exception);
        }

        public static Error Create(ServiceError error)
        {
            return new Error(error.Code, error.Message);
        }

        public override string ToString()
        {
            return $"{{ Code: \"{Code}\", Message: \"{Message}\", Trace: \"{Trace}\" }}";
        }
    }

    public class ErrorTrace
    {
        public string Message { get; }
        public string StackTrace { get; }
        public string Type { get; }

        public ErrorTrace(Exception exception)
        {
            if (exception == null)
            {
                return;
            }
            Message = exception.Message;
            var stackTrace = exception.StackTrace;
            if (!string.IsNullOrEmpty(stackTrace))
            {
                var stackTraceLenght = stackTrace.Length;
                const int maxCapacity = 500;
                StackTrace = stackTraceLenght > maxCapacity ? stackTrace.Substring(stackTraceLenght - maxCapacity) : stackTrace;
            }
            Type = exception.GetType().ToString();
        }

        public override string ToString()
        {
            return $"{{ Type: \"{Type}\", Message: \"{Message}\", StackTrace: \"{StackTrace}\" }}";
        }
    }
}
