using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Security
{
    internal class PasswordValidator : IPasswordValidator
    {
        private const byte PasswordMinLength = 8;
        private const bool AllowNullPassword = true;
        private readonly Regex[] Requirements = new Regex[]
        {
            new Regex(@"[0-9]+"),                          // Пароль содержит цифры
            new Regex(@"[A-Z]+"),                          // Пароль содержит символы верхнего регистра
            new Regex(@"[a-z]+"),                          // Пароль содержит символы нижнего регистра
            new Regex(@".{" + PasswordMinLength + ",}"),   // Минимальная длина пароля
            new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]"), // Пароль содержит специальные символы
            // new Regex(@"[0-9]+|[,.?;:-]+|[0-9]")        // В пароле чередуются цифры, знаки препинания и снова цифры 
            // ...
        };     
        
        public bool IsValid(string password)
        {
            if (!AllowNullPassword && string.IsNullOrEmpty(password)) return false;

            return Requirements.All(x => x.IsMatch(password));
        }
    }
}