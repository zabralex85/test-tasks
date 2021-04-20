using System.Collections.Generic;
using Mars.WalkingRobots.Lib.Models;

namespace Mars.WalkingRobots.Lib.Objects
{
    public class CommandProcessor<T> where T : Command
    {
        public Queue<T> Commands { get; set; }
        public List<T> CommandsExecuted { get; set; }

        public CommandProcessor()
        {
            Commands = new Queue<T>();
            CommandsExecuted = new List<T>();
        }
    }
}
