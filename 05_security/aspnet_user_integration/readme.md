# Using the Currently Logged-In User in Your Code  

When reading or adding entities, you may need to determine which user is currently authenticated against the API. The snippets in this folder will help you achieve this.  

## Components  

### `AspNetIdentityAuthenticationService : IAuthenticationService`  
This class contains the logic to retrieve the currently authenticated user from the ASP.NET Identity Framework Middleware.  
- It implements `IAuthenticationService`, allowing you to mock this authentication logic in unit tests.  

### `Program.cs`  
Add the provided snippet to `Program.cs` to configure your dependency injection container. Not doing this will result in error at startup.

## Implementation Steps  

1. Inject `IAuthenticationService` into the `ApiController` where you need access to the current user.  
2. Call `GetCurrentAuthenticatedUserId()` to retrieve the currently authenticated user ID.  
3. Use the retrieved user ID to filter or extend your queries.  
