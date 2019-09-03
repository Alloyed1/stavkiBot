using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using stavkiBot.Models;
using stavkiBot.Repository;
using Telegram.Bot;

namespace stavkiBot.Controllers
{
    public class HomeController : Controller
    {
        private IBotRepository bot;
        public HomeController(IBotRepository bot)
        {
            this.bot = bot;
            RecurringJob.AddOrUpdate(
                () => bot.CheckMatch(),
                Cron.Minutely());
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
