using System.Numerics;

namespace Lab8
{
    class Program2
    {
        static void Main()
        {
            string? filePath = "";
            FileReader? file = null;

            while (filePath == "")
            {
                InitiateFileReader(ref filePath, ref file);
            }

            CommandManager commandManager = new CommandManager(file.FileContent);
            BankAccount bankAccount = new BankAccount(commandManager.CommandList);

            string time = "";
            while (time != "q" && bankAccount.AccountCreated)
            {
                Console.WriteLine("Введите дату и время или q для выхода");
                time = Console.ReadLine();

                if (time == "q")
                    break;

                CheckBalanceAtTime(bankAccount, time);
            }

        }

        private static void InitiateFileReader(ref string? filePath, ref FileReader? file)
        {
            Console.WriteLine("Введите путь файла.");
            filePath = Console.ReadLine();

            try
            {
                file = new FileReader(filePath);
                filePath = file.FilePath;
            }
            catch (NullReferenceException ex)
            {
                filePath = new string("");
            }
        }

        private static void CheckBalanceAtTime(BankAccount bankAccount, string? time)
        {
            try
            {
                BigInteger balance = bankAccount.CheckForErrorsFindBalanceAtTime(DateTime.ParseExact(time, "yyyy-MM-dd hh:mm",
                    System.Globalization.CultureInfo.InvariantCulture));
                Console.WriteLine(balance.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Неверная дата.");
            }
        }
    }
}
