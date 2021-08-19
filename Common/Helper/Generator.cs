using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public interface IGenerator
    {
        string GetHashPasswork(string password);
    }
    public class Generator : IGenerator
    {
        public string GetHashPasswork(string password)
        {
            return string.Empty;
        }
    }
}
