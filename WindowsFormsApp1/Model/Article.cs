using System;
using ServiceStack.DataAnnotations;


namespace WindowsFormsApp1.Model
{
    public class Article
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string url { get; set; }
        public DateTime Date{ get; set; }
        public string Html { get; set; }
        public string Text { get; set; }
    }
}
