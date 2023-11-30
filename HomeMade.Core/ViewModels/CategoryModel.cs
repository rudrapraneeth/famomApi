using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Core.ViewModels
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public int Key { get; set; }
        public bool IsPicked { get; set; } = false;
    }
}
