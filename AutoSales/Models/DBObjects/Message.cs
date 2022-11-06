using System;
using System.Collections.Generic;

namespace AutoSales.Models.DBObjects
{
    public partial class Message
    {
        public Guid IdMessage { get; set; }
        public string Text { get; set; } = null!;
        public Guid IdUser { get; set; }
        public Guid IdPost { get; set; }

        public virtual Post IdPostNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
