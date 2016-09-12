using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House
{
    class OutsideWithDoor : Outside, IHasExteriorDoor
    {
        public OutsideWithDoor(string name, bool hot, string doorDecoration) : base(name, hot)
        {
            DoorDescription = doorDecoration;
        }

        // The DoorLocation property goes here  
        // The read-only DoorDescription property goes here
        public string DoorDescription { get; private set;  }


        public override string Description
        {
            get
            {
                return base.Description + " You see " + DoorDescription;
            }
        }

        public Location DoorLocation { get; set; }
    }
}
