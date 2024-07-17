using RimWorld;
using Verse;

namespace Cryptids
{
    public class IncidentWorker_MothmanAttack : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            var map = (Map)parms.target;

            if (!map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(ThingDef.Named("Mothman")))
            {
                return false;
            }

            return TryFindEntryCell(map, out _);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            var map = (Map)parms.target;

            if (!TryFindEntryCell(map, out var cell)) // if it doesn't find a valid entry cell
            {
                return false;
            }

            PawnKindDef mothman = PawnKindDef.Named("Mothman");

            Pawn pawn = null;

            var location = CellFinder.RandomClosewalkCellNear(cell, map, 10);

            pawn = PawnGenerator.GeneratePawn(mothman, Find.FactionManager.FirstFactionOfDef(FactionDef.Named("Cryptids")));
            GenSpawn.Spawn(pawn, location, map, Rot4.Random);

            SendStandardLetter("LetterLabelMothmanAttack".Translate(mothman.label).CapitalizeFirst(),
                "LetterMothmanAttack".Translate(mothman.label), LetterDefOf.ThreatBig, parms, pawn);
            return true;
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f);
        }
    }
}
