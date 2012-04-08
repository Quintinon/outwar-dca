using System;
using System.Collections.Generic;
using System.Threading;
using DCT.Outwar;
using DCT.Outwar.World;
using DCT.Pathfinding;
using DCT.Settings;
using DCT.Threading;

namespace DCT.UI
{
    partial class CoreUI
    {
        #region Attacking

        private void SetUpHandler()
        {
            List<Account> accounts = new List<Account>();
            if (AccountsPanel.CheckedIndices.Count > 0)
            {
                foreach (int i in AccountsPanel.CheckedIndices)
                {
                    accounts.Add(AccountsPanel.Engine[i]);
                }
            }
            else if (AccountsPanel.FocusedAccount != null)
            {
                accounts.Add(AccountsPanel.Engine[AccountsPanel.FocusedAccount.Index]);
            }
            else if(AccountsPanel.Engine.Count > 0)
            {
                accounts.Add(AccountsPanel.Engine[0]);
            }

            AttackHandler.Set(accounts, Settings.AttackMode);
        }

        internal void AttackArea()
        {
            SetUpHandler();
            AttackHandler.BeginArea();
        }

        internal void DoAttackArea()
        {
            Globals.AttackOn = true;

            AccountsPanel.Engine.MainAccount.Mover.CoverArea();

            if (!Globals.AttackMode)
            {
                LogPanel.Log("Single-area coverage quit");
                StopAttacking(true);
            }
        }

        internal void AttackAreas()
        {
            if (RoomsPanel.CheckedRooms.Count < 1)
            {
                LogPanel.Log("E: Choose at least 1 area to cover");
                return;
            }

            Dictionary<int, string> rooms = new Dictionary<int, string>();
            foreach (int i in RoomsPanel.CheckedIndices)
            {
                rooms.Add(int.Parse(RoomsPanel.Rooms[i].SubItems[1].Text), RoomsPanel.Rooms[i].Text);
            }

            SetUpHandler();
            AttackHandler.BeginMultiArea(rooms);
        }

        internal void DoAttackMultiAreas(Dictionary<int, string> rooms)
        {
            // TODO shouldn't need room name as string...

            Account mAccount = AccountsPanel.Engine.MainAccount;
            List<string> done = new List<string>();
            foreach (int rm in rooms.Keys)
            {
                if (!Globals.AttackMode || mAccount.Mover.Location == null)
                    goto quit;

                if (done.Contains(rooms[rm]))
                    continue;

                done.Add(rooms[rm]);

                Globals.AttackOn = false;

                mAccount.Mover.PathfindTo(rm);

                if (!Globals.AttackMode)
                    goto quit;

                Globals.AttackOn = true;

                AccountsPanel.Engine.MainAccount.Mover.CoverArea();

                if (!Globals.AttackOn || !Globals.AttackMode)
                    goto quit;
            }
            return;
            quit:
            LogPanel.Log("Multi-area coverage quit");
            StopAttacking(true);
        }

        internal void AttackMobs()
        {
            if (MobsPanel.CheckedMobs.Count < 1)
            {
                LogPanel.Log("E: Choose at least 1 mob to attack");
                return;
            }
            
            // sort by value - ie., sort by room number
            List<AttackHandler.MobArg> mobs = new List<AttackHandler.MobArg>();
            
            foreach (DCT.UI.MobsPanel_v2.DataGridMob dgm in MobsPanel.CheckedMobs)
            {
                for(int i = 0; i < dgm.Ids.Count; i++)
                {
                    int id = (int)dgm.Ids[i];
                    int room = (int)dgm.Rooms[i];
                    string name = dgm.Name;
                    AttackHandler.MobArg arg = new AttackHandler.MobArg(id, room, name);
                    if (!mobs.Contains(arg))
                    {
                        mobs.Add(arg);
                    }
                }
            }
            mobs.Sort(
              delegate(
                AttackHandler.MobArg first,
                AttackHandler.MobArg second)
              {
                  return second.RoomId.CompareTo(first.RoomId);
              }
              );

            SetUpHandler();
            AttackHandler.BeginMobs(mobs);
        }

        internal void DoAttackMobs(List<AttackHandler.MobArg> mobs)
        {
            // TODO: Clean this crap up ... what is all this Globals AttackOn checking ...
            Account mAccount = AccountsPanel.Engine.MainAccount;
            // Generate list of potential rooms to visit
            List<int> rooms = new List<int>();

            foreach (AttackHandler.MobArg arg in mobs)
            {
                if (rooms.Contains(arg.RoomId) == false)
                    rooms.Add(arg.RoomId);
            }
            bool quit = false;
            while (rooms.Count > 0)
            {
                if (!Globals.AttackMode || mAccount.Mover.Location == null)
                {
                    quit = true;
                    break;
                }

                Globals.AttackOn = false;

                int r = mAccount.Mover.PathfindTo(rooms);
                rooms.Remove(r);

                if (!Globals.AttackMode)
                {
                    quit = true;
                    break;
                }

                Globals.AttackOn = true;

                foreach (AttackHandler.MobArg arg in mobs)
                {
                    if (r == arg.RoomId)
                    {
                        //if Arg.Id < 0, supposidly a spawn
                        if (arg.Id < 0)
                            AccountsPanel.Engine.MainAccount.Mover.Location.AttackMob(arg.Name);
                        else
                            AccountsPanel.Engine.MainAccount.Mover.Location.AttackMob(arg.Id);
                    }
                }

                if (!Globals.AttackOn)
                    break;
            }
            if (quit)
            {
                LogPanel.Log("Mob coverage quit");
                StopAttacking(true);
            }
            /*foreach (AttackHandler.MobArg arg in mobs)
            {
                if (!Globals.AttackMode || mAccount.Mover.Location == null)
                    goto quit;

                Globals.AttackOn = false;

                mAccount.Mover.PathfindTo(arg.RoomId);

                if (!Globals.AttackMode)
                    goto quit;

                Globals.AttackOn = true;

                if (arg.Id < 0)
                {
                    // userspawn
                    AccountsPanel.Engine.MainAccount.Mover.Location.AttackMob(arg.Name);
                }
                else
                {
                    AccountsPanel.Engine.MainAccount.Mover.Location.AttackMob(arg.Id);
                }
                if (!Globals.AttackOn)
                {
                    return;
                }
            }
            return;
            quit:
            LogPanel.Log("Mob coverage quit");
            StopAttacking(true);*/
        }

        internal void AttackRooms()
        {
            List<int> rooms = new List<int>();
            foreach (int i in RoomsPanel.CheckedIndices)
            {
                rooms.Add(int.Parse(RoomsPanel.Rooms[i].SubItems[1].Text));
            }
            rooms.Sort();

            SetUpHandler();
            AttackHandler.BeginRooms(rooms);
        }

        internal void DoAttackRooms(List<int> rooms)
        {
            Account mAccount = AccountsPanel.Engine.MainAccount;

            bool quit = false;
            while (rooms.Count > 0)
            {
                if (!Globals.AttackMode || mAccount.Mover.Location == null)
                {
                    quit = true;
                    break;
                }

                Globals.AttackOn = false;

                int r = mAccount.Mover.PathfindTo(rooms);
                rooms.Remove(r);

                if (!Globals.AttackMode)
                {
                    quit = true;
                    break;
                }

                Globals.AttackOn = true;

                mAccount.Mover.Location.Attack();
                if (!Globals.AttackOn)
                {
                    return;
                }
            }
            if (quit)
            {
                LogPanel.Log("Rooms coverage quit");
                StopAttacking(true);
            }
            /*foreach (int rm in rooms)
            {
                if (!Globals.AttackMode || mAccount.Mover.Location == null)
                    goto quit;

                Globals.AttackOn = false;

                mAccount.Mover.PathfindTo(rm);

                if (!Globals.AttackMode)
                    goto quit;

                Globals.AttackOn = true;

                AccountsPanel.Engine.MainAccount.Mover.Location.Attack();
                if (!Globals.AttackOn)
                {
                    return;
                }
            }
            return;
        quit:
            LogPanel.Log("Rooms coverage quit");
            StopAttacking(true);*/
        }

        internal void AttackQuests()
        {
            SetUpHandler();
            AttackHandler.BeginQuests();
        }

        internal void DoAttackQuests()
        {
            Account mAccount = AccountsPanel.Engine.MainAccount;

            bool AllCompleted = false;
            List<string> mobs = new List<string>();
            List<int> amount = new List<int>();
            while (Globals.AttackMode && !AllCompleted)
            {
                //We need to Enum our quests
                mAccount.RefreshQuests();

                AllCompleted = true;
                foreach (Quest q in mAccount.Quests)
                {
                    if(q.Completed() == false)
                    {
                        AllCompleted = false;
                        foreach (QuestObjective qo in q.Objectives)
                        {
                            if(MobsPanel.Mobs.Contains(new DCT.UI.MobsPanel_v2.DataGridMob(qo.Name)))
                            {
                                if(mobs.Contains(qo.Name))
                                {
                                    amount[mobs.IndexOf(qo.Name)] += qo.Amount - qo.AmountDone;
                                }
                                else
                                {
                                    mobs.Add(qo.Name);
                                    amount.Add(qo.Amount - qo.AmountDone);
                                }
                            }
                            /* TODO
                             * else
                             * Find item in item database and go kill THAT mob
                             */
                        }
                    }
                }

                DoAttackQuestMobs(mobs, amount);
            }
        }

        private void DoAttackQuestMobs(List<string> mobs, List<int> amount)
        {

            // Copied from DoAttackMobs
            // TODO: Clean this crap up ... what is all this Globals AttackOn checking ...
            Account mAccount = AccountsPanel.Engine.MainAccount;
            // Generate list of potential rooms to visit
            List<int> rooms = new List<int>();

            foreach (string mob in mobs)
            {
                int index = MobsPanel.Mobs.IndexOf(new DCT.UI.MobsPanel_v2.DataGridMob(mob));
                DCT.UI.MobsPanel_v2.DataGridMob dgm = MobsPanel.Mobs[index];
                foreach(int roomId in dgm.Rooms)
                    if(rooms.Contains(roomId) == false)
                        rooms.Add(roomId);
            }
            bool quit = false;
            while (rooms.Count > 0)
            {
                if (!Globals.AttackMode || mAccount.Mover.Location == null)
                {
                    quit = true;
                    break;
                }

                Globals.AttackOn = false;

                int r = mAccount.Mover.PathfindTo(rooms);
                if (r == -1)
                {
                    quit = true;
                    break;
                }
                else if (r < 0)
                    rooms.Remove(-r);
                else
                    rooms.Remove(r);

                if (!Globals.AttackMode)
                {
                    quit = true;
                    break;
                }

                Globals.AttackOn = true;

                for(int i = 0; i < mobs.Count; i++)
                {
                    amount[i] -= AccountsPanel.Engine.MainAccount.Mover.Location.AttackMobs(mobs[i], amount[i]);
                    /*if (r == arg.RoomId)
                    {
                        //if Arg.Id < 0, supposidly a spawn
                        if (arg.Id < 0)
                            AccountsPanel.Engine.MainAccount.Mover.Location.AttackMob(arg.Name);
                        else
                            AccountsPanel.Engine.MainAccount.Mover.Location.AttackMob(arg.Id);
                    }*/
                }

                if (!Globals.AttackOn)
                    break;
            }
            if (quit)
            {
                LogPanel.Log("Mob coverage quit");
                StopAttacking(true);
            }
        }
        #endregion

        #region Pathfinding

        internal void InvokePathfind(int room)
        {
            if (AccountsPanel.CheckedIndices.Count < 1)
            {
                LogPanel.Log("E: Check the accounts you want to move");
                return;
            }
            if (!Pathfinder.Exists(room))
            {
                LogPanel.Log("E: Select a room that exists in the map database.  If you are moving to a mob, its room information may be invalid.");
                return;
            }

            InvokeBulkMove(room);
        }

        private class BulkMoveArg
        {
            internal int AcccountIndex {get;private set;}
            internal int Room { get; private set; }
            internal ManualResetEvent Handle {get; private set;}
            public BulkMoveArg(int accountIndex, int room, ManualResetEvent handle)
            {
                AcccountIndex = accountIndex;
                Room = room;
                Handle = handle;
            }
        }

        internal void InvokeBulkMove(int room)
        {
            Toggle(false);

            int[] indices = new int[AccountsPanel.CheckedIndices.Count];
            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = AccountsPanel.CheckedIndices[i];
            }

            BulkMoveHandler d = DoBulkMove;
            d.BeginInvoke(indices, room, BulkMoveCallback, d);
        }

        private void BulkMoveCallback(IAsyncResult ar)
        {
            BulkMoveHandler d = (BulkMoveHandler)ar.AsyncState;
            d.EndInvoke(ar);

            Toggle(true);
        }

        private delegate void BulkMoveHandler(int[] indices, int room);
        private void DoBulkMove(int[] indices, int room)
        {
            ManualResetEvent[] doneEvents = new ManualResetEvent[indices.Length];
            int i = 0;
            foreach (int index in indices)
            {
                doneEvents[i] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(DoBulkMoveCallback, new BulkMoveArg(index, room, doneEvents[i]));
                i++;

                //DoPathfind(index, room);
            }

            WaitHandle.WaitAll(doneEvents);
        }

        // callback for threadpool
        private void DoBulkMoveCallback(object context)
        {
            BulkMoveArg a = (BulkMoveArg)context;
            DoPathfind(a.AcccountIndex, a.Room);
            a.Handle.Set();
        }

        private void DoPathfind(int accountIndex, int room)
        {
            AccountsPanel.Engine[accountIndex].Mover.RefreshRoom();
            AccountsPanel.Engine[accountIndex].Mover.PathfindTo(room);
        }

        #endregion

        #region Training

        internal void InvokeTraining(bool returnToStart)
        {
            if (AccountsPanel.CheckedIndices.Count < 1)
            {
                LogPanel.Log("E: Check the accounts you want to move");
                return;
            }

            Toggle(false);

            foreach (int index in AccountsPanel.CheckedIndices)
            {
                AccountsPanel.Engine[index].Mover.RefreshRoom();
                AccountsPanel.Engine[index].Mover.ReturnToStartHandler.SetOriginal();
                ThreadEngine.DefaultInstance.Enqueue(AccountsPanel.Engine[index].Mover.Train);
            }

            // TODO this needs to go - convert to ThreadPool
            ThreadEngine.DefaultInstance.ProcessAll();

            if (returnToStart)
            {
                foreach (int index in AccountsPanel.CheckedIndices)
                {
                    AccountsPanel.Engine[index].Mover.ReturnToStartHandler.InvokeReturn();
                    // TODO not necessary to thread this - it already is threaded
                    //InvokeReturn(index);
                }
            }

            Toggle(true);
        }

        #endregion
    }
}