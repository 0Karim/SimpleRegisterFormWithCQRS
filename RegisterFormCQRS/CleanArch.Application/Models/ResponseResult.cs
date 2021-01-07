using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanArch.Application.Models
{
    public class ResponseResult
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }

        public string Msg { set; get; }

        public ResponseResult() { }

        public ResponseResult(bool succeeded, IEnumerable<string> errors, string Msg)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            this.Msg = Msg;
        }


        public static ResponseResult Success(string Msg)
        {
            return new ResponseResult(true, new string[] { }, Msg);
        }

        public static ResponseResult Failure(IEnumerable<string> errors, string Msg)
        {
            return new ResponseResult(false, errors, Msg);
        }
    }
}
