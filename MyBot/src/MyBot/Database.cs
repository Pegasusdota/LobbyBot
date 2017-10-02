using Discord;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Discord.WebSocket;
using System.Data;



namespace MyBot
{
    public class Database
    {


        private string table { get; set; }
        private const string server = "localhost";
        private const string database = "acidplayers";
        private const string username = "root";
        private const string password = "Chand13#";
        private MySqlConnection dbConnection;



        public Database(string table)
        {
            this.table = table;
            MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();
            stringBuilder.Server = server;
            stringBuilder.UserID = username;
            stringBuilder.Password = password;
            stringBuilder.Database = database;
            stringBuilder.SslMode = MySqlSslMode.None;

            var connectionString = stringBuilder.ToString();

            dbConnection = new MySqlConnection(connectionString);

            dbConnection.Open();
        }
        public MySqlDataReader FireCommand(string query)
        {
            if (dbConnection == null)
            {
                return null;
            }

            MySqlCommand command = new MySqlCommand(query, dbConnection);

            var mySqlReader = command.ExecuteReader();

            return mySqlReader;
        }
        public void CloseConnection()
        {
            if (dbConnection != null)
            {
                dbConnection.Close();
            }
        }
        public static void DeleteUserLobby(IUser user)
        {
            var database = new Database("acidplayers");
            var str = string.Format(@"DELETE FROM newgame WHERE user_id = '{0}'", user.Id);
            var table = database.FireCommand(str);
            database.CloseConnection();
        }

        public static void DeleteTable()
        {
            var database = new Database("acidplayers");
            string str = @"DELETE FROM newgame;";
            var table = database.FireCommand(str);
            database.CloseConnection();
            var database2 = new Database("acidplayers");
            string str2 = @"DELETE FROM team2;";
            var table2 = database2.FireCommand(str2);
            database2.CloseConnection();
            var database3 = new Database("acidplayers");
            string str3 = @"DELETE FROM team1;";
            var table3 = database3.FireCommand(str3);
            database3.CloseConnection();
            

        }
        public static void DeleteTableBlind()
        {
            var database = new Database("acidplayers");
            string str = @"DELETE FROM newgame;";
            var table = database.FireCommand(str);
            database.CloseConnection();
            var database2 = new Database("acidplayers");
            string str2 = @"DELETE FROM blindteam2;";
            var table2 = database2.FireCommand(str2);
            database2.CloseConnection();
            var database3 = new Database("acidplayers");
            string str3 = @"DELETE FROM blindteam1;";
            var table3 = database3.FireCommand(str3);
            database3.CloseConnection();


        }
        public static List<String> CheckExistingUser(IUser user)
        {
            var result = new List<String>();
            var database = new Database("acidplayers");

            var str = string.Format("SELECT * FROM playertable WHERE user_id = '{0}'", user.Id);
            var tableName = database.FireCommand(str);

            while (tableName.Read())
            {
                var userId = (string)tableName["user_id"];

                result.Add(userId);
            }

            return result;
        }
        public static List<String> CheckExistingLobby(IUser user)
        {
            var result = new List<String>();
            var database = new Database("acidplayers");

            var str = string.Format("SELECT * FROM newgame WHERE user_id = '{0}'", user.Id);
            var tableName = database.FireCommand(str);

            while (tableName.Read())
            {
                var userId = (string)tableName["user_id"];

                result.Add(userId);
            }

            return result;
        }
        public static List<String> CheckExistingUserTeams(IUser user)
        {
            var result = new List<String>();
            var database = new Database("acidplayers");
            var str = string.Format("SELECT * FROM team1 WHERE user_id = '{0}'", user.Id);

            var tableName = database.FireCommand(str);
            while (tableName.Read())
            {
                var userId = (string)tableName["user_id"];

                result.Add(userId);
            }

            database.CloseConnection();
            var database2 = new Database("acidplayers");
            var str2 = string.Format("SELECT * FROM team2 WHERE user_id = '{0}'", user.Id);
            var tableName2 = database2.FireCommand(str2);

            while (tableName2.Read())
            {
                var userId = (string)tableName2["user_id"];

                result.Add(userId);
            }
            database2.CloseConnection();
            return result;
        }
        public static int GetPlayerCount()
        {
            var database = new Database("acidplayers");
            int count = 0;
            var str = string.Format("SELECT username FROM newgame");
            var tableName = database.FireCommand(str);
            while (tableName.Read())
            {
                count++;

            }
            return count;
        }
        public static void UpdateGameID()
        {
            var database = new Database("acidplayers");
            int gameid = 0;
            var str = string.Format("SELECT id FROM game_id");
            var tableName = database.FireCommand(str);
            while (tableName.Read())
            {
                gameid = (int)tableName["id"];

            }
            int updatedID = gameid + 1;
            database.CloseConnection();
            var database2 = new Database("acidplayers");        
            var str2 = string.Format("UPDATE game_id SET id = '{0}'", updatedID );
            var tableName2 = database2.FireCommand(str2);
            
        }
        public static string GetlobbyinfoNoPrint()
        {


            var database = new Database("acidplayers");

            var str = string.Format("SELECT * FROM newgame");
            var tableName = database.FireCommand(str);
            string PlayerNames = "Players in the Lobby:\n";
            while (tableName.Read())
            {
                PlayerNames += (string)tableName["username"] + " (" + (int)tableName["playermmr"] + ")\n";
            }
            return PlayerNames;

        }
        public static string Getlobbyinfo()
        {


            var database = new Database("acidplayers");

            var str = string.Format("SELECT * FROM newgame");
            var tableName = database.FireCommand(str);
            string PlayerNames = ":small_blue_diamond: Players still in the lobby:\n";
            int count = 0;
            while (tableName.Read())
            {
                count++;
                PlayerNames += (string)tableName["username"] + " (" + (int)tableName["playermmr"] + ")\n";
            }
            PlayerNames += "\n\n Total Players: " + count + "\n\n ------------------------------------------";
            return PlayerNames;

        }
        public static ulong[] SelectCaptains()
        {

            var database = new Database("acidplayers");
            var str = string.Format("SELECT * FROM newgame");
            var tableName = database.FireCommand(str);
            string[] top10 = new string[100];
            int[] top10mmr = new int[100];
            
            int total;
            int count = 0;
            bool swapped = true;
            int temp;
            string temp2;
            while (tableName.Read())
            {
                top10[count] = (string)tableName["user_id"];

                top10mmr[count] = (int)tableName["playermmr"];
                count++;
            }
            database.CloseConnection();
            total = count;

            for (int i = 1; (i <= (count - 1)) && swapped; i++)
            {
                swapped = false;
                for (int j = 0; j < count - 1; j++)
                {
                    if (top10mmr[j + 1] > top10mmr[j])
                    {
                        temp = top10mmr[j];
                        temp2 = top10[j];
                        top10mmr[j] = top10mmr[j + 1];
                        top10[j] = top10[j + 1];
                        top10mmr[j + 1] = temp;
                        top10[j + 1] = temp2;
                        swapped = true;
                    }
                }
            }
            
            Random rnd = new Random();
            int num = rnd.Next(4);            
            int num2 = rnd.Next(4);
            while (num == num2)
            {
                num2 = rnd.Next(4);
                num = rnd.Next(4);
            }
            ulong[] SelectedCaptains = new ulong[2];
            SelectedCaptains[0] = Convert.ToUInt64(top10[num]);
            SelectedCaptains[1] = Convert.ToUInt64(top10[num2]);
            return SelectedCaptains;
           
        }

        

        public static List<String> CheckExistingUserLobby(IUser user)
        {
            var result = new List<String>();
            var database = new Database("acidplayers");

            var str = string.Format("SELECT * FROM newgame WHERE user_id = '{0}'", user.Id);
            var tableName = database.FireCommand(str);

            while (tableName.Read())
            {
                var userId = (string)tableName["username"];

                result.Add(userId);
            }

            return result;
        }

        public static bool CheckFull(IUser user)
        {
            var database = new Database("acidplayers");

            var str = string.Format("SELECT * FROM newgame WHERE user_id = '{0}'", user.Id);
            var tableName = database.FireCommand(str);
            int x = 0;
            while (tableName.Read())
            {
                x++;

            }
            if (x <= 15)
                return false;
            else
                return true;

        }
        public static bool CheckFullTeam1()
        {
            var database = new Database("acidplayers");

            var str = string.Format("SELECT * FROM team1 ");
            var tableName = database.FireCommand(str);
            int x = 0;
            while (tableName.Read())
            {
                x++;

            }
            if (x == 5)
                return true;
            else
                return false;

        }
        public static bool CheckFullTeam2()
        {
            var database = new Database("acidplayers");

            var str = string.Format("SELECT * FROM team2");
            var tableName = database.FireCommand(str);
            int x = 0;
            while (tableName.Read())
            {
                x++;

            }
            if (x == 5)
                return true;
            else
                return false;

        }
        public static string GetTeaminfo()
        {
            var database = new Database("acidplayers");

            var str = string.Format("SELECT * FROM team2");
            var tableName = database.FireCommand(str);
            string PlayerNames = ":small_blue_diamond: Players on Team 2:\n";
            while (tableName.Read())
            {
                PlayerNames += (string)tableName["username"] + " (" + (int)tableName["mmr"] + ")\n";
            }
            database.CloseConnection();
            var database2 = new Database("acidplayers");

            var str2 = string.Format("SELECT * FROM team1");
            var tableName2 = database2.FireCommand(str2);
            PlayerNames += "\n:small_blue_diamond: Players on Team 1:\n";
            while (tableName2.Read())
            {
                PlayerNames += (string)tableName2["username"] + " (" + (int)tableName2["mmr"] + ")\n";
            }
            return PlayerNames;
        }
        public static string GetTeaminfoBlind()
        {
            var database = new Database("acidplayers");

            var str = string.Format("SELECT * FROM blindteam2");
            var tableName = database.FireCommand(str);
            string PlayerNames = ":small_blue_diamond: Players on Team 2:\n";
            while (tableName.Read())
            {
                PlayerNames += (string)tableName["username"] + " (" + (int)tableName["mmr"] + ")\n";
            }
            database.CloseConnection();
            var database2 = new Database("acidplayers");

            var str2 = string.Format("SELECT * FROM blindteam1");
            var tableName2 = database2.FireCommand(str2);
            PlayerNames += "\n:small_blue_diamond: Players on Team 1:\n";
            while (tableName2.Read())
            {
                PlayerNames += (string)tableName2["username"] + " (" + (int)tableName2["mmr"] + ")\n";
            }
            return PlayerNames;
        }
        public static string EnterUserTeam1(IUser user)
        {
            var result = new List<tableName>();

            var database = new Database("acidplayers");

            var str = string.Format("SELECT * FROM newgame WHERE user_id = '{0}'", user.Id);
            var tableName = database.FireCommand(str);
            int currentmmr = 0;
            while (tableName.Read())
            {
                currentmmr = (int)tableName["playermmr"];
                result.Add(new tableName
                {
                    mmr = currentmmr,
                });
            }
            database.CloseConnection();

            var str1 = string.Format("INSERT INTO team1(user_id, username, mmr, ready) VALUES ('{0}', '{1}', '{2}', '0')", user.Id, user.Username, currentmmr);
            var database2 = new Database("acidplayers");
            var table = database2.FireCommand(str1);
            database2.CloseConnection();

            return null;
        }
        public static string EnterUserTeam2(IUser user)
        {
            var result = new List<tableName>();

            var database = new Database("acidplayers");
            var str = string.Format("SELECT * FROM newgame WHERE user_id = '{0}'", user.Id);
            var tableName = database.FireCommand(str);
            int currentmmr = 0;
            while (tableName.Read())
            {
                currentmmr = (int)tableName["playermmr"];
                result.Add(new tableName
                {
                    mmr = currentmmr,
                });
            }
            var str1 = string.Format("INSERT INTO team2(user_id, username, mmr, ready) VALUES ('{0}', '{1}', '{2}', '0')", user.Id, user.Username, currentmmr);
            database.CloseConnection();

            var database2 = new Database("acidplayers");
            var table = database2.FireCommand(str1);


            database2.CloseConnection();
            return null;
        }
        //-------------------------------------------------------------------------------------------------------
        // Blind Picks
        public static string EnterUserBlindTeam1(IUser user)
        {
            var result = new List<tableName>();

            var database = new Database("acidplayers");

            var str = string.Format("SELECT * FROM newgame WHERE user_id = '{0}'", user.Id);
            var tableName = database.FireCommand(str);
            int currentmmr = 0;
            while (tableName.Read())
            {
                currentmmr = (int)tableName["playermmr"];
                result.Add(new tableName
                {
                    mmr = currentmmr,
                });
            }
            database.CloseConnection();

            var str1 = string.Format("INSERT INTO blindteam1(user_id, username, mmr, ready) VALUES ('{0}', '{1}', '{2}', '0')", user.Id, user.Username, currentmmr);
            var database2 = new Database("acidplayers");
            var table = database2.FireCommand(str1);
            database2.CloseConnection();

            return null;
        }
        public static string EnterUserBlindTeam2(IUser user)
        {
            var result = new List<tableName>();

            var database = new Database("acidplayers");
            var str = string.Format("SELECT * FROM newgame WHERE user_id = '{0}'", user.Id);
            var tableName = database.FireCommand(str);
            int currentmmr = 0;
            while (tableName.Read())
            {
                currentmmr = (int)tableName["playermmr"];
                result.Add(new tableName
                {
                    mmr = currentmmr,
                });
            }
            var str1 = string.Format("INSERT INTO blindteam2(user_id, username, mmr, ready) VALUES ('{0}', '{1}', '{2}', '0')", user.Id, user.Username, currentmmr);
            database.CloseConnection();

            var database2 = new Database("acidplayers");
            var table = database2.FireCommand(str1);
            database2.CloseConnection();
            return null;
        }

        public static string EnterUser(IUser user)
        {
            var database = new Database("acidplayers");

            var str = string.Format("INSERT INTO playertable(user_id, username, mmr, wins) VALUES ('{0}', '{1}', '100', '0')", user.Id, user.Username);
            var table = database.FireCommand(str);

            database.CloseConnection();

            return null;
        }
        public static List<tableName> EnterUserLobby(IUser user)
        {
            var result = new List<tableName>();

            var database = new Database("acidplayers");

            var str = string.Format("SELECT * FROM playertable WHERE user_id = '{0}'", user.Id);
            var tableName = database.FireCommand(str);
            int currentmmr = 0;
            while (tableName.Read())
            {
                currentmmr = (int)tableName["mmr"];
                result.Add(new tableName
                {
                    mmr = currentmmr,
                });
            }

            database.CloseConnection();

            var database2 = new Database("acidplayers");
            var str1 = string.Format("INSERT INTO newgame(user_id, username, playermmr) VALUES ('{0}', '{1}', '{2}')", user.Id, user.Username, currentmmr);
            var table = database2.FireCommand(str1);


            database2.CloseConnection();
            return null;
        }
        public static void SetResults()
        {

            var result = new List<tableName>();
            var database = new Database("acidplayers");
            int currentgame_id = 0;
            var str = string.Format("SELECT * FROM game_id");
            var tableName = database.FireCommand(str);
            while (tableName.Read())
            {
                currentgame_id = (int)tableName["id"];
                result.Add(new tableName
                {
                    id = currentgame_id,
                });
            }
            database.CloseConnection();
            var database2 = new Database("acidplayers");
            var str2 = string.Format("INSERT INTO result(team2score, team1score, game_id, captain_ready) VALUES( '0', '0', '{0}', '0')", currentgame_id);
            var tableName2 = database2.FireCommand(str2);
            UpdateGameID();
            
        }
        public static int GetGameID()
        {
            var database = new Database("acidplayers");
            int gameid = 0;
            var str = string.Format("SELECT id FROM game_id");
            var tableName = database.FireCommand(str);
            while (tableName.Read())
            {
                gameid = (int)tableName["id"];

            }          
            database.CloseConnection();
            return gameid - 1;
        }
        public static String Winner(int game_id, string winner)
        {



            var database = new Database("acidplayers");
            var str = string.Format("SELECT * FROM result WHERE game_id = '{0}'",game_id);
            var tableName = database.FireCommand(str);
            var team2count = 0;
            var team1count = 0;
            while (tableName.Read())
            {
                team2count = (int)tableName["team2score"];
                team1count = (int)tableName["team1score"];
            }
            database.CloseConnection();

            var database2 = new Database("acidplayers");

            if (winner == "team2")
            {
                team2count++;
                var str2 = string.Format("UPDATE result SET team2score = '{1}' WHERE game_id = '{0}' ", game_id, team2count);
                var tableName2 = database2.FireCommand(str2);

            }
            else if (winner == "team1")
            {
                team1count++;
                var str4 = string.Format("UPDATE result SET team1score = '{1}' WHERE game_id = '{0}' ", game_id, team1count);
                var tableName2 = database2.FireCommand(str4);
            }
            database2.CloseConnection();

            var database3 = new Database("acidplayers");
            var str3 = string.Format("SELECT * FROM result");
            var tableName3 = database3.FireCommand(str3);
            while (tableName3.Read())
            {
                if ((int)tableName3["team2score"] >= 6)
                {
                    string WinningTeam = "team2";
                    database3.CloseConnection();
                    return (WinningTeam);
                }
                else if ((int)tableName3["team1score"] >= 6)
                {
                    string WinningTeam2 = "team1";
                    database3.CloseConnection();
                    return (WinningTeam2);
                }
            }


            database3.CloseConnection();
            return null;


        }

        public static List<tableName> GetUserStatus(IUser user)
        {
            var result = new List<tableName>();

            var database = new Database("acidplayers");

            var str = string.Format("SELECT * FROM playertable WHERE user_id = '{0}'", user.Id);
            var tableName = database.FireCommand(str);

            while (tableName.Read())
            {
                var userId = (string)tableName["user_id"];
                var userName = (string)tableName["username"];
                var currentmmr = (int)tableName["MMR"];
                var currentwins = (int)tableName["wins"];
                result.Add(new tableName
                {
                    UserId = userId,
                    Username = userName,
                    mmr = currentmmr,
                    wins = currentwins
                });
            }
            database.CloseConnection();

            return result;

        }


        public static void ChangeMMR(IUser user, int mMR)
        {
            var database = new Database("acidplayers");

            try
            {
                var strings = string.Format("UPDATE playertable SET mmr = '{1}' WHERE user_id = {0}", user.Id, mMR);
                var reader = database.FireCommand(strings);
                reader.Close();
                database.CloseConnection();
                return;
            }
            catch (Exception)
            {
                database.CloseConnection();
                return;
            }
        }
        public static void ChangeMMRUserID(ulong user, int mMR)
        {


            var database = new Database("acidplayers");

            try
            {
                var strings = string.Format("UPDATE playertable SET mmr = '{1}' WHERE user_id = {0}", user, mMR);
                var reader = database.FireCommand(strings);
                reader.Close();
                database.CloseConnection();
                return;
            }
            catch (Exception)
            {
                database.CloseConnection();
                return;
            }
        }
        public static void GiveMMR(string winner, int mmrwinnings)
        {
            if (winner == "team1")
            {
                var database = new Database("acidplayers");
                var str = string.Format("SELECT * FROM team1");
                var tableName = database.FireCommand(str);

                ulong[] team1Array = new ulong[6];
                int[] team1 = new int[6];
                int i = 0;
                string playerid = " ";
                while (tableName.Read())
                {
                    playerid = (string)tableName["user_id"];
                    team1Array[i] = Convert.ToUInt64(playerid);
                    team1[i] = (int)tableName["mmr"];
                    i++;
                }
                database.CloseConnection();


                while (i >= 0)
                {
                    ChangeMMRUserID(team1Array[i], team1[i] + mmrwinnings);
                    i--;
                }


            }


            if (winner == "team2")
            {
                var database2 = new Database("acidplayers");
                var str2 = string.Format("SELECT * FROM team2");
                var tableName2 = database2.FireCommand(str2);


                ulong[] team2Array = new ulong[6];
                int[] team2mmr = new int[6];
                int i = 0;
                string playerid = " ";
                while (tableName2.Read())
                {
                    playerid = (string)tableName2["user_id"];
                    team2Array[i] = Convert.ToUInt64(playerid);
                    team2mmr[i] = (int)tableName2["mmr"];
                    i++;
                }
                database2.CloseConnection();


                while (i >= 0)
                {
                    ChangeMMRUserID(team2Array[i], team2mmr[i] + mmrwinnings);
                    i--;
                }

            }
        }

        public static void TakeMMR(string loser, int mmrgone)
        {
            int mmrlost = -1 * mmrgone;
            if (loser == "team1")
            {
                var database = new Database("acidplayers");
                var str = string.Format("SELECT * FROM team1");
                var tableName = database.FireCommand(str);

                ulong[] Team1Array = new ulong[6];
                int[] team1mmr = new int[6];
                int i = 0;
                string playerid = " ";
                while (tableName.Read())
                {
                    playerid = (string)tableName["user_id"];
                    Team1Array[i] = Convert.ToUInt64(playerid);
                    team1mmr[i] = (int)tableName["mmr"];
                    i++;
                }
                database.CloseConnection();


                while (i >= 0)
                {
                    ChangeMMRUserID(Team1Array[i], team1mmr[i] + mmrlost);
                    i--;
                }

            }
            if (loser == "team2")
            {
                var database2 = new Database("acidplayers");
                var str2 = string.Format("SELECT * FROM team2");
                var tableName2 = database2.FireCommand(str2);

                ulong[] team2Array = new ulong[6];
                int[] team2mmr = new int[6];
                int i = 0;
                string playerid = " ";
                while (tableName2.Read())
                {
                    playerid = (string)tableName2["user_id"];
                    team2Array[i] = Convert.ToUInt64(playerid);
                    team2mmr[i] = (int)tableName2["mmr"];
                    i++;
                }
                database2.CloseConnection();


                while (i >= 0)
                {
                    ChangeMMRUserID(team2Array[i], team2mmr[i] + mmrlost);
                    i--;
                }

            }
        }
        public static int GetResultCount(string winner, int gameID)
        {
            int count = 0;
            var database = new Database("acidplayers");
            var str = string.Format("SELECT * FROM result WHERE game_id = '{0}' ", gameID);
            var tableName = database.FireCommand(str);
            while (tableName.Read())
            {
                if (winner == "team2")
                {
                    count = (int)tableName["team2score"];
                }
                if (winner == "team1")
                {
                    count = (int)tableName["team1score"];
                }
            }
            return count;
        }
        public static int GetResultCount2()
        {
            int count = 0;
            /*
            var database = new Database("acidplayers");
            var str = string.Format("SELECT * FROM result");
            var tableName = database.FireCommand(str);
            while (tableName.Read())
            {              
                    count = (int)tableName["captain_ready"];                               
            }
            */
            return count;
        }

        public static bool CaptainReady(string team)
        {                   
            if (team == "team1")
            {
                var database = new Database("acidplayers");
                var str = string.Format("UPDATE team1 SET ready = '1' ");
                var tableName = database.FireCommand(str);                
                return true;
            }
            if (team == "team2")
            {
                var database = new Database("acidplayers");
                var str = string.Format("UPDATE team2 SET ready = '1' ");
                var tableName = database.FireCommand(str);
                return true;
            }
            return false;
        }
        public static int IsCaptainReady(string team)
        {
            
            int count = 0;
            int newcount = 0; 
            if (team == "team1")
            {
                var database = new Database("acidplayers");
                var str = string.Format("SELECT * FROM team1");
                var tableName = database.FireCommand(str);
                while (tableName.Read())
                {
                    count = (int)tableName["ready"];
                    newcount = count + newcount;
                }
                return newcount;
            }
            else if (team == "team2")
            {
                var database = new Database("acidplayers");
                var str = string.Format("SELECT * FROM team2");
                var tableName = database.FireCommand(str);
                while (tableName.Read())
                {
                    count += (int)tableName["ready"];
                }
                return count;
            }
            else
                return count;
        }
        public static bool CaptainReadyBlind(string team)
        {
            if (team == "team1")
            {
                var database = new Database("acidplayers");
                var str = string.Format("UPDATE blindteam1 SET ready = '1' ");
                var tableName = database.FireCommand(str);

                return true;
            }
            if (team == "team2")
            {
                var database = new Database("acidplayers");
                var str = string.Format("UPDATE blindteam2 SET ready = '1' ");
                var tableName = database.FireCommand(str);
                return true;
            }
            return false;
        }
        public static string CreateLobby()
        {
            string[] passwords = new string[10];
            passwords[1] = "rune";
            passwords[2] = "322";
            passwords[3] = "rtz";
            passwords[4] = "liquid";
            passwords[5] = "secret";
            passwords[6] = "acid";
            passwords[7] = "dota2";
            passwords[8] = "haste";
            passwords[9] = "creeps";
            passwords[0] = "2ez";
            Random rnd = new Random();
            int num = rnd.Next(10);
            int game_id = GetGameID();
            string lobbyinfo = "\n \n 1 Captain must create a lobby titled, Acid Inhouse League " + game_id  +"\n\n  The password being **\"" + passwords[num] + "\"**\n" + "\n\n  The gameID being **\"" + game_id + "\"**\n" +
                "\n Lobby settings are as follows: \n Captains Mode, USE or USW,  Selection Priority: Automatic (Each team must have a Dota 2 team selected for this to work.) \n Dota tv delay : 2 minutes";

            return lobbyinfo;
        }
        public static double MMROffset(string winner)
        {
            int team1total = 0;
            int team2total = 0;
            var database2 = new Database("acidplayers");
            var str2 = string.Format("SELECT * FROM team1");
            var tableName2 = database2.FireCommand(str2);
            while (tableName2.Read())
            {
                team1total += (int)tableName2["mmr"];
            }
            team1total = team1total / 5;
            database2.CloseConnection();

            var database = new Database("acidplayers");
            var str = string.Format("SELECT * FROM team2");
            var tableName = database.FireCommand(str);
            while (tableName.Read())
            {
                team2total += (int)tableName["mmr"];
            }
            team2total = team2total / 5;
            database.CloseConnection();
            int offset = 0;
            
            // figure out OFFSET
            if (team1total - team2total > 0 && winner == "team1")
            {
                //team 1 is favored and won
                offset = team1total - team2total;
                if(offset <= 10)
                {
                    return 1;
                }
                if (offset <= 20)
                {
                    return .7;
                }
                else
                    return .4;
            }
            if (team1total - team2total > 0 && winner == "team2")
            {
                // team 1 is favored and lost
                offset = team1total - team2total;
                if (offset <= 10)
                {
                    return 1;
                }
                if (offset <= 20)
                {
                    return 1.3;
                }
                else
                    return 1.6;
            }
            if (team2total - team1total > 0 && winner == "team2")
            {
                //team 2 is favored and won
                offset = team2total - team1total;
                if (offset <= 10)
                {
                    return 1;
                }
                if (offset <= 20)
                {
                    return .7;
                }
                else
                    return .4;
            }
            if (team2total - team1total > 0 && winner == "team1")
            {
                // team 2 is favored and lost
                offset = team2total - team1total;
                if (offset <= 10)
                {
                    return 1;
                }
                if (offset <= 20)
                {
                    return 1.3;
                }
                else
                    return 1.6;
            }
            else
                return 1;
        }
        public static string GetLeaderboardInfo()
        {

            var database = new Database("acidplayers");
            var str = string.Format("SELECT * FROM playertable");
            var tableName = database.FireCommand(str);
            string[] top10 = new string[200];
            int[] top10mmr = new int[200];
           
            int total; 
            int count = 0;
            bool swapped = true;
            int temp;
            string temp2;
            while (tableName.Read())
            {
                top10[count] = (string)tableName["username"];

                top10mmr[count] = (int)tableName["mmr"];
                count++;
            }
            database.CloseConnection();
            total = 25;

          
            for (int i = 1; (i <= (count - 1)) && swapped; i++)
            {
                swapped = false;
                for (int j = 0; j < count - 1; j++)
                {
                    if (top10mmr[j + 1] > top10mmr[j])
                    {
                        temp = top10mmr[j];
                        temp2 = top10[j];
                        top10mmr[j] = top10mmr[j + 1];
                        top10[j] = top10[j + 1];
                        top10mmr[j + 1] = temp;
                        top10[j + 1] = temp2;
                        swapped = true;
                    }
                }
            }


            int length = 0;
            string leaders = " ";

            while (length < total)
            {
                leaders += ((length+1) + ". " + top10[length] + " (" + top10mmr[length] + ")\n");
                length++;
            }
            return leaders;
        }
        public string [] Swapstring(int first, int second, string[] top10)
        {
            int temp = second;
            top10[second] = top10[first];           
            top10[first] = top10[temp];
            return top10;
        }
        public static ulong [] BlindPick()
        {
            var database2 = new Database("acidplayers");
            var str2 = string.Format("INSERT INTO result (team2score, team1score, captain_ready) VALUES( '0', '0', ' 0')");
            var tableName2 = database2.FireCommand(str2);
            database2.CloseConnection();
            ulong[] playersArray = new ulong[8];

            var database = new Database("acidplayers");
            var str = string.Format("SELECT * FROM newgame");
            var tableName = database.FireCommand(str);
            string[] top10 = new string[10];
            int[] top10mmr = new int[10];

            int total;
            int count = 0;
            bool swapped = true;
            int temp;
            string temp2;
            while (tableName.Read())
            {
                top10[count] = (string)tableName["user_id"];

                top10mmr[count] = (int)tableName["playermmr"];
                count++;
            }
            database.CloseConnection();
            total = count;

            for (int i = 1; (i <= (count - 1)) && swapped; i++)
            {
                swapped = false;
                for (int j = 0; j < count - 1; j++)
                {
                    if (top10mmr[j + 1] > top10mmr[j])
                    {
                        temp = top10mmr[j];
                        temp2 = top10[j];
                        top10mmr[j] = top10mmr[j + 1];
                        top10[j] = top10[j + 1];
                        top10mmr[j + 1] = temp;
                        top10[j + 1] = temp2;
                        swapped = true;
                    }
                }
            }
            int count2 = 0;
            while (count2 != 8)
            { 
            playersArray[count2] = Convert.ToUInt64(top10[count2]);
                count2++;
            }
            return playersArray;
        } 
    }

}
