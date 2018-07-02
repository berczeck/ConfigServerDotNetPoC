using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ConfigServerHost.Core.Domain
{
    [DataContract]
    public class ServiceOperationResult<T> : ServiceOperationResult
    {
        [DataMember]
        public T Value { get; set; }
    }

    [DataContract]
    public class ServiceOperationResult
    {
        [DataMember]
        public bool Failure { get; set; }
        [DataMember]
        public bool Success { get; set; }
        [DataMember]
        public List<ServiceError> ErrorList { get; set; }

        public ServiceOperationResult()
        {
            ErrorList = new List<ServiceError>();
        }

        public static ServiceOperationResult Create(OperationResult result)
        {
            return new ServiceOperationResult
            {
                Success = result.Success,
                Failure = result.Failure,
                ErrorList = result.ErrorList?.Select(x => ServiceError.Create(x)).ToList()
            };
        }

        public static ServiceOperationResult<R> Create<R>(OperationResult<R> result)
        {
            return new ServiceOperationResult<R>
            {
                Success = result.Success,
                Failure = result.Failure,
                ErrorList = result.ErrorList?.Select(x => ServiceError.Create(x)).ToList(),
                Value = result.Value
            };
        }
    }
}
