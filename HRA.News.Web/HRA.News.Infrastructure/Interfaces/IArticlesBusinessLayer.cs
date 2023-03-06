using HRA.News.Infrastructure.Models;
using System.Collections.Generic;


namespace HRA.News.Infrastructure.Interfaces
{
   public interface IArticlesBusinessLayer
    {
        /// <summary>
        /// Get all articles
        /// </summary>
        /// <param name="refresh">boolean value to refresh data</param>
        /// <param name="language">user culture</param>
        IEnumerable<Article> GetAllArticles(bool refresh, string language);

        /// <summary>
        /// Get searched articles
        /// </summary>
        /// <param name="searchTerm">searched word</param>
        /// <param name="language">boolean value to refresh data</param>
        IEnumerable<Article> SearchAllArticles(string language, string searchTerm);

        /// <summary>
        /// Get article by id
        /// </summary>
        /// <param name="id">article id</param>
        Article GetArticleById(int id);

        /// <summary>
        /// Get language Dropdown values
        /// </summary>
        IEnumerable<LanguagesDrp> GetLanguagesDrps();
    }
}
