using Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ViewModels.Product
{
    public class ProductSearchModel : PageModel
    {
        public long? ProductID { get; set; }
        
        public string? ProductName { get; set; }

        public int? ProductPoint { get; set; }

        public bool? IsAvailable { get; set; }

    }
}
