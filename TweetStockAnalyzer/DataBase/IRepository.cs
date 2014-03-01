using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetStockAnalyzer.DataBase
{
    public interface IRepository<T>
    {
        T Create();
        T Read(int id);
        void Update(T valud);
        void Delte(int id);
    }
}
