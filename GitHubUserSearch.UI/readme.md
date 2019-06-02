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

### To-do

* Logging
* Use pagination to bring back all repositories
* Abstract GitHubService to generic SourceControlService?
* Split out `index.ts` into separate classes