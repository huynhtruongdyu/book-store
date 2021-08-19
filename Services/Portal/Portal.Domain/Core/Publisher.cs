using System.Collections.Generic;

namespace Portal.Domain.Core
{
    public class Publisher : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Publisher() : base()
        {
            IsActive = true;
        }
    }
}