using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BankAccountV2.Classes
{
    internal class AppStateHelper
    {
        public static void SaveState(List<BankAccount> allBankAccounts)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "CreatedAccountsDB.json");
            var json = JsonConvert.SerializeObject(allBankAccounts, Formatting.Indented, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });

            File.WriteAllText(path, json);
        }

        public static List<BankAccount>? ReadState()
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "CreatedAccountsDB.json");
                if (!File.Exists(path))
                {
                    return null;
                }
                var json = File.ReadAllText(path);
                var newList = JsonConvert.DeserializeObject<List<BankAccount>>(json, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                if (newList is not null && newList.Any())
                {
                    var max = newList.Select(x => int.Parse(x.Number)).Max();
                    BankAccount.AccountNumberSeed = max + 1;
                    return newList;
                }
            }
            catch (FileNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Using new Database.");
            }
            return null;
        }
    }
}
