namespace laba
{
    public class Program1
    {
        public static void Main()
        {
            string filePath = @"C:\Users\Nikita\source\repos\SuperMegaCoolLaboratoryNumberOfEight\tskj.txt";
            FileWorker worker = new FileWorker(filePath);
            var commands = GetCommands(worker);
            var dict = CreateTimeLine(commands);
            var console = new ConsoleWorker('*', '*', FindLargestCommand(commands));

            for (var i = 0; i < dict.Keys.Last(); i++)
            {
                console.ClearConsole();
                console.Draw(dict[i]);
                Thread.Sleep(System.TimeSpan.FromSeconds(1));
            }
        }

        public static List<Command> GetCommands(FileWorker worker)
        {
            var list = new List<Command>();
            foreach (var str in worker.FileContent)
            {
                if (str != "")
                    list.Add(new Command(str));
            }

            return list;
        }

        public static Dictionary<int, List<Command>> CreateTimeLine(List<Command> commands)
        {
            var dict = new Dictionary<int, List<Command>>();

            int minTime = 0;
            int maxTime = FindMaxTime(commands);

            for (var i = minTime; i <= maxTime; i++)
            {
                var listCommands = FindCommandsWithTime(i, commands);
                if (!dict.TryAdd(i, listCommands))
                {
                    dict[i].AddRange(listCommands);
                }
            }

            return dict;
        }

        public static int FindMaxTime(List<Command> commands)
        {
            int maxTime = 0;
            foreach (var command in commands)
            {
                if (command.End.Second > maxTime)
                    maxTime = command.End.Second;
            }

            return maxTime;
        }

        public static List<Command> FindCommandsWithTime(int time, List<Command> commands)
        {
            var list = new List<Command>();

            foreach (var command in commands)
            {
                if (command.Start.Second <= time && command.End.Second >= time)
                {
                    list.Add(command);
                }
            }

            return list;
        }

        public static int FindLargestCommand(List<Command> commands)
        {
            int max = 0;
            foreach (var command in commands)
            {
                if (command.Words.Length > max)
                    max = command.Words.Length;
            }

            return max;
        }
    }
}
