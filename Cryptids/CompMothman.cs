using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Cryptids
{
    public class CompMothman : ThingComp
    {
        public MothmanState state;

        // may be required?
        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref state, "state", MothmanState.Wander);
        }
    }

    public enum MothmanState
    {
        Wander,
        Hunt,
        Flee
    }
}
