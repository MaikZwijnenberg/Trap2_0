using System.Collections.Generic;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace Trap2_0.Models
{
    public class InputGroup
    {
        public List<Beam> Beams { get; set; }

        public InputGroup()
        {
            Beams = new List<Beam>();
        }
    }
}
