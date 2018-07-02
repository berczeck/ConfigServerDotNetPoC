using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfigServerHost.Core.Domain
{
    public static class ExceptionExtension
    {
        public static IEnumerable<Exception> FilterValidExceptions(this AggregateException ae)
        {
            var finalList = new List<Exception> { ae };
            var asyncExceptions = ae.Flatten().InnerExceptions.Where(x => x.GetType() != typeof(AggregateException));

            foreach (var item in asyncExceptions)
            {
                var innerExceptionList = GetInnerExceptionList(item);
                finalList.AddRange(innerExceptionList);
            }

            return finalList;
        }

        private static List<Exception> GetInnerExceptionList(Exception ex)
        {
            var innerExceptionList = new List<Exception>();
            Exception currentEx = ex;
            while (currentEx.InnerException != null)
            {
                innerExceptionList.Add(currentEx);
                currentEx = currentEx.InnerException;
            }
            return innerExceptionList;
        }

        public static IEnumerable<Exception> GetAllInnerExceptions(this Exception ex)
        {
            var finalList = new List<Exception>();
            Exception currentEx = ex;
            while (currentEx != null)
            {
                finalList.Add(currentEx);
                currentEx = currentEx.InnerException;
            }
            return finalList;
        }
    }
}
