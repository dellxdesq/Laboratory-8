using System.Numerics;

namespace Lab8;

public class BankAccount
{
    private BigInteger _currentBalance;
    private bool _accountCreated;
    private BigInteger _lastOperationAmount;
    private SortedDictionary<DateTime, BigInteger> _timeBalanceHistory;

    public bool AccountCreated => _accountCreated;

    public BankAccount(List<Command> commandList)
    {
        _accountCreated = false;
        _timeBalanceHistory = new SortedDictionary<DateTime, BigInteger>();

        if (commandList.Count <= 0)
        {
            _currentBalance = 0;
            _lastOperationAmount = 0;
        }

        foreach (var command in commandList)
        {
            OperateBalance(DefineOperation(command), command.Amount);
            AddBalanceToBalanceHistory(command);
        }
    }

    private Func<BigInteger, bool> DefineOperation(Command command)
    {
        OperationType operationType = command.Type;

        switch (operationType)
        {
            case OperationType.Create:
                return CreateAccount;
            case OperationType.In:
                return Deposit;
            case OperationType.Out:
                return WithDraw;
            case OperationType.Revert:
                return Revert;
            case OperationType.Wrong:
                Console.WriteLine("Неправильная операция.");
                return ErrorOperation;
            default:
                Console.WriteLine($"Неизвестный тип операции: {command.Type}.");
                return ErrorOperation;
        }
    }

    private void OperateBalance(Func<BigInteger, bool> balanceOperation, BigInteger amount)
    {
        var tempBalance = _currentBalance;

        if (!IsAmountPositive(amount) || !balanceOperation(amount))
        {
            Console.WriteLine("Ошибка при работе с балансом.");
            return;
        }
        _lastOperationAmount = _currentBalance - tempBalance;
    }

    private bool CreateAccount(BigInteger amount)
    {

        _currentBalance = amount;
        _accountCreated = true;
        return true;
    }

    private bool Deposit(BigInteger amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Неверная сумма депозита. Должно быть больше 0");
            return false;
        }

        _currentBalance += amount;
        return true;
    }

    private bool WithDraw(BigInteger amount)
    {
        if (amount > _currentBalance)
        {
            Console.WriteLine($"Недостаточно баланса({_currentBalance}) для снятия {amount}." + $" депозит {amount - _currentBalance} для снятия.");
            return false;
        }

        _currentBalance -= amount;
        return true;
    }

    private bool IsAmountPositive(BigInteger amount)
    {
        return amount >= 0;
    }

    private bool ErrorOperation(BigInteger amount)
    {
        return false;
    }

    private bool Revert(BigInteger amount)
    {
        if (_lastOperationAmount > 0)
        {
            return WithDraw(_lastOperationAmount);
        }
        else if (_lastOperationAmount < 0)
        {
            return Deposit(BigInteger.Abs(_lastOperationAmount));
        }

        return ErrorOperation(_lastOperationAmount);
    }

    private void AddBalanceToBalanceHistory(Command command)
    {
        _timeBalanceHistory[command.DateTime] = _currentBalance;
    }

    public BigInteger CheckForErrorsFindBalanceAtTime(DateTime time)
    {
        DateTime tempTime;
        BigInteger tempBalance = new BigInteger(-1);

        tempBalance = FindBalanceAtTime(time, tempBalance);

        return tempBalance;
    }

    private BigInteger FindBalanceAtTime(DateTime time, BigInteger tempBalance)
    {
        if (time > _timeBalanceHistory.Last().Key)
            return _currentBalance;

        foreach (var (timeHistory, balance) in _timeBalanceHistory)
        {
            if (timeHistory == time)
            {
                tempBalance = balance;
                break;
            }
            else if (timeHistory < time)
            {
                tempBalance = balance;
            }
        }

        return tempBalance;
    }
}
