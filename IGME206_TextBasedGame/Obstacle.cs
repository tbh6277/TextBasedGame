
namespace IGME206_TextBasedGame
{
    internal class Obstacle
    {
        protected string successMessage;
        protected string defeatMessage;
        protected bool obstacleDefeated = false;
        protected string obstacleType;
        protected Random rnd = new Random();

        internal Obstacle (string successMessage, string defeatMessage, string obstacleType)
        {
            this.defeatMessage = defeatMessage;
            this.successMessage = successMessage;
            this.obstacleType = obstacleType;
        }

        internal string ObstacleType { get { return obstacleType; } }

        internal virtual void Encounter(GhostHunter gh) { }

        internal bool ObstacleDefeated { get { return this.obstacleDefeated; } }

        protected void Success()
        {
            Console.WriteLine(this.successMessage);
            obstacleDefeated=true;
        }


        protected virtual void Failure()
        {
            Console.WriteLine("\n" + this.defeatMessage);
            Program.EndGame();

        }
    }
}
