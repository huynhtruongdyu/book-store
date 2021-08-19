using System.Collections.Generic;

namespace Portal.Domain.Core
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Author() : base()
        {
            IsActive = true;
        }
    }
}