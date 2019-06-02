using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using GitHubUserSearch.Dtos;
using GitHubUserSearch.Services;

namespace GitHubUserSearch.UI.Controllers
{
    public class ApiController : Controller
    {
        private readonly GitHubService _gitHubService;

        public ApiController(GitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet]
        public async Task<ActionResult> UserByUserName(string userName)
        {
            var validateUserNameResult = GitHubUserNameValidator.Validate(userName);
            if (validateUserNameResult.IsFailure)
                return HttpNotFound();

            var maybeUser = await _gitHubService.GetUser(userName);

            if (maybeUser.HasNoValue)
                return HttpNotFound();

            return Json(maybeUser.Value, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> RepositoriesByUserName(string userName)
        {
            var validateUserNameResult = GitHubUserNameValidator.Validate(userName);
            if (validateUserNameResult.IsFailure)
                return Json(new List<RepositoryDto>(), JsonRequestBehavior.AllowGet);

            var repositories = await _gitHubService.GetRepositories(userName);

            return Json(repositories, JsonRequestBehavior.AllowGet);
        }
    }
}
