To compile the JavaScript/CSS files using webpack, run the following commands in the `GitHubUserSearch.UI` folder:

```
npm install
npm run build:prod
```

Webpack will create the `_Layout.cshtml` file from `_Layout_Template.cshtml` and inject the compiled css and js resources into it.

For convenience, you can install the [NPM Task Runner](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.NPMTaskRunner) extension, which will run the build command whenever you build in Visual Studio.

To run the UI tests, run the following commands in the `GitHubUserSearch.UI.Tests` folder, while the application is running on `http://localhost:52907/`:

```
npm install
npm test
```

The GitHub API allows 60 requests per hour. To increase this rate-limit to 5000 requests per hour, create `GitHubUserSearch.UI/secrets.config` with the following content:

```
<?xml version="1.0"?> 
<appSettings>
  <add key="GitHub:ClientId" value="YOUR_CLIENT_ID"/>
  <add key="GitHub:ClientSecret" value="YOUR_CLIENT_SECRET"/>
</appSettings>
```

The application will automatically detect this if present, and send the extra query parameters needed to increase the rate-limit.

### To-do

* Logging
* Use pagination to bring back all repositories
* Abstract GitHubService to generic SourceControlService?
* Split out `index.ts` into separate classes