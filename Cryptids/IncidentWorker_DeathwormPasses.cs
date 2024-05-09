using RimWorld;
using UnityEngine;
using Verse;

namespace Cryptids
{
    public class IncidentWorker_DeathwormPasses : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            var map = (Map)parms.target;

            if (map.Biome != BiomeDefOf.Desert && map.Biome != BiomeDef.Named("ExtremeDesert"))
            {
                return false;
            }

            return TryFindEntryCell(map, out _);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            var map = (Map) parms.target;

            if (!TryFindEntryCell(map, out var cell)) // if it doesn't find a valid entry cell
            {
                return false;
            }

            PawnKindDef deathworm = PawnKindDef.Named("MongolianDeathWorm");

            var value = GenMath.RoundRandom(StorytellerUtility.DefaultThreatPointsNow(map) / deathworm.combatPower); // get a number to spawn based on combat power?
            var max = Rand.RangeInclusive(4, 8); // max number to spawn?
            value = Mathf.Clamp(value, 1, max);

            int num = Rand.RangeInclusive(90000, 150000); // how long they stay in the map

            Pawn pawn = null;

            for (int i = 0; i < value; i++)
            {
                var location = CellFinder.RandomClosewalkCellNear(cell, map, 10);

                pawn = PawnGenerator.GeneratePawn(deathworm, Find.FactionManager.FirstFactionOfDef(FactionDef.Named("Cryptids")));
                GenSpawn.Spawn(pawn, location, map, Rot4.Random);

                pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + num;
            }

            SendStandardLetter("LetterLabelDeathwormPasses".Translate(deathworm.label).CapitalizeFirst(), 
                "LetterDeathwormPasses".Translate(deathworm.label), LetterDefOf.ThreatBig, parms, pawn);
            return true;
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f);
        }
    }
}