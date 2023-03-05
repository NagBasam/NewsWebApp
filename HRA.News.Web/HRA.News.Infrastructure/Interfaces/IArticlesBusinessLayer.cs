using HRA.News.Infrastructure.Models;
using System.Collections.Generic;


namespace HRA.News.Infrastructure.Interfaces
{
   public interface IArticlesBusinessLayer
    {
        IEnumerable<Article> GetAllArticles(bool refresh, string language);
        IEnumerable<Article> SearchAllArticles(string language, string searchTerm);
        Article GetArticle(int id);
        IEnumerable<LanguagesDrp> GetLanguagesDrps();
    }
}
