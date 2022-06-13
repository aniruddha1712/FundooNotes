using System;
using System.Collections.Generic;
using System.Text;

namespace FundooCommonLayer
{
    public class ResponseModel<T>
    {
        public bool Status { get; set; }


        public string Message { get; set; }


        public T Data { get; set; }
    }
}
