using System;

namespace Example.Model
{
    public class Log : BaseModel
    {
        public string Location { get; set; }
        public string ShortMessage { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public string UserID { get; set; }
    }
}
