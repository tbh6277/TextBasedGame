using System.Collections;

namespace IGME206_TextBasedGame
{
    internal class CauseOfDeath
    {
        // Causes listed in ascending order of aggregation
        private static string[] POSSIBLE_CAUSES = { "old age", "illness", "unknown", "accident", "murder" };
        private string cause;
        private string description;
        private int causeIndex;
        private Ghost? personResponsible;
        private Item? weapon;

        internal CauseOfDeath(
            string cause = "old age",
            string description = " lived a long life before dying peacefully in their sleep.")
        {
            this.cause = cause;
            this.description = description;
            this.causeIndex = Array.IndexOf(POSSIBLE_CAUSES, cause);
            if(causeIndex == -1) { causeIndex = 0; }
        }

        internal Ghost PersonResponsible
        {
            set { this.personResponsible = value; }
        }

        internal Item Weapon
        {
            set { this.weapon = value; }
        }

        internal int CauseIndex { get { return this.causeIndex; } }

        internal string Description
        {
            get { return this.description; }
        }

        internal string[] PossibleCauses
        {
            get { return POSSIBLE_CAUSES; }
        }

        internal string Cause
        {
            get { return this.cause; }
        }

    }

 }
