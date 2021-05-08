using System.Collections.Generic;


namespace ArticlesOrganiser.Contracts
{
    public class ArticleViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public ResultsType ResultsType { get; set; }
        public string PaginationToken { get; set; }
    }
}
