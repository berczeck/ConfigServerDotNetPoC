using System.Runtime.Serialization;

namespace ConfigServerHost.Core.Domain
{
    [DataContract]
    public class ServiceError
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Message { get; set; }

        public static ServiceError Create(Error error)
        {
            return new ServiceError { Code = error.Code, Message = error.Message };
        }
    }    
}
