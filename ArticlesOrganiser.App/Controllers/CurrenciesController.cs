using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticlesOrganiser.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesOrganiser.Controllers
{
    [Route("[controller]")]
    public class CurrenciesController : Controller
    {
        private IDigitalCurrenciesRepository _repository;
        public CurrenciesController(IDigitalCurrenciesRepository repository)
        {
            _repository = repository;
        }
        // GET: CurrenciesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CurrenciesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CurrenciesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CurrenciesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CurrenciesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CurrenciesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CurrenciesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CurrenciesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
