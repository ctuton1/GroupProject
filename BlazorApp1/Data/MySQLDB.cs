using MySql.Data.MySqlClient;

namespace BlazorApp1.Data
{
    public class MySQLDB
    {
        string cs = @"server=localhost;userid=root;password=Team@SE3;database=societydb";
        MySqlConnection con;
        public MySQLDB()
        {
            con = new MySqlConnection(cs);
            con.Open();
        }
        ProfileBioForm GetProfileBio(int userId)
        {
            ProfileBioForm form = new ProfileBioForm();

            

            string sql = string.Format("SELECT userbio FROM userdetails WHERE userid={0};", userId);

            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            form.profileBio = rdr.GetString(0);

            return form;
        }

        public void UpdateUserPhoto(string userPhotoName, int userId)
        {
            //Write user photo name to db
        }

        public bool CreateUser(string userName, string password, string fName, string lName, string accountType)
        {
            int accountTypeID = 0;
            switch(accountType.ToLower())
            {
                case "student":
                    accountTypeID = 3;
                break;
                case "society":
                    accountTypeID = 2;
                    break;
                default:
                    return false;
            }

            string sql = string.Format("INSERT INTO userDetails (firstName, lastName, accountTypeID) VALUES ({0}, {1}, {2};", fName, lName, accountTypeID);
            MySqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
            long userId = command.LastInsertedId;
            sql = string.Format("INSERT INTO userLogin (userID, username, userPassword) VALUES ({0}, {1}, {2};", userId, userName, password);
            MySqlCommand command2 = con.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
            return true;

        }
        public void UpdateUserBio(string userBio, int userId)
        {
            //Write user bio to db
        }

        public void UpdateUsername(string username, int userId)
        {
            //Write new username to db
        }

        public void UpdatePassword(string password, int userId)
        {
            //Write new password to db
        }

        public void CloseConnection()
        {
            con.Close();
        }
    }
}
