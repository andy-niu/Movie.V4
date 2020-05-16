using System;
using System.Collections.Generic;
using System.Text;

namespace M.Model
{
    public class ApiResult
    {

        public ApiResult() { }

        public ApiResult(ApiResultCode code, string msg = default(string), string msgcn = default(string), object data = default(object))
        {
            this.code = code;
            this.msg = msg;
            this.msgcn = msgcn;
            this.data = data;
        }
        public ApiResultCode code { get; set; }

        public string msg { get; set; }

        public string msgcn { get; set; }

        public object data { get; set; }
    }

    public class ApiResult<T> : ApiResult
    {
        public ApiResult()
        {
        }

        public ApiResult(ApiResultCode code, string msg, string msgcn, T data = default(T))
            : base(code, msg, msgcn, data)
        {
            this.data = data;
        }

        public new T data { get; set; }
    }

    public enum ApiResultCode
    {
        Success = 200,          // request success

        SystemError = 400,      // system error
        NoPayer = 402,      // no Payer error

        ValidationError = 420,  // validation error


    }
}
