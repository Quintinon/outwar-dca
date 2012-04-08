using System;
using System.Collections.Generic;
using System.Text;
using DCT.Outwar.World;
using DCT.Util;

namespace DCT.Pathfinding
{
    internal static class RoomCollector
    {
        //private const string SUBMIT_URL = "http://typpo.us/submit_mobs.php";

        private static List<MappedRoom> mRooms;
        private static List<MappedRoom> mSubmitted;

        internal static int Count
        {
            get { return mRooms.Count; }
        }

        static RoomCollector()
        {
            mRooms = new List<MappedRoom>();
            mSubmitted = new List<MappedRoom>();
        }

        internal static void Add(Room rm)
        {
            //MappedMob mm = new MappedMob(mb.Name, mb.Id, mb.Room.Id, mb.Level, mb.Rage);
            MappedRoom mapped = new MappedRoom(rm.Id, rm.Name, rm.Links);
            if (!mRooms.Contains(mapped) && !mSubmitted.Contains(mapped))
            {
                mRooms.Add(mapped);
            }
        }

        internal static void Submit()
        {
            // TODO: Make this update OUR database of monsters
            StringBuilder sb = new StringBuilder();
            foreach (MappedRoom mb in mRooms)
            {
                sb.AppendLine(mb.ToString());
            }

            // send data
            //HttpSocket s = new HttpSocket();
            //s.Post(SUBMIT_URL, string.Format("mobs={0}", HttpUtility.UrlEncode(sb.ToString())));

            FileIO.SaveFileFromString("Export New Rooms", "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                "DCT mobs.txt", sb.ToString());

            mSubmitted.AddRange(mRooms);
            mRooms.Clear();
        }
    }
}
