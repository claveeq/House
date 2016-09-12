﻿using System;
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
        int num = 1;

        Location currentLocation;

        Room stairs;
        RoomWithHidingPlace upstairsHallway;
        RoomWithHidingPlace mastersBedRoom;
        RoomWithHidingPlace secondBedRoom;
        RoomWithHidingPlace bathRoom;

        Room dinningRoom;
        RoomWithDoor kitchen;
        RoomWithDoor livingRoom;

        Outside garden;
        OutsideWithDoor frontYard;
        OutsideWithDoor backYard;

        OutsideWithHidingPlace driveway;

        Opponent opponent;
        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            MoveToANewLocation(garden);
        }

        private void CreateObjects()
        {

            stairs = new Room("Stairs","a wooden banister");
            upstairsHallway = new RoomWithHidingPlace("Updstairs Hallway", "a picture of a dog","a closet to hide in." );
            mastersBedRoom = new RoomWithHidingPlace("Master's Bedroom", "a large bed", "under the bed");
            secondBedRoom = new RoomWithHidingPlace("Second Bedroom", "a small bed", "under the bed");
            bathRoom = new RoomWithHidingPlace("Master's Bedroom", "sink and a toilet", "in the shower"); ;

            dinningRoom = new Room("Dinning Room", "a crystal chandelier");
            kitchen = new RoomWithDoor("Kitchen", "an antique carpet", "a screen door","in a cabinet");
            livingRoom = new RoomWithDoor("Living Room", "an antique carpet", "an oak door with a brass knob","in a closet");

            garden = new Outside("Garden", false);
            frontYard = new OutsideWithDoor("Front Yard", false, "an oak door with a brass knob");
            backYard = new OutsideWithDoor("Back Yard", true, "a screen door");

            driveway = new OutsideWithHidingPlace("Driveway", true, "in the garage");

            dinningRoom.Exits = new Location[] { livingRoom, kitchen };
            kitchen.Exits = new Location[] { dinningRoom };
            livingRoom.Exits = new Location[] { dinningRoom , stairs};
            garden.Exits = new Location[] { frontYard, backYard };
            frontYard.Exits = new Location[] { backYard, garden , driveway};
            backYard.Exits = new Location[] { frontYard, garden , driveway};
            mastersBedRoom.Exits = new Location[] { upstairsHallway };
            secondBedRoom.Exits = new Location[] { upstairsHallway };
            bathRoom.Exits = new Location[] { upstairsHallway };
            upstairsHallway.Exits = new Location[] { mastersBedRoom, secondBedRoom, bathRoom,stairs };
            stairs.Exits = new Location[] { upstairsHallway, livingRoom };
            driveway.Exits = new Location[] { frontYard, backYard };

            livingRoom.DoorLocation = frontYard;
            frontYard.DoorLocation = livingRoom;

            kitchen.DoorLocation = backYard;
            backYard.DoorLocation = kitchen;

            opponent = new Opponent(frontYard);

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

        private void hide_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void check_Click(object sender, EventArgs e)
        {
 
            if (opponent.Check(currentLocation))
                MessageBox.Show("You caught me!");
            else
                MessageBox.Show("He's not here!");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            opponent.Move();
            description.Text = num++ + "...";
            if (num == 11)
            {
                description.Text ="Find him!";
                timer1.Enabled = true;
                exits.Visible = true;
                goHere.Visible = true;
                goThroughTheDoor.Visible = true;
                check.Visible = true;
                hide.Enabled = false;
                timer1.Stop();
            }
        }
    }
}
