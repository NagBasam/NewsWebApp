using HRA.News.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRA.News.Infrastructure.Interfaces
{
    public interface IArticlesRepository
    {
        /// <summary>
        /// save articles
        /// </summary>
        /// <param name="article">article info</param>
        void SaveArticle(Article article);

        /// <summary>
        /// Get all articles
        /// </summary>
        /// <param name="language">user culture</param>
        IEnumerable<Article> GetAllArticles(string language);

        /// <summary>
        /// Get latest article
        /// </summary>
        /// <param name="language">user culture</param>
        IEnumerable<Article> GetLatestPublisedArticle(string language);

        /// <summary>
        /// Get article by id
        /// </summary>
        /// <param name="id">article id</param>
        Article GetArticleById(int id);

        /// <summary>
        /// Get searched articles
        /// </summary>
        /// <param name="searchTerm">searched word</param>
        /// <param name="language">boolean value to refresh data</param>
        IEnumerable<Article> SearchAllArticles(string language, string searchTerm);
    }
}
