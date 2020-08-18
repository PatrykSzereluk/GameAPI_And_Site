namespace Security
{
    using System.Security.Cryptography;
    using System.Text;

    public class Encrypter
    {
        public string Encrypted(string password)
        {
            StringBuilder strBuilderPass = new StringBuilder();
            using (var sha512 = SHA512.Create())
            {
                var toEncryptData = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < toEncryptData.Length; i++)
                {
                    strBuilderPass.Append(toEncryptData[i].ToString("x2"));
                }
            }
            return strBuilderPass.ToString();
        }
    }
}
