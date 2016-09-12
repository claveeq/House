using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House
{
    class OutsideWithHidingPlace : Outside, IHidingPlace
    {
        public OutsideWithHidingPlace(string name, bool hot, string hidingPlace) : base(name, hot)
        {
            HidingPlace = hidingPlace;
        }

      
        public string HidingPlace { get;  private set;  }

        public override string Description
        {
            get
            {
                return base.Description + " Maybe he is " + HidingPlace;
            }
        }
    }
}
