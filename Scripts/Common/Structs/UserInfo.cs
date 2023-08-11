namespace Core.Query
{
    public struct UserInfo
    {
        public int ID;
        public string Login;
        public string Password;
        public string FIO;
        public AccessType AccessType;
        public UserStatus UserStatus;
    }
}