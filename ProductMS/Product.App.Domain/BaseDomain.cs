using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.App.Domain
{
    public class BaseDomain
    {
        public BaseDomain()
        {
            this.CreatedAt = DateTime.Now;
            IsActive = true;
        }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdateddAt { get; set; }
    }
}
