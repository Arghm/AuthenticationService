namespace AuthenticationService.Contracts.Handlers
{
    /// <summary>
    /// Password handler.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashing password.
        /// </summary>
        string Hash(string password);

        /// <summary>
        /// Verifies incoming and current password.
        /// </summary>
        /// <param name="verifiedPassword">Password to verify</param>
        /// <param name="currentPassword">Password hash in DB</param>
        bool IsValid(string password, string hashedPassword);
    }
}
