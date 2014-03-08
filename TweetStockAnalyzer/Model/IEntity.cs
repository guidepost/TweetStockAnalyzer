using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetStockAnalyzer.Model
{
    public interface IEntity
    {
        DateTime UpdateDate { get; set; }
        DateTime RegisterDate { get; set; }
    }
}
