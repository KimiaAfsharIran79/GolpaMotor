using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ViewModels.Warranty
{
    public class WarrantyCardListItem
    {
        public long WarrantyCardID { get; set; }

        public string ProductName { get; set; }

        public string SerialNumber { get; set; }

        public string WarrantyCode { get; set; }

        public bool IsActive { get; set; }
    }
}
