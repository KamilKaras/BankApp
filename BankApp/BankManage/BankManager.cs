using System;
using System.Collections.Generic;

namespace Aplikacja_Banku
{
    public class BankManager : AccountsManager
    {
        public static void Run()
        { 
            bool leaveAplication = true;
            do
            {
                PrintMainMenu();
                string decision = Console.ReadLine();
                switch (decision)
                {
                    case "1":
                        GetAccountsFor();
                        Console.ReadKey();
                        break;
                    case "2":
                        AddBillingAccount();
                        break;
                    case "3":
                        AddSavingsAccount();
                        break;
                    case "4":
                        Deposit();
                        Console.ReadKey();
                        break;
                    case "5":
                        Withdraw();
                        Console.ReadKey();
                        break;
                    case "6":
                        ListOfCustomers();
                        Console.ReadKey();
                        break;
                    case "7":
                        ListOfAccounts();
                        Console.ReadKey();
                        break;
                    case "8":
                        CloseMonth();
                        Console.ReadKey();
                        break;
                    case "0":
                        leaveAplication = false;
                        break;
                    default:
                        Console.WriteLine("Podałeś zły numer akcji, wcisnij dowolny przycisk");
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
            } while (leaveAplication);
        }
        private static void PrintMainMenu()
        {
            Console.WriteLine("Wybierz akcję:");
            Console.WriteLine("\t"+ "1 - Lista kont klienta");
            Console.WriteLine("\t"+ "2 - Dodaj konto rozliczeniowe");
            Console.WriteLine("\t"+ "3 - Dodaj konto oszczędnościowe");
            Console.WriteLine("\t"+ "4 - Wpłać pieniądze na konto");
            Console.WriteLine("\t"+ "5 - Wypłać pieniądze z konta");
            Console.WriteLine("\t"+ "6 - Lista klientów");
            Console.WriteLine("\t"+ "7 - Wszystkie konta");
            Console.WriteLine("\t"+ "8 - Zakończ miesiąc");
            Console.WriteLine("\t" + "0 - Zakończ");
        }
        private static void ListOfAccounts()
        {
            GetAllAccounts();
        }
        private static List<string> ReadCustomerData()
        {
            List<string> custoremData = new List<string>();
            Console.WriteLine("Wprowadź swoje imie: ");
            string name = Console.ReadLine();
            custoremData.Add(name);
            Console.WriteLine("Wprowadź swoje nazwisko: ");
            string lastName = Console.ReadLine();
            custoremData.Add(lastName);
            Console.WriteLine("Wprowadź swój pesel: ");
            string pesel = (Console.ReadLine());
            custoremData.Add(pesel);
            return custoremData;
        }
        private static void AddBillingAccount()
        {
            var customerBillingData = ReadCustomerData();
            CreateBillingAccount(customerBillingData);
        }
        private static void AddSavingsAccount()
        {
            var customerSavingData = ReadCustomerData();
            CreateSavingAccount(customerSavingData);
        }
    }
}
