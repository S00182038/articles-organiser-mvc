using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using ArticlesOrganiser.Contracts;
using ArticlesOrganiser.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticlesOrganiser.Core
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly DynamoDBContext _context;
        AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
       
        public ArticlesRepository()
        {
            clientConfig.RegionEndpoint = RegionEndpoint.EUWest1;
            _client = new AmazonDynamoDBClient(clientConfig);
            _context = new DynamoDBContext(_client);
        }

        public async Task Add(ArticleInputModel entity)
        {
            var article = new Article
            {
                Id = Guid.NewGuid(),             
                Title = entity.Title,
                Url = entity.Url,
                createCollection = entity.CreateCollection,
                AddedOn = DateTime.Now
                
            };

            await _context.SaveAsync<Article>(article);
        }

        public async Task<ArticleViewModel> All(string paginationToken = "")
        {
            // Get the Table ref from the Model
            var table = _context.GetTargetTable<Article>();

            // If there's a PaginationToken
            // Use it in the Scan options
            // to fetch the next set
            var scanOps = new ScanOperationConfig();
            
            if (!string.IsNullOrEmpty(paginationToken))
            {
                scanOps.PaginationToken = paginationToken;
            }

            // returns the set of Document objects
            // for the supplied ScanOptions
            var results = table.Scan(scanOps);
            List<Document> data = await results.GetNextSetAsync();

            // transform the generic Document objects
            // into our Entity Model
            IEnumerable<Article> readers = _context.FromDocuments<Article>(data);

            // Pass the PaginationToken
            // if available from the Results
            // along with the Result set
            return new ArticleViewModel
            {
                PaginationToken = results.PaginationToken,
                Articles = readers,
                ResultsType = ResultsType.List
            };

            /* The Non-Pagination approach */
            //var scanConditions = new List<ScanCondition>() { new ScanCondition("Id", ScanOperator.IsNotNull) };
            //var searchResults = _context.ScanAsync<Article>(scanConditions, null);
            //return await searchResults.GetNextSetAsync();
        }

        public async Task<IEnumerable<Article>> Find(SearchRequest searchReq)
        {
            var scanConditions = new List<ScanCondition>();
            if (!string.IsNullOrEmpty(searchReq.Title))
                scanConditions.Add(new ScanCondition("Url", ScanOperator.Equal, searchReq.Title));
            if (!string.IsNullOrEmpty(searchReq.Url))
                scanConditions.Add(new ScanCondition("Title", ScanOperator.Equal, searchReq.Url));
            //if (!string.IsNullOrEmpty(searchReq.Cr))
            //    scanConditions.Add(new ScanCondition("Name", ScanOperator.Equal, searchReq.Name));

            return await _context.ScanAsync<Article>(scanConditions, null).GetRemainingAsync();
        }

        public async Task Remove(Guid articleId)
        {
            await _context.DeleteAsync<Article>(articleId);
        }

        public async Task<Article> Single(Guid articleId)
        {
            return await _context.LoadAsync<Article>(articleId); //.QueryAsync<Article>(articleId.ToString()).GetRemainingAsync();
        }

        public async Task Update(Guid articleId, ArticleInputModel entity)
        {
            var article = await Single(articleId);

            article.Title = entity.Title;
            article.Url = entity.Url;
            article.createCollection = entity.CreateCollection;
           await _context.SaveAsync<Article>(article);
        }
    }
}
