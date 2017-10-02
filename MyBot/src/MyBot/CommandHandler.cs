using System.Threading.Tasks;
using System.Reflection;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System;
using Discord;
using MySql.Data.MySqlClient;

namespace MyBot
{
    public class CommandHandler : ModuleBase
    {
        private CommandService _cmds;
        private DiscordSocketClient _client;
        public SocketGuildUser Team1Captain;
        public SocketGuildUser Team2Captain;
        
        public async Task Install(DiscordSocketClient c)
        {
            _client = c;
            _cmds = new CommandService();

            await _cmds.AddModulesAsync(Assembly.GetEntryAssembly());

            _client.MessageReceived += HandleCommand;
           

          

        
           
        }

       public async void SendMessage(string v)
        {
            var channel = await Context.Guild.GetChannelAsync(354894820596121611) as SocketTextChannel;
            await channel.SendMessageAsync(v);
        }

        public async Task HandleCommand(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;

            var context = new CommandContext(_client, msg);

            int argPos = 0;
            if (msg.HasStringPrefix("!", ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var result = await _cmds.ExecuteAsync(context, argPos);

                if (!result.IsSuccess) await context.Channel.SendMessageAsync(result.ToString());
            }
        }
        


        
        [Command("t1pick")]
        [Alias("t1", "team1")]
        [Summary("select a player for team1")]
        [RequireUserPermission(GuildPermission.ManageWebhooks)]
        public async Task team1pick(SocketGuildUser user)
        {
            bool full = Database.CheckFullTeam1();

            var result = Database.CheckExistingUserTeams(user);
            var playercheck = Database.CheckExistingLobby(user);
            if (result.Count() <= 0 && playercheck.Count() == 1) 
            {
               if (full)
                {
                    await ReplyAsync("Your team is full, Use !go \"yourname\" to ready up.");
               }
               else
               {
                    Database.EnterUserTeam1(user);
                    await ReplyAsync(user.Mention + "you are now on the Team 1");
                    Database.DeleteUserLobby(user);
                    await ReplyAsync("!list");
               }
            }
            else if (full)
            {
                await ReplyAsync("Your team is full, Use !go \"yourname\" to ready up.");
            }
            else
            {
                await ReplyAsync(user.Mention + "is not in the Lobby, or is alreay on a team");
            }
            // check if team is full
        }
        [Command("t2pick")]
        [Alias("t2", "team2")]
        [Summary("select a player for  team2 ")]
        [RequireUserPermission(GuildPermission.ManageEmojis)]
        public async Task Team2pick(SocketGuildUser user)
        {
            bool full = Database.CheckFullTeam2();
            
            var result = Database.CheckExistingUserTeams(user);
            var playercheck = Database.CheckExistingLobby(user);
            if (result.Count() <= 0 && playercheck.Count() == 1)
            {
                if (full)
                {
                    await ReplyAsync("Your team is full, Use !go \"yourname\" to ready up.");
                }
                else
                {
                    Database.EnterUserTeam2(user);
                    await ReplyAsync(user.Mention + "you are now on the Team 2");
                    Database.DeleteUserLobby(user);
                    await ReplyAsync("!list");
                }
            }
            
            else
            {
                await ReplyAsync(user.Mention + "is not in the Lobby, or is alreay on a team");
            }
            // check if team is full
           

        }
        [Command("go")]
        [Alias("g", "ready")]
        [Summary("ready to start game")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task Startgame(SocketGuildUser user)
        {
            // REDO THIS WITH NEW CAPTAIN READY
            int count = 0;
            var channel = await Context.Guild.GetChannelAsync(357768928421740545) as SocketTextChannel;
            var role2 = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "team2");
            if (user.Roles.Contains(role2))
            {
               
                Database.CaptainReady("team2");
                count += Database.IsCaptainReady("team2");
                await user.RemoveRoleAsync(role2);
            }
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "team1");
            if (user.Roles.Contains(role))
            {
                Database.CaptainReady("team1");
                count += Database.IsCaptainReady("team1");                
                await user.RemoveRoleAsync(role);
            }
                        
            if (count >= 2)
            {
                string lobbyinfo = Database.CreateLobby();
                string teaminfo = Database.GetTeaminfo();

                await channel.SendMessageAsync(teaminfo);
                await channel.SendMessageAsync(lobbyinfo);
            }
            else 
            {
               await ReplyAsync("Need the other captain to use !go YOURNAME to continue");
            }
        }
        [Command("captainblindteam2")]
        [Summary("select a player for captain of team2")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task BlindCaptaindire(SocketGuildUser user)
        {
            var role = user.Guild.Roles.Where(has => has.Name.ToUpper() == "team2".ToUpper());

            Database.EnterUserBlindTeam2(user);
            Database.CaptainReadyBlind("team2");
            Database.DeleteUserLobby(user);
            await user.AddRolesAsync(role);
        }
        [Command("captainblindteam1")]
        [Summary("select a player for captain of team1")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task BlindCaptainteam1(SocketGuildUser user)
        {
            var role = user.Guild.Roles.Where(has => has.Name.ToUpper() == "team1".ToUpper());
            Database.EnterUserBlindTeam1(user);
            Database.CaptainReadyBlind("team1");
            Database.DeleteUserLobby(user);
            await user.AddRolesAsync(role);
        }
        [Command("captainteam2")]       
        [Summary("select a player for captain of team2")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task Captaindire(SocketGuildUser user)
        {
            var role = user.Guild.Roles.Where(has => has.Name.ToUpper() == "team2".ToUpper());

            Database.EnterUserTeam2(user);
           // Database.CaptainReady("team2");
            Database.DeleteUserLobby(user);
            await user.AddRolesAsync(role);
        }
        [Command("captainteam1")]
        [Summary("select a player for captain of team1")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task Captainteam1(SocketGuildUser user)
        {
            var role = user.Guild.Roles.Where(has => has.Name.ToUpper() == "team1".ToUpper());
            Database.EnterUserTeam1(user);
            //Database.CaptainReady("team1");
            Database.DeleteUserLobby(user);
            await user.AddRolesAsync(role);
        }
        
        [Command("delete")]
        [Summary("delete messages from a channel")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task PurgeChat(uint amount)
        {
            var messages = await this.Context.Channel.GetMessagesAsync((int)amount + 1).Flatten();

            await this.Context.Channel.DeleteMessagesAsync(messages);
            const int delay = 5000;
            var m = await this.ReplyAsync($"Purge completed. _This message will be deleted in {delay / 1000} seconds._");
            await Task.Delay(delay);
            await m.DeleteAsync();
        }
       

        [Command("startcaptainspick")]
        [Alias("scp", "startcaptains")]
        [Summary("Creates a new Game")]
        public async Task StartGame()
        {
            var channel = await Context.Guild.GetChannelAsync(357768928421740545) as SocketTextChannel;                    
            int count = Database.GetPlayerCount(); // get count of players in lobby

            if (count < 10)
            {
                await channel.SendMessageAsync("Not enough players, Please get more Players to join with the **!join\"playername\"** command. ");
            }
            else if (count >= 10)
            {
               
                string str = Database.GetlobbyinfoNoPrint(); // get top 2 players for captain roles                
                ulong[] capArray = new ulong[2];
                capArray = Database.SelectCaptains();
                var HighestUser = (Context.Guild as SocketGuild).GetUser(capArray[0]);
                var SecondUser = (Context.Guild as SocketGuild).GetUser(capArray[1]);
                string str2 = " ";
                string str3 = " ";
                Random rnd = new Random();
                int side = rnd.Next(1);
                string sidename = " ";
                string sidename2 = " ";

                if (side == 1)
                {
                    sidename = "Team 1";
                    str2 = "!captainteam1 " + HighestUser.Mention;
                }
                else
                {
                    sidename = "Team 2";
                    str2 = "!captainteam2 " + HighestUser.Mention;
                }
                if (sidename == "Team 1")
                {
                    sidename2 = "Team 2";
                    str3 = "!captainteam2 " + SecondUser.Mention;
                }
                else
                {
                    sidename2 = "Team 1";
                    str3 = "!captainteam1 " + SecondUser.Mention;
                }
                await channel.SendMessageAsync(str2);
                await channel.SendMessageAsync("You are now captain of " + sidename);
                await channel.SendMessageAsync(str3);
                await channel.SendMessageAsync("You are now captain of " + sidename2);
                await channel.SendMessageAsync("!list");
                await channel.SendMessageAsync("When picking is over captains must use !go @\"yourname\" to continue");

            }           
        }
        [Command("teams")]
        [Alias("t", "team")]
        [Summary("Displays whos on each team")]
        public async Task GetTeams()
        {
            // var channel = await Context.Guild.GetChannelAsync(357768928421740545) as SocketTextChannel;
            var str = Database.GetTeaminfo();
            await ReplyAsync(str);
            await ReplyAsync("If team1 wins the MMR Loss/Gain will be: " + Database.MMROffset("team1") * 10);
            await ReplyAsync("If team2 wins the MMR Loss/Gain will be: " + Database.MMROffset("team2") * 10);
        }
        [Command("captainspick")]
        [Alias("cp", "captainpick")]
        [Summary("Creates a new captains pick Game")]
        public async Task NewGame()
        {
            var channel = await Context.Guild.GetChannelAsync(354894820596121611) as SocketTextChannel;
            if (Database.GetResultCount2() == 0)
            {
               

                await channel.SendMessageAsync("@ here New Game has been created \nJoin game by typing !join @\"YOURNAME\"");

                Database.DeleteTable();
                Database.SetResults();
                var channel2 = await Context.Guild.GetChannelAsync(359900035313434636) as SocketTextChannel;
                string str = "!delete 200";
                await channel2.SendMessageAsync(str);
            }
            else
            {
                await channel.SendMessageAsync("Cant make a newgame when the previous game is still going.");
            }
        }
        [Command("blindpick")]
        [Alias("bp")]
        [Summary("Creates a new Game where teams are randomly assigned")]
        public async Task BlindPick()
        {
            var channel = await Context.Guild.GetChannelAsync(354894820596121611) as SocketTextChannel;
            if (Database.GetResultCount2() == 0)
            {


                await channel.SendMessageAsync("@here New Game of Blind pick has been created \nJoin game by typing !join @YOURNAME ");

                Database.DeleteTableBlind();
                Database.SetResults();                
                var channel2 = await Context.Guild.GetChannelAsync(359900035313434636) as SocketTextChannel;
                string str = "!delete 200";
                await channel2.SendMessageAsync(str);
            }
            else
            {
                await channel.SendMessageAsync("Cant make a newgame when the previous game is still going.");
            }
        }
        [Command("startblindpick")]
        [Alias("sbp", "startblind")]
        [Summary("Creates a new Game")]
        public async Task StartGameblind()
        {
            var channel = await Context.Guild.GetChannelAsync(357768928421740545) as SocketTextChannel;


            int count = Database.GetPlayerCount(); // get count of players in lobby

            if (count < 10)
            {
                await channel.SendMessageAsync("Not enough players, Please get more Players to join with the **!join\"playername\"** command. ");
            }
            else if (count > 10)
            {
                await channel.SendMessageAsync("Too many players, Consider playing a PlayerDraft game.");
            }
            else
            {
                ulong[] captains = new ulong[2];
            
                captains = Database.SelectCaptains();
                var HighestUser = (Context.Guild as SocketGuild).GetUser(captains[0]);
                var SecondUser = (Context.Guild as SocketGuild).GetUser(captains[1]);
                string str2 = "!captainblindteam1 " + HighestUser.Mention;
                string str3 = "!captainblindteam2 " + SecondUser.Mention;
                await channel.SendMessageAsync(str2);
                await channel.SendMessageAsync("You are now captain of team1");
                await channel.SendMessageAsync(str3);
                await channel.SendMessageAsync("You are now captain of team2");                
                await channel.SendMessageAsync(" 1 Captain please use !ready start the game.");
            }
        }
        [Command("ready")]
        [Alias("r")]
        [Summary("Creates the teams for blind pick")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task Ready()
        {
            var channel = await Context.Guild.GetChannelAsync(357768928421740545) as SocketTextChannel;
            ulong[] players = new ulong[8];
            players = Database.BlindPick();
            int count2 = 0;
            int counter = 0;
            while (counter < 4)
            {
                var team1player = (Context.Guild as SocketGuild).GetUser(players[count2]);  // 0 , 2 , 4 , 6,
                Database.EnterUserBlindTeam1(team1player);
                Database.DeleteUserLobby(team1player);
                var team2player = (Context.Guild as SocketGuild).GetUser(players[count2 + 1]);// 1, 3, 5, 7,
                Database.EnterUserBlindTeam2(team2player);
                Database.DeleteUserLobby(team2player);
                count2 = count2 + 2;
                counter++;
            }            

            string lobbyinfo = Database.CreateLobby();
            await channel.SendMessageAsync(lobbyinfo);
            string teaminfo = Database.GetTeaminfoBlind();
            await channel.SendMessageAsync(teaminfo);            
            await ReplyAsync("If team1 wins the MMR Loss/Gain will be: " + Database.MMROffset("team1") * 10);
            await ReplyAsync("If team2 wins the MMR Loss/Gain will be: " + Database.MMROffset("team2") * 10);
        }
        
        [Command("quit")]
        [Alias("q")]
        [Summary("leaves the  game")]
        public async Task quit(SocketGuildUser user)
        {
            var result = Database.CheckExistingUserLobby(user);
            if (result.Count == 1)
            {
                Database.DeleteUserLobby(user);
                //var channel = await Context.Guild.GetChannelAsync(354894820596121611) as SocketTextChannel;
                await ReplyAsync(user.Username + " left the game.");
                string str = Database.Getlobbyinfo();
                var channel3 = await Context.Guild.GetChannelAsync(359900035313434636) as SocketTextChannel;
                await channel3.SendMessageAsync("Current Lobby List is: \n " + str);
            }
            else
            {                
                var channel2 = await Context.Guild.GetChannelAsync(354894820596121611) as SocketTextChannel;
                await channel2.SendMessageAsync("There seems to be a mistake " + user.Username + " is not in the lobby");
            }

        }


        [Command("join")]
        [Alias("j")]
        [Summary("joins the  game")]
        public async Task Join(SocketGuildUser user)
        {  
            var role2 = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "team2");
            if (user.Roles.Contains(role2))
            {
                await user.RemoveRoleAsync(role2);
            }

            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "team1");
            if (user.Roles.Contains(role))
            {
                await user.RemoveRoleAsync(role);
            }
           
            var tableName = Database.GetUserStatus(user);
            var result = Database.CheckExistingUserLobby(user);
            bool count = Database.CheckFull(user);
            if (!count)
            {
                if (result.Count() <= 0)
                {
                    Database.EnterUserLobby(user);
                    // channel = await Context.Guild.GetChannelAsync(354894820596121611) as SocketTextChannel;
                    await ReplyAsync(user.Username + " joined the game with " + tableName.FirstOrDefault().mmr + " mmr");
                    string str = Database.Getlobbyinfo();
                    var channel3 = await Context.Guild.GetChannelAsync(359900035313434636) as SocketTextChannel;
                    await channel3.SendMessageAsync("Current Lobby List is: \n " + str);
                }
                else
                {
                    var channel2 = await Context.Guild.GetChannelAsync(354894820596121611) as SocketTextChannel;
                    await channel2.SendMessageAsync("there seems to be a mistake are you already in?");
                }
            }
            else
            {
                var channel2 = await Context.Guild.GetChannelAsync(354894820596121611) as SocketTextChannel;
                await channel2.SendMessageAsync("there seems to be a mistake");
            }
        }

        [Command("Status")]
        [Alias("status", "s")]
        public async Task Status([Remainder] IUser user = null)
        {
            if (user == null)
            {
                user = Context.User;
            }
            var tableName = Database.GetUserStatus(user);

            await ReplyAsync(Context.User.Mention + ", \n" + user.Username + "'s current status is as follows: \n" +
                  ":small_blue_diamond:" + tableName.FirstOrDefault().mmr + " Acid MMR \n");
                  //":small_blue_diamond:" + tableName.FirstOrDefault().wins + " wins");
        }

        [Command("list")]
        [Alias("l")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task List()
        {
            // var channel = await Context.Guild.GetChannelAsync(357768928421740545) as SocketTextChannel;
            var str = Database.Getlobbyinfo();
            await ReplyAsync(str);
        }

        [Command("Changemmr")]
        [Alias("changemmr", "cmmr")]
        [Summary("changes a players mmr")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task ChangeMMR(SocketGuildUser user, [Remainder] int MMR)
        {
            Database.ChangeMMR(user, MMR);
            var tableName = Database.GetUserStatus(user);
            await ReplyAsync(Context.User.Mention + ", \n" + user.Username + "'s current status is as follows: \n" +
                                               ":small_blue_diamond:" + tableName.FirstOrDefault().mmr + " Acid MMR \n" +
                                               ":small_blue_diamond:" + tableName.FirstOrDefault().wins + " wins");
        }

        [Command("add")]
        [Alias("a")]
        [Summary("adds Player to the Dictionary")]
        public async Task Addplayer(SocketGuildUser user)
        {
            var result = Database.CheckExistingUser(user);
            if (result.Count() <= 0)
            {
                Database.EnterUser(user);
            }
           
            await ReplyAsync("Welcome to the Acid Inhouse League " + user.Username +" Your Acid MMR is now 100. You can always check your MMR with !status " + user.Mention);
        }
        [Command("result")]
        [Alias("r")]
        [Summary("report the score of the game")]
        public async Task Result(int game_id, [Remainder] string winner ) //pass int game_id
        {

            

            string result = " ";
            string loser = " ";
            int count;
            double mmroffset = Database. MMROffset(winner);
            
            int mmrwinnings = (int)(mmroffset * 10);

            if (winner == "team2")
            {
                loser = "team1";
                result = "team2";
            }
            else if(winner == "team1")
            {
                loser = "team2";
                result = "team1";
            }
            string GameWinner = " ";
            GameWinner = Database.Winner(game_id,result); //GAME ID PASS
            count = Database.GetResultCount(result, game_id);
           
            if (count >= 6)
            {
                if (GameWinner != null)
                 {
                    //await ReplyAsync("The Winning team is " + result + " you have won " + mmrwinnings + " Acidmmr.");
                    Database.GiveMMR(result, mmrwinnings);
                    Database.TakeMMR(loser, mmrwinnings);
                    // Database.SetResults(game_id); 
                }
            }
            if (count > 7)
            {
                await ReplyAsync(result + " has already be declared the winner");

            }
            if (result == "team1")
            {
                await ReplyAsync("You have reported " + result + " as the winner");
            }
            if (result == "team2")
            {
                await ReplyAsync("You have reported " + result + " as the winner");
            }
            else
            {
                await ReplyAsync("Incorrect usage of !result, looking for the game ID and team1 or team2 as paramater.");
            }
        }

        [Command("leaderboard")]
        [Alias("ranking", "rank")]
        [Summary("report the 10 best players in the league")]
        public async Task Leaderboard()
        {
            string leaders  = Database.GetLeaderboardInfo();
            
           await ReplyAsync(":small_blue_diamond: The Leadersboards: \n" + leaders);
        }






    }
}