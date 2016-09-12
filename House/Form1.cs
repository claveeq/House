using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace House
{
    public partial class Form1 : Form
    {
        Location currentLocation;

        Room dinningRoom;
        RoomWithDoor kitchen;
        RoomWithDoor livingRoom;

        Outside garden;
        OutsideWithDoor frontYard;
        OutsideWithDoor backYard;

        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            MoveToANewLocation(livingRoom);
        }

        private void CreateObjects()
        {

            dinningRoom = new Room("Dinning Room", "a crystal chandelier");
            kitchen = new RoomWithDoor("Kitchen", "an antique carpet", "a screen door");
            livingRoom = new RoomWithDoor("Living Room", "an antique carpet", "an oak door with a brass knob");

            garden = new Outside("Garden", false);
            frontYard = new OutsideWithDoor("Front Yard", false, "an oak door with a brass knob");
            backYard = new OutsideWithDoor("Back Yard", true, "a screen door");

            dinningRoom.Exits = new Location[] { livingRoom, kitchen };
            kitchen.Exits = new Location[] { dinningRoom };
            livingRoom.Exits = new Location[] { dinningRoom };
            garden.Exits = new Location[] { frontYard, backYard };
            frontYard.Exits = new Location[] { backYard, garden };
            backYard.Exits = new Location[] { frontYard, garden };

            livingRoom.DoorLocation = frontYard;
            frontYard.DoorLocation = livingRoom;

            kitchen.DoorLocation = backYard;
            backYard.DoorLocation = kitchen;

        }

        private void MoveToANewLocation(Location newLocation)
        {
            currentLocation = newLocation;

            exits.Items.Clear();
            for (int i = 0; i < currentLocation.Exits.Length; i++)
            {
                exits.Items.Add(currentLocation.Exits[i].Name);
            }
            exits.SelectedIndex = 0;

            description.Text = currentLocation.Description;

            if (currentLocation is IHasExteriorDoor)
            {
                goThroughTheDoor.Visible = true;
            }
            else
            {
                goThroughTheDoor.Visible = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void goHere_Click(object sender, EventArgs e)
        {
            MoveToANewLocation(currentLocation.Exits[exits.SelectedIndex]);
        }

        private void goThroughTheDoor_Click(object sender, EventArgs e)
        {
            IHasExteriorDoor hasDoor = currentLocation as IHasExteriorDoor;
            MoveToANewLocation(hasDoor.DoorLocation);
        }
    }
}
