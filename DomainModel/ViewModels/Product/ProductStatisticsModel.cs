using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ViewModels.Product
{
    public class ProductStatisticsModel
    {
        public int TotalProducts { get; set; }

        public int AvailableProducts { get; set; }

        public int UnavailableProducts { get; set; }

        public double AveragePoint { get; set; }
    }
}