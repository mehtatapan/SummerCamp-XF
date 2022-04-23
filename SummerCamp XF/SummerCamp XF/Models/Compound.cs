using System;
using System.Collections.Generic;
using System.Text;

namespace SummerCamp_XF.Models
{
    public class Compound
    {
        public Compound()
        {
            this.Campers = new HashSet<Camper>();
        }
        public int ID { get; set; }

        public string Name { get; set; }

        public byte[] RowVersion { get; set; }

        public ICollection<Camper> Campers { get; set; }
    }
}
