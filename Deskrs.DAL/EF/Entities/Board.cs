using System;
using System.Collections.Generic;

namespace Deskrs.DAL.EF.Entities
{
    public partial class Board
    {
        public Board()
        {
            List = new HashSet<List>();
        }

        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string Title { get; set; }
        public string BackgroundColor { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<List> List { get; set; }
    }
}
