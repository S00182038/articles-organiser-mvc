using System;
using System.Threading.Tasks;
using ArticlesOrganiser.Contracts;
using ArticlesOrganiser.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesOrganiser.App.Controllers
{
    [Route("[controller]")]
    public class ArticlesController : Controller
    {
        private IArticlesRepository _repository;

        public ArticlesController(IArticlesRepository repository)
        {
            _repository = repository;
        }

        // GET: ArticlesController
        public async Task<ActionResult> Index(string title = "")
        {
            if (!string.IsNullOrEmpty(title))
            {
                var articles = await _repository.Find(new SearchRequest { Title = title });
                return View(new ArticleViewModel
                {
                    Articles = articles,
                    ResultsType = ResultsType.Search
                });
            }
            else
            {
                var articles = await _repository.All();
                return View(articles);
            }
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View("~/Views/Articles/CreateOrUpdate.cshtml");
        }

        // POST: ArticlesController/Create
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ArticleInputModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("~/Views/Articles/CreateOrUpdate.cshtml", model);

                await _repository.Add(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("~/Views/Articles/CreateOrUpdate.cshtml", model);
            }
        }

        [HttpGet]
        [Route("Edit/{articleId}")]
        public async Task<ActionResult> Edit(Guid articleId)
        {
            var article = await _repository.Single(articleId);

            ViewBag.ArticleId = articleId;

            return View("~/Views/Articles/CreateOrUpdate.cshtml", new ArticleInputModel
            {
                Title = article.Title,              
                Url = article.Url,
                CreateCollection = article.createCollection,
                InputType = InputType.Update
            });
        }

        // POST: ArticlesController/Edit/5
        [HttpPost]
        [Route("Edit/{articleId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid articleId, ArticleInputModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("~/Views/Articles/CreateOrUpdate.cshtml", model);

                await _repository.Update(articleId, model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("~/Views/Articles/CreateOrUpdate.cshtml", model);
            }
        }

        [HttpGet]
        [Route("Delete/{articleId}")]
        public async Task<ActionResult> Delete(Guid articleId)
        {
            await _repository.Remove(articleId);

            return RedirectToAction(nameof(Index));
        }
    }
}
