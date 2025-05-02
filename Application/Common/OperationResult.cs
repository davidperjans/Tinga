using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; private set; }
        public T Data { get; private set; }
        public string Message { get; private set; }

        private OperationResult(bool isSuccess, T data, string message)
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
        }

        public static OperationResult<T> Success(T data, string message = null)
        {
            return new OperationResult<T>(true, data, message);
        }

        public static OperationResult<T> Failure(string message)
        {
            return new OperationResult<T>(false, default, message);
        }
    }
}
