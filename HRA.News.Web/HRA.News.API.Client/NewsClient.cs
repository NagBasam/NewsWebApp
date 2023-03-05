using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System;

namespace HRA.News.API.Client
{
    public class NewsClient : INewsClient
    {
        public ArticlesResult GetArticles(string apikey, Languages language, int pageSize, int pageNum, DateTime fromDate, DateTime toDate)
        {
            var newsApiClient = new NewsApiClient(apikey);
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = "apple",
                SortBy = SortBys.PublishedAt,
                Language = language,
                PageSize = pageSize,
                Page = pageNum,
                From = fromDate,
                To = toDate,
            });
            if (articlesResponse.Status == Statuses.Ok)
            {
                return articlesResponse;
            }
            return articlesResponse;
        }
    }
}
