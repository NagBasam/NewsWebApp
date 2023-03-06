using Dapper;
using HRA.News.Core.ApplicationDbContext;
using HRA.News.Infrastructure.Interfaces;
using HRA.News.Infrastructure.Models;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HRA.News.Core
{
   public class ArticlesRepository : IArticlesRepository
    {
        private readonly IDapperContext _context;
        public ArticlesRepository(IDapperContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to save articles
        /// </summary>
        /// <param name="article">article data</param>
        public void SaveArticle(Article article)
        {
            var query = "INSERT INTO Articles (Author, Title, Description, Content, Url, UrlToImage, PublishedAt, Language) VALUES (@Author, @Title, @Description,@Content,@Url,@UrlToImage,@PublishedAt,@Language)";
            var parameters = new DynamicParameters();
            parameters.Add("Author", article.Author, DbType.String);
            parameters.Add("Title", article.Title, DbType.String);
            parameters.Add("Description", article.Description, DbType.String);
            parameters.Add("Content", article.Content, DbType.String);
            parameters.Add("Url", article.Url, DbType.String);
            parameters.Add("UrlToImage", article.UrlToImage, DbType.String);
            parameters.Add("PublishedAt", article.PublishedAt, DbType.String);
            parameters.Add("Language", article.Language, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, parameters);
            }
        }

        /// <summary>
        /// Method to get articles
        /// </summary>
        /// <param name="language">language</param>
        public IEnumerable<Article> GetAllArticles(string language)
        {
            var query = "SELECT * FROM Articles WHERE Language = @language ORDER BY Id";
            using (var connection = _context.CreateConnection())
            {
                var articles = connection.Query<Article>(query, new { language });
                return articles;
            }
        }

        /// <summary>
        /// Method to get article by id
        /// </summary>
        /// <param name="id">article id</param>
        public Article GetArticleById(int id)
        {
            var query = "SELECT * FROM Articles WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var Article = connection.QuerySingleOrDefault<Article>(query, new { id });
                return Article;
            }
        }
        /// <summary>
        /// Method to get latest publisedDate article by language
        /// </summary>
        /// <param name="language">language</param>
        public IEnumerable<Article> GetLatestPublisedArticle(string language)
        {
            var query = "SELECT TOP 1 * FROM Articles WHERE Language = @language ORDER BY Id";
            using (var connection = _context.CreateConnection())
            {
                var article = connection.Query<Article>(query, new { language });
                return article;
            }
        }
        /// <summary>
        /// Method to get search articles
        /// </summary>
        /// <param name="language">language</param>
        /// <param name="searchTerm">searched word</param>
        public IEnumerable<Article> SearchAllArticles(string language, string searchTerm)
        {
            var query = "SELECT * FROM Articles WHERE Language = @language and Title like @searchTerm or Description like @searchTerm ORDER BY Id";
            using (var connection = _context.CreateConnection())
            {
                var articles = connection.Query<Article>(query, new { language, searchTerm = "%" + searchTerm + "%" });
                return articles;
            }
        }
    }
}
