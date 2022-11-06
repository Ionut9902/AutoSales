using System;
using System.Collections.Generic;

namespace AutoSales.Models.DBObjects
{
    public partial class Favourite
    {
        public Guid IdPost { get; set; }
        public string IdUser { get; set; } = null!;

        public virtual Post IdPostNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
