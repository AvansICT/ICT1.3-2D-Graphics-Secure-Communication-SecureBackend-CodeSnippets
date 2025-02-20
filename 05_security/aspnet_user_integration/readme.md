# Using the Currently Logged-In User in Your Code  

When reading or adding entities, you may need to determine which user is currently authenticated against the API. The snippets in this folder will help you achieve this.  

## Components  

### `AspNetIdentityAuthenticationService : IAuthenticationService`  
This class contains the logic to retrieve the currently authenticated user from the ASP.NET Identity Framework Middleware.  
- It implements `IAuthenticationService`, allowing you to mock this authentication logic in unit tests.  

### `Program.cs`  
Add the provided snippet to `Program.cs` to configure your dependency injection container. Not doing this will result in error at startup.

## Implementation Steps  

1. Add the `IAuthenticationService`, `AspNetIdentityAuthenticationService` to a folder named `Services` in your WebApi project. 
2. Change the `Program.cs` to register this service into the Dependency Injection container.
3. Inject `IAuthenticationService` into the `ApiController` where you need access to the current user.  
4. Call `GetCurrentAuthenticatedUserId()` to retrieve the currently authenticated user ID.  
5. Use the retrieved user ID to filter or extend your queries.  
