using HRA.News.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRA.News.Infrastructure.Interfaces
{
    public interface IArticlesRepository
    {
        void SaveArticle(Article article);
        IEnumerable<Article> GetAllArticles(string language);
        IEnumerable<Article> GetPublisedData(string language);
        Article GetArticle(int id);
        IEnumerable<Article> SearchAllArticles(string language, string searchTerm);
    }
}
