using System.Collections.Generic;

namespace Api.Models
{
    public class MatchResult
    {
        public Dictionary<string, string> Success { get; set; }
        public List<string> Failures { get; set; }

        public MatchResult()
        {
            Success = new Dictionary<string, string>();
            Failures = new List<string>();
        }
    }
}
