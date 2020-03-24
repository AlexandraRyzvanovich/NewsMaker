using System;
using ServiceStack.DataAnnotations;


namespace WindowsFormsApp1.Model
{
    public class Article
    {

        public Article(string title, string url, DateTime date, string html, string text)
        {
            Title = title;
            Url = url;
            Date = date;
            Html = html;
            Text = text;
        }
        [AutoIncrement]
        [Unique]
        [PrimaryKey]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime Date{ get; set; }
        public string Html { get; set; }
        public string Text { get; set; }

        [Default (null)]
        [ForeignKey(typeof(Entities))]
        public int? EntityId { get; set; }
    }
}
