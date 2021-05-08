namespace ArticlesOrganiser.Contracts
{
    public class ArticleInputModel
    {
     
        public string Title { get; set; }
        public string Url { get; set; }
        public bool CreateCollection { get; set; }
        public InputType InputType { get; set; }
    }
}
