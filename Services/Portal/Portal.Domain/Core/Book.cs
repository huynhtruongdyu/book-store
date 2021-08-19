using System.Collections.Generic;

namespace Portal.Domain.Core
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string ISBN { get; set; }
        public string CoverUrl { get; set; }
        public int? Pages { get; set; }
        public int? Rate { get; set; }
        public int? Year { get; set; }
        public bool IsActive { get; set; }

        //Publisher
        public int? PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }

        //Language
        public int? LanguageId { get; set; }
        public virtual Language Language { get; set; }

        //Author
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }

        //Book Categories
        public virtual ICollection<BookCategory> BookCategories { get; set; }

        public Book() : base()
        {
            IsActive = true;
        }
    }
}