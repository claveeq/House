﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House
{
    class RoomWithDoor : RoomWithHidingPlace, IHasExteriorDoor
    {
        public RoomWithDoor(string name, string decoration, string doorDecoration, string hidingPlace) : base(name, decoration,hidingPlace)
        {
            this.DoorDescription = doorDecoration;
        }

        public string DoorDescription {  get;  private set;  }

        // The DoorLocation property goes here 
        // The read-only DoorDescription property goes here


        public Location DoorLocation { get; set; }
    }
}
