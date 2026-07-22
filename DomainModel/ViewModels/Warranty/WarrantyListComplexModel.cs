using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ViewModels.Warranty
{
    public class WarrantyListComplexModel
    {
        public List<WarrantyCardListItem> warrantyList { get; set; }

        public WarrantyCardSearchModel sm { get; set; }

        public WarrantyListComplexModel()
        {
            warrantyList = new List<WarrantyCardListItem>();
            sm = new WarrantyCardSearchModel();
        }
    }
}
