using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfigServerHost.Core.Domain
{
    public class OperationResult
    {
        public bool Failure { get { return !Success; } }
        public bool Success { get; }
        public List<Error> ErrorList { get; }
        protected OperationResult(bool success)
        {
            Success = success;
        }
        protected OperationResult(Error error)
        {
            ErrorList = new List<Error> { error };
        }

        protected OperationResult(List<Error> errorList)
        {
            ErrorList = errorList ?? new List<Error>();
        }

        public override string ToString()
        {
            var message = Failure ? ErrorList?.Select(x => x.ToString()).Aggregate((i, j) => $"{i}{Environment.NewLine}{j}") : "None";
            return $"{{ Sucess: {Success}, Error: [{message}] }}";
        }

        public static OperationResult Create(ServiceOperationResult result)
        {
            return result.Success ? Create() : CreateError(result.ErrorList?.Select(x => Error.Create(x)).ToList());
        }

        public static OperationResult<R> Create<R>(ServiceOperationResult<R> result)
        {
            return result.Success ?
                Create(result.Value) :
                CreateError<R>((result.ErrorList?.Select(x => Error.Create(x)).ToList()));
        }
        
        public static OperationResult CreateError(string errorCode, string errorMessage)
           => new OperationResult(new Error(errorCode, errorMessage));

        public static OperationResult CreateError(OperationResult result)
            => new OperationResult(result.ErrorList);

        public static OperationResult CreateError(List<Error> errorList)
            => new OperationResult(errorList);

        public static OperationResult CreateError(Error error)
            => new OperationResult(new List<Error> { error });

        public static OperationResult Create()
            => new OperationResult(true);

        public static OperationResult<R> CreateError<R>(string errorCode, string errorMessage)
            => new OperationResult<R>(new Error(errorCode, errorMessage));

        public static OperationResult<R> CreateError<R>(List<Error> errorList)
            => new OperationResult<R>(errorList);

        public static OperationResult<R> CreateError<R>(OperationResult<R> result)
            => new OperationResult<R>(result.ErrorList);

        public static OperationResult<R> CreateError<R>(OperationResult result)
            => new OperationResult<R>(result.ErrorList);

        public static OperationResult<R> CreateError<R>(Error error)
            => new OperationResult<R>(error);

        public static OperationResult<R> Create<R>(R value)
            => new OperationResult<R>(true, value);

        public static OperationResult CreateError(List<Error> errorList, Error error)
        {
            errorList.Add(error);
            return CreateError(errorList);
        }

        public static OperationResult<R> CreateError<R>(List<Error> errorList, Error error)
        {
            errorList.Add(error);
            return CreateError<R>(errorList);
        }

        public static OperationResult CreateError(Exception exception, string errorCode, string errorMessage = null)
        {
            var exceptionList = exception.GetType() == typeof(AggregateException)
                ? ((AggregateException)exception).FilterValidExceptions()
                : exception.GetAllInnerExceptions();

            var errorList = new List<Error>();
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                errorList.Add(new Error(errorCode, errorMessage));
            }

            errorList.AddRange(exceptionList.Select(x => new Error(errorCode, x)));

            return CreateError(errorList);
        }

        public static OperationResult<R> CreateError<R>(Exception exception, string errorCode, string errorMessage = null)
        {
            return CreateError<R>(CreateError(exception, errorCode, errorMessage));
        }

        public static OperationResult CreateError(params OperationResult[] operationResult)
        {
            var errorList = operationResult.SelectMany(x => x.ErrorList).ToList();
            return CreateError(errorList);
        }

        public static OperationResult<R> CreateError<R>(params OperationResult[] operationResult)
        {
            var errorList = operationResult.SelectMany(x => x.ErrorList).ToList();
            return CreateError<R>(errorList);
        }
    }

    public static class OperationResultExtensions
    {
        public static OperationResult AsError(this OperationResult result, string errorCode, string errorMessage)
        {
            var errorList = result.ErrorList ?? new List<Error>();
            errorList.Add(new Error(errorCode, errorMessage));
            return OperationResult.CreateError(errorList);
        }

        public static OperationResult<R> AsError<R>(this OperationResult result, string errorCode, string errorMessage)
        {
            var errorList = result.ErrorList ?? new List<Error>();
            errorList.Add(new Error(errorCode, errorMessage));
            return OperationResult.CreateError<R>(errorList);
        }

        public static OperationResult<R> AsError<R>(this OperationResult result)
        {
            return OperationResult.CreateError<R>(result.ErrorList);
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Value { get; }

        internal OperationResult(Error error) : base(error) { }

        internal OperationResult(List<Error> errorList) : base(errorList) { }

        internal OperationResult(bool success) : base(success) { }
        internal OperationResult(bool success, T value) : base(success)
        {
            Value = value;
        }
    }
}
