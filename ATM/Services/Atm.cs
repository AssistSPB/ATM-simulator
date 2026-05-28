using System;
using System.Collections.Generic;
using AtmSimulation.Models;
using AtmSimulation.Data;

namespace AtmSimulation.Services {
  public sealed class Atm {
    private static Atm instanceValue;
    private static readonly object lockObjectValue = new object();
    private Dictionary<string, User> allUsersValue;
    private User loggedUserValue;
    private DataStorage storageValue;

    private Atm() {
      storageValue = new DataStorage();
      allUsersValue = new Dictionary<string, User>();
      LoadAllDataValue();
    }

    public static Atm GetInstanceValue() {
      if (instanceValue == null) {
        lock (lockObjectValue) {
          if (instanceValue == null) {
            instanceValue = new Atm();
          }
        }
      }
      return instanceValue;
    }

    private void LoadAllDataValue() {
      var loadedUsersValue = storageValue.LoadUsersValue();
      if (loadedUsersValue != null) {
        allUsersValue.Clear();
        foreach (var singleUserValue in loadedUsersValue) {
          allUsersValue[singleUserValue.userNameValue] = singleUserValue;
        }
        Console.WriteLine($"Загружено {allUsersValue.Count} богатых миллионера!");
      }
    }

    private void SaveAllDataValue() {
      var userListValue = new List<User>();
      foreach (var singleUserValue in allUsersValue.Values) {
        userListValue.Add(singleUserValue);
      }
      storageValue.SaveUsersValue(userListValue);
    }

    public bool IsUserLoggedInValue() {
      return loggedUserValue != null;
    }

    public bool RegisterUserValue(string userNameValue, string passwordValue) {
      if (string.IsNullOrWhiteSpace(userNameValue)) {
        Console.WriteLine("Имя пользователя не может быть пустым");
        return false;
      }

      if (string.IsNullOrWhiteSpace(passwordValue)) {
        Console.WriteLine("Пароль не может быть пустым");
        return false;
      }

      if (allUsersValue.ContainsKey(userNameValue)) {
        Console.WriteLine($"Пользователь '{userNameValue}' уже существует");
        return false;
      }

      User newUserValue = new User(userNameValue, passwordValue);
      allUsersValue[userNameValue] = newUserValue;
      SaveAllDataValue();
      Console.WriteLine($"Пользователь '{userNameValue}' успешно зарегистрирован");
      return true;
    }

    public bool LoginValue(string userNameValue, string passwordValue) {
      if (!allUsersValue.ContainsKey(userNameValue)) {
        Console.WriteLine($"Пользователь '{userNameValue}' не найден");
        return false;
      }

      User userValue = allUsersValue[userNameValue];
      if (!userValue.CheckPasswordValue(passwordValue)) {
        Console.WriteLine("Неверный пароль<#");
        return false;
      }

      loggedUserValue = userValue;
      Console.WriteLine($"Welcom, {userNameValue}!");
      return true;
    }

    public void LogoutValue() {
      if (loggedUserValue != null) {
        Console.WriteLine($"До свидания, {loggedUserValue.userNameValue}!");
        loggedUserValue = null;
      }
    }

    public bool DepositValue(decimal amountValue) {
      if (loggedUserValue == null) {
        Console.WriteLine("Пользователь не авторизован");
        return false;
      }

      bool resultValue = loggedUserValue.AddMoneyValue(amountValue);
      if (resultValue) {
        SaveAllDataValue();
      }
      return resultValue;
    }

    public bool WithdrawValue(decimal amountValue) {
      if (loggedUserValue == null) {
        Console.WriteLine("Пользователь не авторизован");
        return false;
      }

      bool resultValue = loggedUserValue.RemoveMoneyValue(amountValue);
      if (resultValue) {
        SaveAllDataValue();
      }
      return resultValue;
    }

    public bool TransferValue(string targetUserNameValue, decimal amountValue) {
      if (loggedUserValue == null) {
        Console.WriteLine("Пользователь не авторизован");
        return false;
      }

      if (!allUsersValue.ContainsKey(targetUserNameValue)) {
        Console.WriteLine($"Пользователь '{targetUserNameValue}' не найден");
        return false;
      }

      if (targetUserNameValue == loggedUserValue.userNameValue) {
        Console.WriteLine("Нельзя перевести деньги самому себе");
        return false;
      }

      if (!loggedUserValue.CanTransferValue(amountValue)) {
        Console.WriteLine($"Мани маловато. Доступно: {loggedUserValue.GetBalanceValue():C}");
        return false;
      }

      User targetUserValue = allUsersValue[targetUserNameValue];
      loggedUserValue.RemoveMoneyValue(amountValue);
      targetUserValue.AddMoneyValue(amountValue);
      SaveAllDataValue();
      Console.WriteLine($"Переведено {amountValue:C} пользователю {targetUserNameValue}");
      return true;
    }

    public void ShowBalanceValue() {
      if (loggedUserValue == null) {
        Console.WriteLine("Пользователь не авторизован");
        return;
      }

      Console.WriteLine($"Ваш баланс: {loggedUserValue.GetBalanceValue():C}");
    }

    public void ListAllUsersValue() {
      if (allUsersValue.Count == 0) {
        Console.WriteLine("Нет зарегистрированных пользователей");
        return;
      }

      Console.WriteLine(" \n Список зарегистрированных пользователей:");
      int userCounterValue = 0;
      foreach (var singlePairValue in allUsersValue) {
        ++userCounterValue;
        Console.WriteLine($"{userCounterValue}. {singlePairValue.Key}");
      }
      Console.WriteLine("");
    }

    public string GetCurrentUserNameValue() {
      return loggedUserValue?.userNameValue;
    }
  }
}