using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ViewModels.Warranty
{
    public class WarrantyCodeGeneratorViewModel
    {
        public long ProductID { get; set; }

        public long SerialFrom { get; set; }

        public long SerialTo { get; set; }

        public long CodeFrom { get; set; }

        public long CodeTo { get; set; }

        public int Count { get; set; }

        public int ValidityMonths { get; set; } = 12;

        public string? Description { get; set; }
    }
}
