using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RimWorld;
using Verse;
using Verse.AI;

namespace Cryptids
{
    public class JobGiver_MothmanHunt : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            return JobMaker.MakeJob(DefDatabase<JobDef>.GetNamed("MothmanHunt"), pawn.mindState.enemyTarget);
        }
    }
}
