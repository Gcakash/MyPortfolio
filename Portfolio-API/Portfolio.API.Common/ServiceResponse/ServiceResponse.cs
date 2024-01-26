using Portfolio.API.Common.Enum;
using System;
using System.Threading.Tasks;

namespace Portfolio.API.Common.ServiceResponse
{
    public class ServiceResponse<T>
    {
        public T Result { get; set; }

        public ServiceResponseTypes Type { get; set; }

        public String DisplayMessage { get; set; }

        public String ErrorCode { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public string ErrorMessage
        {
            get
            {
                return Errors != null && Errors.Any() ? string.Join(", ", Errors) : string.Empty;
            }
        }
    }
}
