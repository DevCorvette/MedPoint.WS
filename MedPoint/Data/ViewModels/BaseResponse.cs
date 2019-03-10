using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPoint.Data.ViewModels
{
    public interface BaseResponse { }

    public class BooleanResponse : BaseResponse
    {
        public bool Success { get; set; }
    }

    public class ErrorResponse : BooleanResponse
    {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
    }


}
