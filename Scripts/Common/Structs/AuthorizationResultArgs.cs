namespace Core.Query
{
    public struct AuthorizationResultArgs
    {
        public int UserID;
        public string Login;
        public string Password;
        public string FIO;
        public AccessType AccessType;
        public UserStatus UserStatus;
        public AuthorizationStatus AuthorizationStatus;
        public int CollectionID;

        public static AuthorizationResultArgs Empty => new AuthorizationResultArgs
        {
            UserID = -1,
            Login = string.Empty,
            Password = string.Empty,
            FIO = string.Empty,
            AccessType = AccessType.NULL,
            UserStatus = UserStatus.NULL,
            AuthorizationStatus = AuthorizationStatus.Blocked,
            CollectionID = - 1
        };

        public bool IsAuthorized => AuthorizationStatus == AuthorizationStatus.Authorized && UserStatus == UserStatus.Свободен;
    }
}