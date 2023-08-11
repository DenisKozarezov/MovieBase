namespace Core.Security
{
    interface IPasswordHasher
    {
        string CreateHash(HashType hashType);
    }
}