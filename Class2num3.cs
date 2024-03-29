﻿using System.Data;
using System.Numerics;

namespace Lab8;

public enum OperationType
{
    Create,
    In,
    Out,
    Revert,
    Wrong
}

public class Command
{
    private DateTime _dateTime;
    private BigInteger _amount;
    private OperationType _type;
    private bool IsGood;

    public DateTime DateTime => _dateTime;
    public BigInteger Amount => _amount;
    public OperationType Type => _type;

    public Command(string[] command)
    {
        switch (command.Length)
        {
            case 1:
                IsGood = SetInitialAmount(command[0]) &&
                         SetOperationType("Создать");
                break;
            case 3:
                IsGood = SetDateTime(command[0], command[1]) &&
                         SetOperationType(command[2]);
                break;
            case 4:
                IsGood = SetDateTime(command[0], command[1]) &&
                         SetAmount(command[2]) &&
                         SetOperationType(command[3]);
                break;
            default:
                Console.WriteLine("Неверная длина команды.");
                break;
        }
    }

    private bool SetInitialAmount(string amount)
    {
        BigInteger integerAmount;
        if (BigInteger.TryParse(amount, out integerAmount) && integerAmount >= 0)
        {
            _amount = integerAmount;
            return true;
        }

        Console.WriteLine("Первая строка файла должна быть положительным целым числом.");
        return false;
    }

    private bool SetOperationType(string type)
    {
        type = UpFirstLetter(type);

        if (!Enum.TryParse(type, out _type))
        {
            _type = OperationType.Wrong;
            Console.WriteLine($"{type} Тип операции не существует" + $"Указан неверный тип операции");
            return false;
        }

        return true;
    }

    private static string UpFirstLetter(string type)
    {
        return type[0].ToString().ToUpper() + type.Substring(1);
    }

    private bool SetAmount(string amount)
    {
        BigInteger bigIntegerAmount;
        if (!BigInteger.TryParse(amount, out bigIntegerAmount) || bigIntegerAmount <= 0)
        {
            Console.WriteLine($"Баланс({amount}) должен быть больше 0.");
            return false;
        }

        _amount = bigIntegerAmount;
        return true;
    }

    private bool SetDateTime(string date, string time)
    {
        string dateTime = date + " " + time;
        try
        {
            _dateTime = DateTime.ParseExact(dateTime, "yyyy-MM-dd hh:mm",
                System.Globalization.CultureInfo.InvariantCulture);
        }
        catch (Exception ex)
        {
            _dateTime = DateTime.MinValue;
            Console.WriteLine($"Дата({date}) или время({time}) неверны. " + $"_dateTime {DateTime.MinValue}");
            return false;
        }

        return true;
    }
}
