using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRA.News.Infrastructure.Interfaces;
using HRA.News.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HRA.News.Web.Pages.News
{
    public class DetailsModel : PageModel
    {
        private readonly IArticlesRepository _articlesRepository;
        public Article Article { get; set; }
        public DetailsModel(IArticlesRepository articleRepository)
        {
            this._articlesRepository = articleRepository;
        }

        public void OnGet(int id, string language)
        {
            Article = _articlesRepository.GetArticleById(id);
            TempData["hideSearchFilter"] = true;
            TempData["language"] = language;
        }
    }
}
