using System;
using Core.Query;

namespace Core
{    
    public class User
    {
        public int UserID { private set; get; }
        public string Login { private set; get; }
        public string Password { private set; get; }
        public string FIO { set; get; }

        public UserStatus UserStatus { private set; get; }
            = UserStatus.Свободен;
        public AccessType AccessType { private set; get; }
            = AccessType.Гость;
        public AuthorizationStatus AuthorizationStatus { private set; get; } 
            = AuthorizationStatus.Blocked;

        public int CollectionID { private set; get; }

        public User(AuthorizationResultArgs args)
        {
            UserID = args.UserID;
            Login = args.Login;
            Password = args.Password;
            FIO = args.FIO;
            AccessType = args.AccessType;
            UserStatus = args.UserStatus;
            AuthorizationStatus = AuthorizationStatus.Authorized;
            CollectionID = args.CollectionID;
        }

        public static bool TryParseAccessType(string access, out AccessType result)
        {
            if (!Enum.TryParse(access, out result))
            {
                result = AccessType.NULL;
                return false;
            }
            else return true;
        }
        public static bool TryParseAccessType(object access, out AccessType result)
        {
            if (access == null)
            {
                result = AccessType.NULL;
                return false;
            }
            return TryParseAccessType(access.ToString(), out result);
        }
        public static bool TryParseUserStatus(string status, out UserStatus result)
        {
            if (!Enum.TryParse(status, out result))
            {
                result = UserStatus.NULL;
                return false;
            }
            else return true;
        }
        public static bool TryParseUserStatus(object status, out UserStatus result)
        {
            if (status == null)
            {
                result = UserStatus.NULL;
                return false;
            }
            return TryParseUserStatus(status.ToString(), out result);
        }

        /// <summary>
        /// Осуществляет выход из учетной записи и переходит обратно в окно 
        /// авторизации.
        /// </summary>
        public void LogOut()
        {
            AuthorizationStatus = AuthorizationStatus.Blocked;
        }
    }
}