using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stavkiBot.Models;
using stavkiBot.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Telegram.Bot;
using Dapper;

namespace stavkiBot.Repository
{
    public interface IBotRepository
    {
        Task CheckMatch();
    }
    public class BotRepository : IBotRepository
    {
        private string connectionString;
        TelegramBotClient Bot;
        public BotRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            Bot = new TelegramBotClient("942721392:AAGPOeUb5NagpTz6aY_Wu1uZu3Lm2ykze2Q");
            Bot.OnMessage += Bot_OnMessageReceived;
            Bot.StartReceiving();
        }
        public async Task CheckMatch()
        {
            string matches;
           using(var client = new WebClient())
            {
                matches = await client.DownloadStringTaskAsync("https://api.betsapi.com/v1/events/inplay?sport_id=1&token=12814-EvbDryJ9ijA3nF");

                MatchViewModel joResponse = JsonConvert.DeserializeObject<MatchViewModel>(matches);
                List<int> games = new List<int>();
                using(var db = new SqlConnection(connectionString))
                {
                    games = (await db.QueryAsync<int>("SELECT GameId  FROM Games ")).ToList();
                }

                foreach(var item in joResponse.results.Where(s => s.timer.tm == "45" && s.ss == "0-0"))
                {
                    if (!games.Contains(item.id))
                    {

                        using (var db = new SqlConnection(connectionString))
                        {
                            await db.ExecuteAsync("INSERT INTO Games (GameId) VALUES(@gameid)", new { gameid = item.id });

                            List<int> idsChats = (await db.QueryAsync<int>("SELECT ChatId  FROM Users ")).ToList();
                            foreach (var id in idsChats)
                            {
                                await Bot.SendTextMessageAsync(id, $"GOAL UP\n\n{item.league.name}\n{item.home.name} vs {item.away.name}\n СЧЁТ {item.ss}");
                            }
                        }
                    }
                    
                    
                }

            }
        }
        public async Task SendMess()
        {
            await Bot.SendTextMessageAsync(1231212, "12312313");
        }
        private void Bot_OnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            using(var db = new SqlConnection(connectionString))
            {
                UsersBot user = db.QueryFirstOrDefault<UsersBot>("SELECT * FROM Users WHERE ChatId = @chatid", new { chatid = e.Message.Chat.Id });
                if(user == null && e.Message.Text == "/start")
                {
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, "Добро пожаловать, вы успешно зарегистрировались, теперь вы подключены к рассылке");
                    db.Execute("INSERT INTO Users (ChatId) VALUES(@chatid)", new { chatid = e.Message.Chat.Id });
                }
                else if(e.Message.Text == "/start")
                {
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, "Вы уже подключены");
                }
            }
        }
    }
}
