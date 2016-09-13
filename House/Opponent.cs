using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace House
{
    class Opponent
    {
        Random random;
        private Location myLocation;

        public Opponent(Location startingLocation)
        {
           myLocation = startingLocation;
            random = new Random();
        }
        

        public void Move()
        {
            while (!(myLocation is IHidingPlace))
            {
                if (myLocation is IHasExteriorDoor)
                {
                    IHasExteriorDoor hasDoor = myLocation as IHasExteriorDoor;

                    if (random.Next(2) == 1)
                    myLocation = hasDoor.DoorLocation;
                }                            
                myLocation = myLocation.Exits[random.Next(myLocation.Exits.Length)]; 
            }
        }
        
        public bool Check(Location location)
        {
            if (myLocation == location)
            {
                return true;
            }
            return false;
        }
    }
}
