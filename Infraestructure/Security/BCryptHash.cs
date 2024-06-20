namespace AdministratioSchool.Infraestructure.Security
{
    public class BCryptHash
    {
        public static bool IsBCryptHash(string password)
        {
            return password.StartsWith("$2a$") || password.StartsWith("$2b$") || password.StartsWith("$2y$");
        }
    }
}
