using HRA.News.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace HRA.News.Web.ViewComponents
{
    public class SearchLanguageFilterViewComponent : ViewComponent
    {
        private readonly IArticlesBusinessLayer _articlesBusinessLayer;
        public SearchLanguageFilterViewComponent(IArticlesBusinessLayer articlesBusinessLayer)
        {
            this._articlesBusinessLayer = articlesBusinessLayer;
        }
        public IViewComponentResult Invoke()
        {
            var result = _articlesBusinessLayer.GetLanguagesDrps().Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Value,
                                      Text = a.Text,
                                      Selected = TempData["language"] != null && TempData["language"].ToString() == a.Value,
                                  }).ToList();
            return View(result);
        }

    }
}
