using DCT.Outwar.World;
using DCT.Parsing;
using DCT.Protocols.Http;
using System.Collections.Generic;
using System;
using System.Web;

namespace DCT.Outwar
{
    internal class Account
    {
        private static string security_url = "security_prompt.php";
        internal OutwarHttpSocket Socket { get; private set; }
        internal Mover Mover { get; private set; }
        internal ServerName Server { get; private set; }
        internal List<Quest> Quests { get; private set; }
        internal string Name { get; private set; }
        internal string Security { private get; set; }

        private int mRage;
        internal int Rage
        {
            get { return mRage; }
        }

        private long mGold;
        internal long Gold
        {
            get { return mGold; }
        }

        private int mLevel;
        internal int Level
        {
            get { return mLevel; }
        }
        internal long Id { get; private set; }

        private long mExp;
        internal long Exp
        {
            get { return mExp; }
        }
        internal bool NeedsLevel { get; private set; }

        internal Account(OutwarHttpSocket socket, string name, int id, ServerName server)
        {
            Socket = socket;
            Socket.Account = this;
            Name = name;
            Id = id;
            Server = server;

            mRage = -1;
            mLevel = -1;
            mExp = -1;
            mGold = -1;
            NeedsLevel = false;

            Socket = socket;
            Mover = new Mover(this, Socket);

            //DropHandlers = new List<DropHandler>();
        }

        internal void GetStats(string source)
        {
            Parser mm = new Parser(source);

            int.TryParse(mm.Parse("<b>LEVEL:</b><span id=\"level\">", "</span>").Trim(), out mLevel);
            int.TryParse(mm.Parse(">RAGE</span>:</b><span id=\"rage\">", "</span>").Trim(), out mRage);

            long.TryParse(mm.Parse("EXP:</b><span id=\"exp\">", "</span>").Trim().Replace(",", ""), out mExp);

            long.TryParse(mm.Parse("Gold:</b></td><td>", "</td>").Trim(), out mGold);

            NeedsLevel = source.Contains("LEVEL!");
        }

        /// <summary>
        /// Loads a proper Outwar page to update rage, xp, stats, etc.
        /// Because Ajax DC movement doesn't update that.
        /// </summary>
        internal void RefreshState()
        {
            // Load privacy.php, quickest loading page...
            // contact.php is around 50% faster with quick testing based on their page serv times
            Socket.Get("contact.php");
        }

        internal void RefreshQuests()
        {
            string src = Socket.Post("world_questHelper.php", "");
            src = src.Replace("\\s", "s"); // Hack for new Outwar regex
            src = System.Text.RegularExpressions.Regex.Unescape(src);

            Quests = new List<Quest>();

            string allQuests = Parser.Parse(src, "questhelper", "mobsearch");

            string[] eachQuest = src.Split(new string[] { "colspan" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < eachQuest.Length; i++)
            {
                ParseQuest(eachQuest[i]);
            }

        }
        private void ParseQuest(string src)
        {
            Quest q = new Quest();
            string[] split = src.Split(new string[] { "<td rowspan" }, StringSplitOptions.RemoveEmptyEntries);
            q.Name = ParseQuestTitle(split[0]);
            split = split[1].Split(new string[] { "getQuestHelpData2(" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < split.Length; i++)
                ParseQuestObjective(split[i], ref q);

            Quests.Add(q);
        }

        private void ParseQuestObjective(string src, ref Quest q)
        {
            // 'num', 'num', 'item name', 'num', 'num'
            // <b>Item Name</b>
            // </b> x/y </font>

            string[] split = src.Split(new string[] { "</b>" }, StringSplitOptions.RemoveEmptyEntries);
            //0 is item
            //1 is a colon
            //2 is count/total
            string nameSplit = split[0];
            string name = Parser.Parse(src, "<b>", "</a>");
                //(nameSplit.Split(new string[] { "<b>" }, StringSplitOptions.RemoveEmptyEntries))[1];
            QuestObjective qo = new QuestObjective(name);
            string countSplit = split[1];
            split = countSplit.Split(new string[] { "</a>" }, StringSplitOptions.RemoveEmptyEntries);
            split = split[0].Split(new char[] { '/', ' '}, StringSplitOptions.RemoveEmptyEntries);
            qo.AmountDone = int.Parse(split[0]);
            qo.Amount = int.Parse(split[1]);
            qo.Completed = qo.AmountDone == qo.Amount;

            q.Objectives.Add(qo);
        }

        private string ParseQuestTitle(string src)
        {
            return Parser.Parse(src, "<span style=\"font-family : arial; font-size:10pt;\">", "</span>");
        }

        /* Required Secuirty Code, which I could not get working
         * 
         * internal void RefreshQuests()
        {
            Quests = new List<Quest>();

            // Load questlog.php and parse
            string src = Socket.Get("questlog.php");
            // CAN REQUEST FOR SECURITY CODE
            // put a spot on gui for this and use it!
            if (src.Contains("security_prompt.php"))
            {
                string temp = SendSecurityCode(src);
                //src = Socket.Get("questlog.php");
                src = temp;
            }




            // Start of Whole table
            // <table border="0" cellpadding="0" cellspacing="8" style="border-collapse: collapse; width:100%;" width="100%">
            // OR
            //<h2>Quest Log</h2>
            /* End of whole table
             *      </td>
             *		</tr>
             *		</table>
             *
            // Or <div align="center">
            string allQuests = Parser.Parse(src, "<h2>Quest Log</h2>", "<div align=\"center\">");

            //Each Quest is in its own table
            string[] eachQuest = Parser.MultiParse(allQuests, "<table>", "</table>");

            for (int i = 0; i < eachQuest.Length; i++)
            {
                if (eachQuest[i] != null && eachQuest[i].Equals("ERROR") == false)
                {
                    ParseQuest(eachQuest[i]);
                }
            }
        }

        private void ParseQuest(string src)
        {
            // First <b> </b> tags are NPC Name
            Quest q = new Quest();

            string[] bold = Parser.MultiParse(src, "<b>", "</b>");
            if (bold.Length < 2)
                return;

            q.QuestGiverName = bold[0];
            q.Name = bold[1];

            string[] font = Parser.MultiParse(src, "<font", "</font>");
            for (int i = 1; i < font.Length; i++) // First font tag is for title of quest
            {
                ParseQuestObjective(font[i], ref q);
            }
        }

        private void ParseQuestObjective(string src, ref Quest q)
        {
            string questName = Parser.Parse(src, "<b>", ":");
            QuestObjective qo = new QuestObjective(questName);
            string status = Parser.Parse(src, "</b>", " ");
            string[] statuses = status.Split(new char[] { '/' });
            if (status.Length >= 2)
            {
                qo.AmountDone = int.Parse(statuses[0]);
                qo.Amount = int.Parse(statuses[1]);
            }
            q.Objectives.Add(qo);
        }

        private string SendSecurityCode(string src)
        {
            string promptNumber = Parser.Parse(src, "prompt_number\" value=\"", "\"");
            return Socket.Post(security_url, string.Format("prompt_number={0}&answer={1}&security_submitted=Continue", HttpUtility.UrlEncode(promptNumber), HttpUtility.UrlEncode(Security)));
        }*/
    }
}