namespace Core.Security
{
    interface IPasswordValidator
    {
        bool IsValid(string password);
    }
}