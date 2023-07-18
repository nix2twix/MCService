using MCService.Models;
using MCService.Database;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace MCService.Services
{
    public class UserService
    {
        private readonly SqlDriver sqlDriver;
        public UserService(SqlDriver sqlDriver = null)
        {
            this.sqlDriver = sqlDriver;
            this.sqlDriver.Open();
        }

        ~UserService() 
        { 
            sqlDriver.Close();
        }
        public string AddNewUser(UserModel newModel)
        {
            var passwordHash = GetHashedKey(newModel.Password);

            return sqlDriver.ExecNonQuery("INSERT INTO Users"
                + "(email, login, passwordHash, roleCode)"
                + $"VALUES(N'{newModel.Email}', N'{newModel.Login}', N'{passwordHash}', 1)")
                + " users was added successfully!";
        }

        public string DeleteUserByID(int id)
        {
            return sqlDriver.ExecNonQuery($"DELETE FROM Users WHERE id = {id}")
                + " users was deleted successfully!";
        }

        public UserModel ChangeUserByID(int id, UserModel newModel)
        {
            //хэширование нового пароля для хранения хэша в БД
            var passwordHash = GetHashedKey(newModel.Password);
                
            sqlDriver.ExecNonQuery($"UPDATE Users\n"
                + $"SET email = N'{newModel.Email}',\n"
                + $"login = {newModel.Login},\n"
                + $"passwordHash = {passwordHash},\n"
                + $"roleCode = N'{newModel.Role}'\n"
                + $"WHERE id = {id}");

            return newModel;

        }

        public UserModel GetUserByID(int id)
        {
            var userInfo = sqlDriver.ExecReader($"SELECT * "
                + $"FROM Users WHERE id = {id}");
            userInfo.Read();

            return new UserModel
            {
                Email = userInfo["email"].ToString(),
                Login = userInfo["login"].ToString(),
                Password = userInfo["passwordHash"].ToString(),
                Role = (UserModel.UserRole)userInfo["roleCode"],
            };
        }

        static string GetHashedKey(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder result = new StringBuilder();

                foreach (byte b in data)
                    result.Append(b.ToString("X2"));

                return result.ToString();
            }
        }

    }
}
