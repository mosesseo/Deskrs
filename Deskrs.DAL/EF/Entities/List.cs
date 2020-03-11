using System;
using System.Collections.Generic;

namespace Deskrs.DAL.EF.Entities
{
    public partial class List
    {
        public List()
        {
            Card = new HashSet<Card>();
        }

        public long Id { get; set; }
        public long BoardId { get; set; }
        public string Title { get; set; }
        public int SortOrder { get; set; }
        public DateTime Created { get; set; }

        public virtual Board Board { get; set; }
        public virtual ICollection<Card> Card { get; set; }
    }
}
