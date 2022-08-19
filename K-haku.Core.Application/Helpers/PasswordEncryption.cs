using System.Security.Cryptography;
using System.Text;

namespace K_haku.Core.Application.Helpers
{
    public class PasswordEncryption
    {
        public static string ComputeSha256Hash(string password)
        {
            //create Sha256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                
                //Convert byte array to string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
