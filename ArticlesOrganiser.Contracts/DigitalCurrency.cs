using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticlesOrganiser.Contracts
{
    [DynamoDBTable("Currencies")]
    public class DigitalCurrency
    {
        [DynamoDBProperty("Id")]
        [DynamoDBHashKey]
        public Guid Id { get; set; }


        [DynamoDBProperty("Bitcoin")]
        public string Bitcoin { get; set; }

        [DynamoDBProperty("Etherum")]
        public string Etherum { get; set; }

        [DynamoDBProperty("DateTime")]
        public DateTime DateTime { get; set; }
    }
}
