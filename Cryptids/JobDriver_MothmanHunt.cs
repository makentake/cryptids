using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace Cryptids
{
    public class JobDriver_MothmanHunt : JobDriver
    {
        private const TargetIndex VictimInd = TargetIndex.A;

        private const float NewTargetScanRadius = 20f;

        //private const int SmearMTBTicks = 60;

        private const int CheckForCloserTargetMTB = 180;

        private static readonly int LongStunTicks = 2500;

        //private int hypnotizeEndTick;

        //private int HypnotizeDurationTicks => RevenantUtility.HypnotizeDurationSecondsFromNumColonistsCurve.Evaluate(RevenantUtility.NumSpawnedUnhypnotizedColonists(pawn.Map)).SecondsToTicks();

        //private float RevealRange => RevenantUtility.RevealRangeFromNumColonistsCurve.Evaluate(RevenantUtility.NumSpawnedUnhypnotizedColonists(pawn.Map));

        //private CompRevenant Comp => pawn.TryGetComp<CompRevenant>();

        // something to do with hypnotizing
        /*public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref hypnotizeEndTick, "hypnotizeEndTick", 0);
        }*/

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        // assigns the pawn an enemy target if it doesn't have one upon starting
        /*public override void Notify_Starting()
        {
            base.Notify_Starting();
            if (pawn.mindState.enemyTarget != null)
            {
                job.SetTarget(TargetIndex.A, pawn.mindState.enemyTarget);
            }
        }*/

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedOrNull(TargetIndex.A); // fail if the target is despawned

            // create the "job" that does the work
            Toil stalk = Toils_Combat.FollowAndMeleeAttack(TargetIndex.A, TargetIndex.B, delegate
            {
                if (!pawn.stances.FullBodyBusy)
                {
                    JobDriver curDriver = pawn.jobs.curDriver;
                    curDriver.ReadyForNextToil();
                }
            });

            // initialize stuff when meeting a pawn
            Toil toil = stalk;

            // when the stalk action finishes
            stalk.AddFinishAction(delegate
            {
                Pawn targetPawn = job.GetTarget(TargetIndex.A).Pawn;

                if (targetPawn == null || !targetPawn.Spawned || targetPawn.Map != pawn.Map || targetPawn.Downed)
                {
                    /*if (targetPawn != null)
                    {
                        Find.Anomaly.EndHypnotize(targetPawn);
                    }
                    job.SetTarget(TargetIndex.A, RevenantUtility.GetClosestTargetInRadius(pawn, 20f));
                    pawn.mindState.enemyTarget = job.GetTarget(TargetIndex.A).Pawn;
                    if (pawn.mindState.enemyTarget == null)
                    {
                        Comp.revenantState = RevenantState.Wander;
                    }*/


                }
                else
                {
                    job.SetTarget(VictimInd, targetPawn);
                    pawn.mindState.enemyTarget = targetPawn;
                }
            });

            yield return stalk; // wait until the stalking is finished
        }
    }
}
