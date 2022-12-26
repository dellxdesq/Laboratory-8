namespace laba;

public class ConsoleWorker
{
    private int _windowWidth;
    private int _windowHeight;
    private char _horizontalSymbol;
    private char _verticalSymbol;

    public ConsoleWorker(char horizontalSymbol, char verticalSymbol,
        int largestCommandPhrase)
    {
        _horizontalSymbol = horizontalSymbol;
        _verticalSymbol = verticalSymbol;
        SetScreenSize(largestCommandPhrase);
    }

    private void SetScreenSize(int largestCommandPhrase)
    {
        _windowWidth = 50 + largestCommandPhrase;
        _windowHeight = 25;
    }

    public void Draw(List<Command> commandList)
    {
        Dictionary<Tuple<int, int>, Command> commandDict = ProcessCommands(commandList);

        PrintHorizontalBorder(true);
        for (int i = 0; i < _windowHeight; ++i)
        {
            Console.Write(_verticalSymbol);

            PrintContent(commandDict, i);

            Console.WriteLine(_verticalSymbol);
        }
        PrintHorizontalBorder(false);
    }

    private void PrintContent(Dictionary<Tuple<int, int>, Command> commandDict, int i)
    {
        for (int j = 0; j < _windowWidth - 2; ++j)
        {
            j += PrintStroke(commandDict, j, i);
            Console.Write(' ');
        }
    }

    private static int PrintStroke(Dictionary<Tuple<int, int>, Command> commandDict, int j, int i)
    {
        commandDict.TryGetValue(Tuple.Create(j, i), out var temp);

        if (temp is not null)
        {
            Console.ForegroundColor = GetColor(temp);
            Console.Write(temp.Words);
            Console.ResetColor();
            return temp.Words.Length;
        }

        return 0;
    }

    public void PrintHorizontalBorder(bool isTop)
    {
        for (int i = 0; i < _windowWidth; ++i)
        {
            Console.Write(_horizontalSymbol);
        }
        if (isTop)
            Console.WriteLine();
    }

    private Dictionary<Tuple<int, int>, Command> ProcessCommands(List<Command> commandList)
    {
        var dict = new Dictionary<Tuple<int, int>, Command>();
        try
        {
            foreach (var command in commandList)
            {
                Tuple<int, int> posXY = PosToCoordinatesTuple(command);
                dict.Add(posXY, command);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message} in Gui.ProcessCommands()");
        }

        return dict;
    }
    private int HowManyLinesTextTakes(string text)
    {
        return text.Length / _windowWidth;
    }

    private Tuple<int, int> PosToCoordinatesTuple(Command command)
    {
        var posXY = new Dictionary<string, Tuple<int, int>>()
        {
            {"Top",  Tuple.Create(_windowWidth / 2 - 1, 0) },
            {"Left", Tuple.Create(1, _windowHeight / 2 - 1) },
            {"Right", Tuple.Create(_windowWidth - command.Words.Length - 3, _windowHeight / 2 - 1) },
            {"Bottom", Tuple.Create(_windowWidth / 2 - 1, _windowHeight - 1)}
        };

        try
        {
            return posXY[command.Position];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message} in Gui.PosToCoordinatesTuple(), return is Tuple({0,0})");
            return Tuple.Create(0, 0);
        }
    }

    public static ConsoleColor GetColor(Command command)
    {
        return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), command.Color);
    }

    public void ClearConsole()
    {
        Console.Clear();
    }
}
