using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Models
{
    public class Response<T>
    {
        public T Value { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
    }
    public class ResponseStatus
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class ResponseListValue<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<T> List { get; set; }
    }
}
