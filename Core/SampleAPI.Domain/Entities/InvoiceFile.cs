using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.Domain.Entities
{
    public class InvoiceFile : File
    {
        public float Price { get; set; }
    }
}
