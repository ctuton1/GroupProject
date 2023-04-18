using MySql.Data.MySqlClient;

namespace BlazorApp1.Data
{
    public class MySQLDB
    {
        string cs = @"server=localhost;userid=root;password=Team@SE3;database=societydb";
        MySqlConnection con;
        public MySQLDB()
        {
            
        }

        private void OpenConnection()
        {
            con = new MySqlConnection(cs);
            con.Open();
        }

        ProfileBioForm GetProfileBio(int userId)
        {
            OpenConnection();

            ProfileBioForm form = new ProfileBioForm();

            

            string sql = string.Format($"SELECT userbio FROM userdetails WHERE userid='{userId}';");

            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            form.profileBio = rdr.GetString(0);

            CloseConnection();

            return form;
        }

        public void UpdateUserPhoto(string userPhotoName, int userId)
        {
            OpenConnection();

            string sql = string.Format($"UPDATE userdetails SET userphoto = '{userPhotoName}' where userid = '{userId}';");
            MySqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
            CloseConnection();
        }

        public int SignIn(string userName)
        {
            OpenConnection();

            string sql = string.Format($"SELECT UserID FROM userlogin WHERE Username = '{userName}';");
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                string s = rdr.GetString(0);
                CloseConnection();
                return Convert.ToInt32(s);
            }
            return -1;
        }

        public bool PasswordCheck(string userName, string password)
        {
            OpenConnection();

            try
            {
                string pCSQL = string.Format($"SELECT Username FROM userlogin WHERE Username = '{userName}' AND UserPassword = '{password}';");
                var cmd = new MySqlCommand(pCSQL, con);

                MySqlDataReader rdr = cmd.ExecuteReader();
                CloseConnection();
                return true;
            }
            catch
            {
                CloseConnection();
                return false;
            }
        }

        public bool CreateUser(CreateAccountModel cA)
        {
            OpenConnection();

            int accountTypeID = 0;
            switch(cA.accountType.ToLower())
            {
                case "student":
                    accountTypeID = 6; //change for server
                break;
                case "society":
                    accountTypeID = 5; //change for server
                    break;
                default:
                    return false;
            }

            string sql = string.Format($"INSERT INTO userdetails (firstName, lastName, userTypeID) VALUES ('{cA.firstName}', '{cA.lastName}', '{accountTypeID}');");
            MySqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
            long userId = command.LastInsertedId;
            sql = string.Format($"INSERT INTO userlogin (userID, username, userPassword) VALUES ('{userId}', '{cA.userName}', '{cA.password}');");
            MySqlCommand command2 = con.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
            CloseConnection();
            return true;
            
        }
        public void UpdateUserBio(string userBio, int userId)
        {
            //Write user bio to db
            OpenConnection();

            CloseConnection();
        }

        public void UpdateUsername(string username, int userId)
        {
            //Write new username to db
            OpenConnection();

            CloseConnection();
        }

        public void UpdatePassword(string password, int userId)
        {
            //Write new password to db
            OpenConnection();

            CloseConnection();
        }

        public string GetProfilePhoto(int userId)
        {
            OpenConnection();
            string sql = string.Format($"SELECT UserPhoto FROM userdetails WHERE UserId = '{userId}';");
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            string s = "";
            if (rdr.Read())
            {
                try
                {
                    s = rdr.GetString(0);
                }
                catch
                {
                    s = "default.png";
                }
            }
            CloseConnection();
            return s;
        }

        public void CloseConnection()
        {
            con.Close();
        }
    }
}
