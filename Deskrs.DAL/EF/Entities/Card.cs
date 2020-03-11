using System;
using System.Collections.Generic;

namespace Deskrs.DAL.EF.Entities
{
    public partial class Card
    {
        public long Id { get; set; }
        public long ListId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual List List { get; set; }
    }
}
