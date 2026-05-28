using System;
using AtmSimulation.Services;

namespace AtmSimulation {
  class Program {
    static void Main() {
      Console.Title = "ATM Simulation";
      Atm atmMachineValue = Atm.GetInstanceValue();
      bool programRunningValue = true;

      while (programRunningValue) {
        if (!atmMachineValue.IsUserLoggedInValue()) {
          Console.WriteLine($"\n" +
                            $"БАNКОМАТ\n" +
                            $"1. Регистрация\n" +
                            $"2. Вход\n" +
                            $"3. Список пользователей\n" +
                            $"4. Выход\n" +
                            $"Выберите пункт: ");

          string userChoiceValue = Console.ReadLine();

          switch (userChoiceValue) {
            case "1":
              Console.Write("Введите имя пользователя: ");
              string newUserNameValue = Console.ReadLine();
              Console.Write("Введите пароль: ");
              string newUserPasswordValue = Console.ReadLine();
              atmMachineValue.RegisterUserValue(newUserNameValue, newUserPasswordValue);
              break;
            case "2":
              Console.Write("Введите имя пользователя: ");
              string loginUserNameValue = Console.ReadLine();
              Console.Write("Введите пароль: ");
              string loginPasswordValue = Console.ReadLine();
              atmMachineValue.LoginValue(loginUserNameValue, loginPasswordValue);
              break;
            case "3":
              atmMachineValue.ListAllUsersValue();
              break;
            case "4":
              programRunningValue = false;
              Console.WriteLine("До свидания!");
              break;
            default:
              Console.WriteLine("Неверный выбор");
              break;
          }
        } else {
          Console.WriteLine($" \n БАNКОМАТ - Пользователь: {atmMachineValue.GetCurrentUserNameValue()}");
          Console.WriteLine($"1. Внести деньги" +
                            $"2. Снять деньги" +
                            $"3. Перевести деньги" +
                            $"4. Проверить баланс" +
                            $"5. Выйти из аккаунта" +
                            $"6. Завершить работу" +
                            $"Выберите пункт: ");

          string userChoiceValue = Console.ReadLine();

          switch (userChoiceValue) {
            case "1":
              Console.Write("Введите сумму: ");
              if (decimal.TryParse(Console.ReadLine(), out decimal depositAmountValue))
                atmMachineValue.DepositValue(depositAmountValue);
              else
                Console.WriteLine("Неверная сумма");
              break;
            case "2":
              Console.Write("Введите сумму: ");
              if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmountValue))
                atmMachineValue.WithdrawValue(withdrawAmountValue);
              else
                Console.WriteLine("Неверная сумма");
              break;
            case "3":
              Console.Write("Введите имя получателя: ");
              string targetUserNameValue = Console.ReadLine();
              Console.Write("Введите сумму: ");
              if (decimal.TryParse(Console.ReadLine(), out decimal transferAmountValue))
                atmMachineValue.TransferValue(targetUserNameValue, transferAmountValue);
              else
                Console.WriteLine("Неверная сумма");
              break;
            case "4":
              atmMachineValue.ShowBalanceValue();
              break;
            case "5":
              atmMachineValue.LogoutValue();
              break;
            case "6":
              atmMachineValue.LogoutValue();
              programRunningValue = false;
              Console.WriteLine("До свидания!");
              break;
            default:
              Console.WriteLine("Неверный выбор");
              break;
          }
        }

        if (programRunningValue) {
          Console.WriteLine($" \n Нажмите любую клавишу для продолжения...");
          Console.ReadKey();
        }
      }
    }
  }
}