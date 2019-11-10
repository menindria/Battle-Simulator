using System;
using System.Collections.Generic;
using BattleSimulator.CrossCutting;

namespace BattleSimulator.Domain
{
    public class Battle : EntityBase
    {
        public Battle(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentOutOfRangeException($"Battle name is mandatory");
            }

            Name = name;
        }

        public bool Started { get; private set; }
        public string Name { get; private set; }
        public List<Army> Armies { get; private set; } = new List<Army>();
        public List<Log> Logs { get; private set; } = new List<Log>();

        public IResponse AddArmy(Army army)
        {
            if (Started)
            {
                return new ErrorResponse("Battle already started");
            }

            Armies.Add(army);

            return new SuccessResponse();
        }

        public IResponse Start()
        {
            //if (Armies.Count < 10)
            //{
            //    return new ErrorResponse("There is not enough armies for battle to start");
            //}

            Started = true;
            return new SuccessResponse();
        }
        public void Stop()
        {
            Started = false;
        }
        
        public void ClearLogs()
        {
            Logs.Clear();
        }
    }
}
