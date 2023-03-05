using HRA.News.API.Client;
using NewsAPI.Models;
using HRA.News.Infrastructure.Interfaces;
using HRA.News.Infrastructure.Models;
using System;
using System.Collections.Generic;
using Article = HRA.News.Infrastructure.Models.Article;

namespace HRA.News.Business
{
    public class ArticlesBusinessLayer : IArticlesBusinessLayer
    {
        private readonly INewsClient _newsClient;
        private readonly IArticlesRepository _articlesRepository;
        public ArticlesBusinessLayer(INewsClient newsClient, IArticlesRepository articlesRepository)
        {
            _newsClient = newsClient;
            _articlesRepository = articlesRepository;
        }
        public IEnumerable<Article> GetAllArticles(bool refresh, string language)
        {
            ArticlesResult articlesResponse;
            var data = _articlesRepository.GetPublisedData(language);
            Article a = new Article();
            foreach (Article article1 in data)
            {
                a.PublishedAt = article1.PublishedAt;
            }

            //Intial load for culture  
            if (a.PublishedAt == null)
            {
                articlesResponse = GetArticles("5570328b896a470687a1cccf5433cc45", language == "AR" ? Languages.AR : Languages.EN, 50, 1, DateTime.Today.AddDays(-10), DateTime.Now);
            }
            else if (a.PublishedAt != null && refresh)
            {
                articlesResponse = GetArticles("5570328b896a470687a1cccf5433cc45", language == "AR" ? Languages.AR : Languages.EN, 50, 1, DateTime.Parse(a.PublishedAt).AddSeconds(1), DateTime.Now);
            }
            else
            {
                return _articlesRepository.GetAllArticles(language);
            }

            foreach (var article in articlesResponse.Articles)
            {
                Article _article = new Article();
                _article.Author = article.Author;
                _article.Title = article.Title;
                _article.Description = Truncate(article.Description, int.MaxValue);
                _article.Url = article.Url;
                _article.UrlToImage = article.UrlToImage;
                _article.Content = Truncate(article.Content, int.MaxValue);
                _article.Language = language;
                _article.PublishedAt = article.PublishedAt.ToString();
                _articlesRepository.SaveArticle(_article);
            }

            return _articlesRepository.GetAllArticles(language);
        }
        public IEnumerable<Article> SearchAllArticles(string language, string searchTerm)
        {
            return _articlesRepository.SearchAllArticles(language, searchTerm);
        }
        public Article GetArticle(int id)
        {
            return _articlesRepository.GetArticle(id);
        }
        public IEnumerable<LanguagesDrp> GetLanguagesDrps()
        {
            var languages = new List<LanguagesDrp> {
                new LanguagesDrp { Text = "English", Value = "EN" },
                new LanguagesDrp { Text = "Arabic", Value = "AR" }
            };
            return languages;
        }
        private ArticlesResult GetArticles(string apiKey, Languages language, int pageSize, int page, DateTime from, DateTime to)
        {
            return _newsClient.GetArticles(apiKey, (NewsAPI.Constants.Languages)language, pageSize, page, from, to);
        }
        private static string Truncate(string value, int MaxLength) => value?.Length > MaxLength ? value.Substring(0, MaxLength) : value;
    }

}
