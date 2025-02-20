# Using the Currently Logged-In User in Your Code  

When reading or adding entities, you may need to determine which user is currently authenticated against the API. The snippets in this folder will help you achieve this.  

## Components  

### `AspNetIdentityUserRepository : IUserRepository`  
This class contains the logic to retrieve the currently authenticated user from the ASP.NET Identity Framework Middleware.  
- It implements `IUserRepository`, allowing you to mock authentication logic in unit tests.  

### `Program.cs`  
Add the provided snippet to `Program.cs` to configure your dependency injection container.  

## Implementation Steps  

1. Inject `IUserRepository` into the `ApiController` where you need access to the current user.  
2. Call `GetCurrentUserId()` to retrieve the currently authenticated user ID.  
3. Use the retrieved user ID to filter or extend your queries.  
