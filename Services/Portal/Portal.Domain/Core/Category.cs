using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domain.Core
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        
        public virtual ICollection<BookCategory> BookCategories { get; set; }
        public Category():base()
        {
            IsActive = true;
        }
    }
}
