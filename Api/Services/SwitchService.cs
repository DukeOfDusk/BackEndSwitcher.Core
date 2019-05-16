using Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Api.Services
{
    public class SwitchService : ISwitchService
    {
        public string TransformTemplate(string templatePath, MatchResult matches)
        {
            using (var reader = new StreamReader(templatePath))
            {
                string content = reader.ReadToEnd();

                foreach (var success in matches.Success)
                {
                    content = content.Replace(string.Format("{{{{{0}}}}}", success.Key), success.Value);
                }

                if (matches.Failures.Count > 0)
                {
                    foreach (var failure in matches.Failures)
                    {
                        content = content.Replace(string.Format("{{{{{0}}}}}", failure), "");
                    }
                }

                return content;
            }
        }

        public void SaveChanges(string destinationPath, string content)
        {
            if (System.IO.File.Exists(destinationPath))
            {
                System.IO.File.WriteAllText(destinationPath, content);
            }
        }

        public List<Match> ParseTemplate(string filepath)
        {
            using (var reader = new StreamReader(filepath))
            {
                string content = reader.ReadToEnd();
                Regex rx = new Regex(@"{{[a-z0-9.-]+}}");
                MatchCollection matches = rx.Matches(content);

                return matches.ToList();
            }
        }

        public MatchResult MatchPlaceholders(Dictionary<string, string> envPlaceholders, List<Match> appPlaceholders)
        {
            var result = new MatchResult();
            foreach (Match placeholder in appPlaceholders)
            {
                int length = placeholder.Value.Length - 4;
                var placeholderName = placeholder.Value.Substring(2, length);

                var match = envPlaceholders.FirstOrDefault(p => p.Key == placeholderName);

                if (match.Key != null)
                {
                    result.Success.Add(match.Key, match.Value);
                }
                else
                {
                    result.Failures.Add(placeholderName);
                }
            }

            return result;
        }

        public bool TestMethod()
        {
            throw new NotImplementedException();
        }
    }
}
