using Amazon.DynamoDBv2.DataModel;
using System;

namespace ArticlesOrganiser.Contracts
{
    [DynamoDBTable("articles")]
    public class Article
    {
        [DynamoDBProperty("id")]
        [DynamoDBHashKey]
        public Guid Id { get; set; }

        [DynamoDBProperty("createCollection")]
        public bool createCollection { get; set; }

        [DynamoDBProperty("title")]
        public string Title { get; set; }

        [DynamoDBProperty("url")]
        public string Url { get; set; }

        [DynamoDBProperty("addedOn")]
        public DateTime AddedOn { get; set; }
    }
}
