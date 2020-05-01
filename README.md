# BlogRepository
Web Api with Angular.

Technical task:
Create a simple web site for users to create/submit articles and post comments. Add user management and authentication.
Start from back-end implementation!

**Back-end

Complete application must meet the following criterias:

Application should be written with .NET CORE 3.x by using REST architecture (use HTTP and JSON) as a microservice;
Authentication (choose any method). Only authenticated users can post articles and comments;
Any user can view content. User information should consist of UserName, Email and Password. Not allowed to create two users with identical name or email;    --
Article length can't be more than 2000 symbols. Article required fields: Title, CreatedUser, CreatedDate, *Category;
Comment length is limited to 200 symbols;
User can't create a post with already existing article title;

*Add articles search by partial title occurrence, user name or *category;

*Add functionality to restore forgotten passwords with email notification. Send new password to user.

P.S. Add middleware to handle exceptions.

Unit Testing (Required)

Cover back-end implementation with Unit Tests (any framework allowed: NUnit, Xunit, MSTest etc.);

*Add integrations tests for articles workflow;

**Add code coverage tool to check.


UI

Choose any familiar framework/library to achieve results as fast as possible (Angular, React, Vue, JQuery etc.);
Create 3 pages: Login Page, Articles list (just titles) and article view (whole post with comments below);
Add a current logged in user indication in the header;

*Add a search bar (see back-end part requirements). Replace an articles list with search results;

*Allow users to choose category of their posts (show category tag).

P.S. Don't spend too much time on UI. Create as simple as possible. Use existing layouts and libraries. If you don't have time - skip UI implementation and use Postman of Swagger instead.

DB

Use MongoDB (allowed to use any nuget packages for DB connection) to store all necessary information;
Provide CRUD operations for each document.


Other

Write documentation with public API explanation and usages, how to start your application and required environment;


Additional tasks:


**Deploy and run microservice inside Docker.


Legend


* - Additional requirements (implement main part first);

** - The lowest priority. Do other tasks first.


Fork this repo and send to me link to your own repository with implementation.

Deadline - MONDAY 6 PM 17th of February
Good luck!
