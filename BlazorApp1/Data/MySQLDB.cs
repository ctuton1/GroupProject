using MySql.Data.MySqlClient;

namespace BlazorApp1.Data
{
    public class MySQLDB
    {
        string cs = @"server=localhost;userid=root;password=Team@SE3;database=societydb";
        MySqlConnection con;
        public MySQLDB()
        {
            ClearOldEvents();
        }

        private void OpenConnection()
        {
            con = new MySqlConnection(cs);
            con.Open();
        }

        public ProfileBioForm GetProfileBio(int userId)
        {
            OpenConnection();

            ProfileBioForm form = new ProfileBioForm();

            

            string sql = string.Format($"SELECT userbio FROM userdetails WHERE userid='{userId}';");

            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();
            if(rdr.Read())
            {
                if(!rdr.IsDBNull(0))
                {
                    form.profileBio = rdr.GetString(0);
                }
                else
                {
                    form.profileBio = "";
                }
            }

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
            CloseConnection();
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
                if(rdr.HasRows)
                {
                    CloseConnection();
                    return true;
                }
                else
                {
                    CloseConnection();
                    return false;
                }
                
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
                    CloseConnection();
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
            OpenConnection();

            string sql = string.Format($"UPDATE userdetails SET userBio = '{userBio}' where userid = '{userId}';");
            MySqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
            CloseConnection();
        }

        public string UpdateUsername(string username, int userId)
        {
            if (CheckIfUsernameExists(username))
            {
                CloseConnection();
                return "Username exists";
            }
            OpenConnection();
            string sql = string.Format($"UPDATE UserLogin SET username = '{username}' where userid = '{userId}';");
            MySqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
            CloseConnection();
            return "updated";
        }

        public string GetName(int userId)
        {
            OpenConnection();
            string sql = string.Format($"SELECT FirstName, LastName FROM UserDetails Where UserId = '{userId}';");
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            string firstName = "";
            string lastName = "";
            if (rdr.Read())
            {
                firstName = rdr.GetString(0);
                lastName = rdr.GetString(1);
            }
            CloseConnection();
            return firstName + " " + lastName;
        }

        public bool CheckIfUsernameExists(string username)
        {
            OpenConnection();
            string sql = string.Format($"SELECT Username FROM UserLogin Where Username = '{username}';");
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            if(rdr.Read())
            {
                if (!rdr.IsDBNull(0))
                {
                    CloseConnection();
                    return true;
                }
                else
                {
                    CloseConnection();
                    return false;
                }
            }
            CloseConnection();
            return false;
        }

        public string GetUserName(int userId)
        {
            OpenConnection();
            string sql = string.Format($"SELECT Username FROM UserLogin Where UserId = '{ userId}';");
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            string UserName = "";
            if (rdr.Read())
            {
                UserName = rdr.GetString(0);
            }
            CloseConnection();
            return UserName;
        }

        public string GetPassword(int userId)
        {
            OpenConnection();
            string sql = string.Format($"SELECT userPassword FROM UserLogin Where UserId = '{ userId}';");
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            string Password = "";
            if (rdr.Read())
            {
                Password = rdr.GetString(0);
            }
            CloseConnection();
            return Password;
        }

        public string GetAccountType(int userId)
        {
            OpenConnection();
            string sql = string.Format($"SELECT b.UserTypeTitle FROM UserDetails as a INNER JOIN UserTypes as b on a.UserTypeID = b.UserTypeID Where a.UserId = '{ userId}';");
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            string result = "";
            if (rdr.Read())
            {
                result = rdr.GetString(0);
            }
            CloseConnection();
            return result;
        }

        public void UpdatePassword(string password, int userId)
        {
            OpenConnection();

            string sql = string.Format($"UPDATE UserLogin SET userpassword = '{password}' where userid = '{userId}';");
            MySqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
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
                if(!rdr.IsDBNull(0))
                {
                    s = rdr.GetString(0);
                }
                else
                {
                    s = "default.png";
                }
            }
            CloseConnection();
            return s;
        }

        public List<SocietyBarModel> GetAllSocieties()
        {
            OpenConnection();
            List<SocietyBarModel> result = new List<SocietyBarModel>();
            List<int> userId = new List<int>();
            string sql = string.Format($"SELECT a.userid from Userdetails AS a INNER JOIN Usertypes AS b on a.UserTypeId = b.UserTypeId WHERE b.Usertypetitle = 'Society';");
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            int count = rdr.FieldCount;
            while (rdr.Read())
            {
                for(int i = 0; i < count; i++)
                {
                    userId.Add(Convert.ToInt32(rdr.GetString(0)));
                }
            }
            CloseConnection();
            foreach (int iD in userId)
            {
                SocietyBarModel temp = new SocietyBarModel();
                temp.Id = iD;
                temp.UserName = GetUserName(iD);
                temp.Bio = GetProfileBio(iD).profileBio;
                temp.Photo = $"userphotos\\{GetProfilePhoto(iD)}";
                result.Add(temp);
            }
            return result;
        }

        public void AddEvent(EventDataModel eDM)
        {
            OpenConnection();
            string sql = string.Format($"INSERT INTO events (EventName, EventDate, EventDescription, EventOwner) Values ('{eDM.Name}','{eDM.Date.ToString("yyyy-MM-dd HH:mm:ss.fff")}','{eDM.Description}','{eDM.EventOwnerId}');");
            MySqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
            CloseConnection();
        }

        public List<EventDataModel> GetSocietyEvents(int iD)
        {
            OpenConnection();
            List<EventDataModel> result = new List<EventDataModel>();
            string sql = string.Format($"Select * From Events WHERE EventOwner = '{iD}'");
            var cmd = new MySqlCommand(sql, con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            int count = rdr.FieldCount;
            while (rdr.Read())
            {
                    EventDataModel temp = new EventDataModel();
                    temp.Id = Convert.ToInt32(rdr.GetValue(0));
                    temp.Name = rdr.GetString(1);
                    temp.Date = rdr.GetDateTime(2);
                    temp.Description = rdr.GetString(3);
                    temp.EventOwnerId = Convert.ToInt32(rdr.GetValue(4));

                    result.Add(temp);
            }
            CloseConnection();
            return result;
        }

        public List<EventDataModel> GetAllEvents()
        {
            OpenConnection();
            List<EventDataModel> result = new List<EventDataModel>();
            string sql = string.Format($"Select * From Events;");
            var cmd = new MySqlCommand(sql, con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            int count = rdr.FieldCount;
            while (rdr.Read())
            {
                EventDataModel temp = new EventDataModel();
                temp.Id = Convert.ToInt32(rdr.GetValue(0));
                temp.Name = rdr.GetString(1);
                temp.Date = rdr.GetDateTime(2);
                temp.Description = rdr.GetString(3);
                temp.EventOwnerId = Convert.ToInt32(rdr.GetValue(4));

                result.Add(temp);
            }
            CloseConnection();
            return result;
        }

        public void AttendEvent(int eventId, int userId)
        {
            OpenConnection();
            string sql = string.Format($"INSERT INTO UserEvents (UserID, EventID) VALUES ('{userId}', '{eventId}');");
            MySqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
            CloseConnection();
        }

        public bool CheckUserEvent(int eventId, int userId)
        {
            OpenConnection();
            string sql = string.Format($"SELECT eventId FROM userevents WHERE UserId = '{userId}' AND eventId = '{eventId}';");
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (!rdr.IsDBNull(0))
                {
                    CloseConnection();
                    return true;
                }
                else
                {
                    CloseConnection();
                    return false;
                }
            }
            CloseConnection();
            return false;
        }

        private void ClearOldEvents()
        {
            OpenConnection();
            DateTime now = DateTime.Now;
            string sql = string.Format($"DELETE From Events Where EventDate < now();");
            MySqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
            CloseConnection();
        }

        public void CloseConnection()
        {
            con.Close();
        }
    }
}
