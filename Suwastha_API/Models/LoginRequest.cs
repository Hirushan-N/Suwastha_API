using Suwastha_API.Configs;

namespace Suwastha_API.Models
{
    public class LoginRequest
    {
        public string? User { get; set; }
        public string? Key { get; set; }

        public byte[] getEncryptedKey()
        {
            return PasswordManager.Encrypt(this.Key);
        }
    }
}
