using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext db;

        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.currencies.ToList());
        }

        public IActionResult EditCurrency(string id)
        {
            var currency = db.currencies.Find(id);
            return View(currency);
        }

        [HttpPost]
        public async Task<IActionResult> EditCurrency(Currency currency)
        {
            Currency cur = db.currencies.Find(currency.Id);
            cur.Name = currency.Name;
            cur.EngName = currency.EngName;
            cur.Nominal = currency.Nominal;
            cur.ParentCode = currency.ParentCode;
            cur.ISO_Num_Code = currency.ISO_Num_Code;
            cur.ISO_Char_Code = currency.ISO_Char_Code;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult CreateCurrency()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCurrency(Currency currency)
        {
            db.currencies.Add(currency);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult LoadCurrency()
        {
            GetCurrencyRate.LoadCurrency(db);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Index(string id)
        {

            var currency = db.currencies.Where(p => p.Id == id).Include(p => p.currencyRates).First();
            if (currency != null)
            {
                db.Remove(currency);
                db.SaveChanges();
            }

            return RedirectToAction();
        }

        public IActionResult CurrencyRate()
        {
            return View(db.currencyRates.Include(p => p.currency).OrderBy(p => p.Id).ToList());
        }
        [HttpPost]
        public IActionResult LoadCurrencyRate()
        {
            GetCurrencyRate.LoadCurrencyRate(db);
            return RedirectToAction("CurrencyRate");
        }

        [HttpPost]
        public IActionResult CurrencyRate(int id)
        {
            var cur = db.currencyRates.Find(id);
            if (cur != null)
            {
                db.currencyRates.Remove(cur);
                db.SaveChanges();
            }
            return RedirectToAction("CurrencyRate");
        }


        public IActionResult SearchCurrency()
        {
            return View(db.currencies.ToList());
        }

        [HttpPost]
        public IActionResult SearchCurrency(DateTime GetDate, string SelectCurrency)
        {
            var search = db.currencyRates.Where(p => p.date == GetDate && p.currency.Id == SelectCurrency).Include(p => p.currency).ToList();
            if ( search.Count != 0)
            {
                ViewBag.Result = search[0];
            }
            return View(db.currencies.ToList());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
