using System;
using System.Collections.Generic;
using HRA.News.Infrastructure.Interfaces;
using HRA.News.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace HRA.News.Web.Pages.News
{
    public class IndexModel : PageModel
    {
        private readonly IArticlesBusinessLayer _articlesBusinessLayer;
        public IEnumerable<Article> Articles { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Refresh { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Language { get; set; }
        public IndexModel(IArticlesBusinessLayer articlesBusinessLayer)
        {
            this._articlesBusinessLayer = articlesBusinessLayer;
        }
        public void OnGet(string searchTerm, bool Refresh, string Language = "EN")
        {
            ViewData["SearchTerm"] = SearchTerm;
            TempData["language"] = Language;
            if (!searchTerm.IsNullOrEmpty())
            {
                Articles = _articlesBusinessLayer.SearchAllArticles(Language, searchTerm);
            }
            else
            {
                Articles = _articlesBusinessLayer.GetAllArticles(Refresh, Language);
            }
        }
    }
}
