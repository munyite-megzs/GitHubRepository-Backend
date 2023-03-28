# Introduction 
This project is a web application designed to view repository data fetched from the GitHub API. The application retrieves repositories, extracts the required fields, stores them in a SQL Server database, and then sends the repositories to the frontend for viewing. Users can view repositories sorted by updatedAt date, number of stars, or number of forks. Additionally, users can filter repositories by topic or language and sort them in ascending or descending order based on the provided criteria. The backend is built using ASP.NET Core Web API, while the frontend is developed using ASP.NET Core MVC. Both applications will be hosted on Azure App Service.

# Getting Started
Follow these steps to get the code up and running on your system:

# Installation process
1. Clone the repository using git clone <repository-url>
2. Navigate to the project folder and open the solution file in Visual Studio.

# Software dependencies
.NET 6.0 or higher
SQL Server (for the database)

# Configuration settings
Make sure to update the following settings in the appsettings.json file according to your environment:

* **ConnectionStrings:DefaultConnection** - Update with your database connection string.
* **GithubSettings:GitHubAccessToken** - Provide your GitHub Personal Access Token.
* **Jwt:Key, Jwt:Issuer, Jwt:Audience** - Update with appropriate values for JWT authentication.
* **GitHubDataFetcherSettings:** Settings for the scheduled service to fetch repositories from github
    * **Size**: Repository size in KBs
    * **Page**: Number of pages to be returned
    * **PerPage**: Number of items per page
    * **FetchIntervalInMinutes**: Periodic interval in hours

# Latest releases
Check the [releases](https://dev.azure.com/MicrosoftLeapClassroom/GitRepositoryTracker/_release) page for the latest version of the application.

# API references
Refer to the [GitHub API documentation](https://docs.github.com/en/rest?apiVersion=2022-11-28) for more information on the API used in this project.

# Build and Test
1. In Visual Studio, make sure you have the latest NuGet packages installed.
2. Set up a SQL Server database and update the connection string in the appsettings.json file accordingly.
3. Build the solution using Ctrl + Shift + B or Build > Build Solution.
4. Run the tests by navigating to Test > Run All Tests.
5. To run the application, press F5 or click Debug > Start Debugging.

# Contribute
We welcome contributions from the community to make this code better. If you'd like to contribute, please follow these steps:

1. Fork the repository.
2. Create a new branch with a descriptive name (e.g., feature-add-new-filter).
3. Commit your changes to the new branch.
4. Push the branch to your forked repository.
5. Create a pull request targeting the main branch of the original repository.

Please make sure to follow the code style and conventions used throughout the project. Add unit tests for your new features or bug fixes to ensure the stability and quality of the codebase.


If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)