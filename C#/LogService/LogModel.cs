using Example.Models.Base;
using System;

namespace Example.Models
{
    public class LogModel : BaseModel
    {
        public long Id { get; set; }
        public string Level { get; set; }
        public string Module { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public int UserId { get; set; }
    }
}
