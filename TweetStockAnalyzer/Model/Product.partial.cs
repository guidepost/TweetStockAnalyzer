using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetStockAnalyzer.Model
{
    public partial class Product
    {
        [ForeignKey("CompanyProductRelations.Company")]
        public virtual IEnumerable<Company> Companies { get { return CompanyProductRelations.Select(p => p.Company); } }
    }
}
