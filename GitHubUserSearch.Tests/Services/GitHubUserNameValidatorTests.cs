using GitHubUserSearch.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace GitHubUserSearch.Tests.Services
{
    public class GitHubUserNameValidatorTests
    {
        [Theory]
        [InlineData("asd", true)]
        [InlineData("123456789012345678901234567890123456789012345678901234567890", false)]
        [InlineData("", false)]
        [InlineData("RobConery", true)]
        [InlineData("Rob Conery", false)]
        [InlineData("RobConery/repos", false)]
        public void Can_validate_user_names(string userName, bool isValid)
        {
            var result = GitHubUserNameValidator.Validate(userName);

            result.IsSuccess.Should().Be(isValid);
        }
    }
}
