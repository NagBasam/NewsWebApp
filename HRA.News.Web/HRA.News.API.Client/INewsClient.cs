using NewsAPI.Constants;
using NewsAPI.Models;
using System;

namespace HRA.News.API.Client
{
    public interface INewsClient
    {
        ArticlesResult GetArticles(string apikey, Languages language, int pageSize, int pageNum, DateTime fromDate, DateTime toDate);
    }
}
