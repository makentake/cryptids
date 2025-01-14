using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace Cryptids
{
    public class ThinkNode_ConditionalMothmanState : ThinkNode_Conditional
    {
        public MothmanState state;

        protected override bool Satisfied(Pawn pawn)
        {
            var compMothman = pawn.TryGetComp<CompMothman>();

            if (compMothman != null)
            {
                return compMothman.state == state;
            }

            return false;
        }
    }
}
