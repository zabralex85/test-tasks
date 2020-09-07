using System;

namespace Medialink.Domain
{
    public class RequestLogDbModel
    {
        public int Id { get; set; }
        public string LogMethod { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
