using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiProj.Models
{
    // TODO: sprawdzić czy potrzeba dać observableObject tak jak w klasie Product 
    public class Tag
    {
        public int Id { get; set; }
        public string Name {  get; set; }


        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Tag))
                return false;
            else
                return this.Id == ((Tag)obj).Id;
        }

    }


}
