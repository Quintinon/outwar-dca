using System;
using System.Collections.Generic;
using System.Text;

namespace DCT.Outwar.World
{
    internal class QuestObjective
    {
        internal string Name { get; set; }
        internal int Amount { get; set; }
        internal int AmountDone { get; set; }
        internal bool Completed { get; set; }
        // Type?

        public QuestObjective(string _Name)
        {
            Name = _Name;
            Amount = 1;
            Completed = false;
        }

        public QuestObjective(string _Name, int _Amount)
        {
            Name = _Name;
            Amount = _Amount;
            Completed = false;
        }
    }
}
