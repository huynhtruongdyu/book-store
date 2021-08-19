using System.Collections.Generic;

namespace Portal.Domain.Core
{
    public class Language : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Language() : base()
        {
            IsActive = true;
        }
    }
}