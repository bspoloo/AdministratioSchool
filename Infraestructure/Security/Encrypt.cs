namespace AdministratioSchool.Infraestructure.Security
{
    public class Encrypt
    {
        public static string EncryptPassword(string password)
        {
            if (BCryptHash.IsBCryptHash(password))
            {
                return password;
            }
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
