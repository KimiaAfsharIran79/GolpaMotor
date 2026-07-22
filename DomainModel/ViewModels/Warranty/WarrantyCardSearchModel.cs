using Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ViewModels.Warranty
{
    public class WarrantyCardSearchModel :PageModel 
    {
        public string? Search { get; set; }

        public long? ProductID { get; set; }

        public bool? IsRegistered { get; set; }
       
    }
}
