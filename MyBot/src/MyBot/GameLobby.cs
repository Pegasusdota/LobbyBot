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
    public class GameLobby : CommandHandler
    {
        public int [] Team1 = new int [5];
        public int [] Team2 = new int[5];
        public int [] Unpicked = new int[15];

       /*
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
      */

       
    }
}
