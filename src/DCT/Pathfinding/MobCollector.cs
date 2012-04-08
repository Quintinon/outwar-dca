using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using DCT.Outwar.World;
using DCT.Protocols.Http;
using DCT.Util;

namespace DCT.Pathfinding
{
    /// <summary>
    /// Static class that keeps track of mobs collected and submits them for inclusion in the mob list.
    /// </summary>
    internal static class MobCollector
    {
        private const string SUBMIT_URL = "http://typpo.us/submit_mobs.php";

        private static List<MappedMob> mMobs;
        private static List<MappedMob> mSubmitted;

        internal static int Count
        {
            get { return mMobs.Count; }
        }

        static MobCollector()
        {
            mMobs = new List<MappedMob>();
            mSubmitted = new List<MappedMob>();
        }

        internal static void Add(Mob mb)
        {
            MappedMob mm = new MappedMob(mb.Name, mb.Id, mb.Room.Id, mb.Level, mb.Rage);
            if (!mMobs.Contains(mm) && !mSubmitted.Contains(mm))
            {
                mMobs.Add(mm);
            }
        }

        internal static void Submit()
        {
            // TODO: Make this update OUR database of monsters
            StringBuilder sb = new StringBuilder();
            foreach (MappedMob mb in mMobs)
            {
                sb.AppendLine(mb.ToString());
            }

            // send data
            //HttpSocket s = new HttpSocket();
            //s.Post(SUBMIT_URL, string.Format("mobs={0}", HttpUtility.UrlEncode(sb.ToString())));

            FileIO.SaveFileFromString("Export New Monsters", "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                "DCT mobs.txt", sb.ToString());

            mSubmitted.AddRange(mMobs);
            mMobs.Clear();
        }
    }
}
