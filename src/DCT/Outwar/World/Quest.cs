using System;
using System.Collections.Generic;
using System.Text;

namespace DCT.Outwar.World
{
    internal class Quest
    {
        internal string Name { get; set; }
        internal int Id { get; set; }
        internal string QuestGiverName { get; set; }
        internal int QuestGiverId { get; set; }
        internal List<QuestObjective> Objectives { get; set; }

        public Quest()
        {
            Objectives = new List<QuestObjective>();
        }

        public Quest(string _Name, int _Id)
        {
            Name = _Name;
            Id = _Id;
            Objectives = new List<QuestObjective>();
        }

        public bool Completed()
        {
            foreach (QuestObjective qo in Objectives)
            {
                if (qo.Completed == false)
                    return false;
            }
            return true;
        }
    }
}
