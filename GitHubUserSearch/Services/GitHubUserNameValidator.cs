using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace GitHubUserSearch.Services
{
    public class GitHubUserNameValidator
    {
        /// <summary>
        /// Validates a GitHub userName against the RegEx provided in this repo: https://github.com/shinnn/github-username-regex
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static Result Validate(string userName)
        {
            var pattern = "^[a-z\\d](?:[a-z\\d]|-(?=[a-z\\d])){0,38}$";
            var validUserName = Regex.IsMatch(userName, pattern, RegexOptions.IgnoreCase);

            return validUserName
                ? Result.Ok()
                : Result.Fail("Username doesn't match regular expression " + pattern);
        }
    }
}
