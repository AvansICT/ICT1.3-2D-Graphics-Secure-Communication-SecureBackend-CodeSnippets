public interface IUserRepository
{
    /// <summary>
    /// Returns the user name of the authenticated user
    /// </summary>
    /// <returns></returns>
    string? GetCurrentUserId();
}

