namespace Mars.WalkingRobots.Lib.Models
{
    public abstract class Command
    {
        public CommandType CommandType;

        protected Command(CommandType commandType)
        {
            CommandType = commandType;
        }
    }
}