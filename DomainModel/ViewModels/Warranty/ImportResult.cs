using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ViewModels.Warranty
{
    public class ImportResult
    {
        public int InsertedCount { get; set; }

        public int DuplicateCount { get; set; }

        public int EmptyCount { get; set; }
    }
}
