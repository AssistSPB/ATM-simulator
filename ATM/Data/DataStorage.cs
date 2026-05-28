using System;
using System.Collections.Generic;
using System.IO;
using AtmSimulation.Models;

namespace AtmSimulation.Data {
  public class DataStorage {
    private const string saveFileValue = "atm_data.txt";

    public void SaveUsersValue(List<User> usersListValue) {
      try {
        using (StreamWriter writerValue = new StreamWriter(saveFileValue)) {
          foreach (var singleUserValue in usersListValue) {
            string lineValue = $"{singleUserValue.userNameValue}:{singleUserValue.userPasswordValue}:{singleUserValue.GetBalanceValue()}";
            writerValue.WriteLine(lineValue);
          }
        }
        Console.WriteLine("Данные сохранены");
      } catch (Exception errorValue) {
        Console.WriteLine($"Ошибка при сохранении: {errorValue.Message}");
      }
    }

    public List<User> LoadUsersValue() {
      var userListValue = new List<User>();

      if (!File.Exists(saveFileValue)) {
        Console.WriteLine("Файл с данными не найден, создаётся новый");
        return userListValue;
      }

      try {
        using (StreamReader readerValue = new StreamReader(saveFileValue)) {
          string lineValue;
          while ((lineValue = readerValue.ReadLine()) != null) {
            string[] partsValue = lineValue.Split(':');
            if (partsValue.Length == 3) {
              string userNameValue = partsValue[0];
              string userPasswordValue = partsValue[1];
              decimal userBalanceValue = decimal.Parse(partsValue[2]);
              User restoredUserValue = new User(userNameValue, userPasswordValue, userBalanceValue);
              userListValue.Add(restoredUserValue);
            }
          }
        }
      } catch (Exception errorValue) {
        Console.WriteLine($"Ошибка при загрузке: {errorValue.Message}");
      }

      return userListValue;
    }
  }
}