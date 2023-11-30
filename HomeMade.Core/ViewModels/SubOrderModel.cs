using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Core.ViewModels
{
    public class SubOrderModel
    {
        public int MenuId { get; set; }
        public int ChefId { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
    }
}
