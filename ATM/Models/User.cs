using System;

namespace AtmSimulation.Models {
  public class User {
    public string userNameValue;
    public string userPasswordValue;
    private decimal userBalanceValue;

    public User(string nameValue, string passwordValue) {
      userNameValue = nameValue;
      userPasswordValue = passwordValue;
      userBalanceValue = 0;
    }

    public User(string nameValue, string passwordValue, decimal initialBalanceValue) {
      userNameValue = nameValue;
      userPasswordValue = passwordValue;
      userBalanceValue = initialBalanceValue;
    }

    public decimal GetBalanceValue() {
      return userBalanceValue;
    }

    public bool CheckPasswordValue(string passwordValue) {
      return userPasswordValue == passwordValue;
    }

    public bool AddMoneyValue(decimal amountValue) {
      if (amountValue <= 0) {
        Console.WriteLine("Сумма должна быть положительной");
        return false;
      }

      userBalanceValue = userBalanceValue + amountValue;
      Console.WriteLine($"Внесено {amountValue:C}. Новый баланс: {userBalanceValue:C}");
      return true;
    }

    public bool RemoveMoneyValue(decimal amountValue) {
      if (amountValue <= 0) {
        Console.WriteLine("Сумма должна быть положительной");
        return false;
      }

      if (amountValue > userBalanceValue) {
        Console.WriteLine($"Недостаточно средств. Доступно: {userBalanceValue:C}");
        return false;
      }

      userBalanceValue = userBalanceValue - amountValue;
      Console.WriteLine($"Снято {amountValue:C}. Новый баланс: {userBalanceValue:C}");
      return true;
    }

    public bool CanTransferValue(decimal amountValue) {
      return amountValue > 0 && amountValue <= userBalanceValue;
    }
  }
}