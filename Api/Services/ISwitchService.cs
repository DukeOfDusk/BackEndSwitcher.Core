using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface ISwitchService
    {
        string TransformTemplate(string templatePath, MatchResult matches);
        void SaveChanges(string destinationPath, string content);
        List<Match> ParseTemplate(string filepath);
        MatchResult MatchPlaceholders(Dictionary<string, string> envPlaceholders, List<Match> appPlaceholders);
    }
}
