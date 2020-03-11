using System;
using System.Collections.Generic;

namespace Deskrs.DAL.EF.Entities
{
    public partial class Project
    {
        public Project()
        {
            Board = new HashSet<Board>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual ICollection<Board> Board { get; set; }
    }
}
