using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_Banku
{
    public abstract class AccountsManager
    {
        protected static List<Account> AccountsList = new List<Account>();
 
        protected static void CreateBillingAccount(List<string> list)
        {
            var billingAccount = new BillingAccount(list[0], list[1], list[2]);
            AccountsList.Add(billingAccount);
        }
        protected static void CreateSavingAccount(List<string> list)
        {
            var savingAccount = new SavingsAccount(list[0], list[1], list[2]);
            AccountsList.Add(savingAccount);
        }
        protected static void GetAllAccounts()
        {
            Console.Clear();
            if (AccountsList.Count == 0)
            {
                Console.WriteLine("Brak zdefiniowanych kont w bazie!!");
            }
            else
            {
                AccountsList.ForEach(a => Printer.Print(a));
            }
        }
        protected static void GetAccountsFor()
        {
            var pesel = GetUserPesel();
            var clientAccounts = GetClientAccounts(pesel);
            if (clientAccounts.Count == 0)
            {
                Console.WriteLine("Podany klient nie znajduje się w bazie banku");
            }
            else
            {
                clientAccounts.ForEach(s => Printer.Print(s));
                Console.WriteLine("Wciśnij dowolny przycisk aby kontynuować");
            }
        }
        protected static void GetAccount()
        {
            Console.WriteLine("Podaj numer klienta: "); string accountNumber = Console.ReadLine();
            var selected = AccountsList.Where(w => w.AccountNumber == accountNumber).ToList();
            Console.Clear();
            if (selected.Count == 0)
            {
                Console.WriteLine("Podany klient nie znajduje się w bazie banku");
            }
            else
            {
                selected.ForEach(s => Printer.Print(s));
            }
        }
        protected static void ListOfCustomers()
        {
            Console.Clear();
            if (AccountsList.Count == 0)
            {
                Console.WriteLine("Brak zdefiniowanych klientów w bazie!!");
            }
            else
            {
                var listOfCustomersNames = AccountsList.Select(l => l.GetFullName()).ToList();
                listOfCustomersNames.ForEach(l => Console.WriteLine($"Klient: {l}"));
            }
        }
        protected static void CloseMonth()
        {
            var pesel = GetUserPesel();
            var clientAccounts = GetClientAccounts(pesel);
            if (clientAccounts.Count == 0)
            {
                Console.WriteLine("Podany klient nie znajduje się w bazie banku");
            }
            else
            {
                Console.WriteLine("Podaj kwotę jaką chcesz przelać na konto oszczędnościowe");
                var interest = int.Parse(Console.ReadLine());
                var billingAccountSelected = clientAccounts.Where(a => a.TypeName() == "Rozliczeniowe").ToList();
                var savingAccountSelected = clientAccounts.Where(a => a.TypeName() == "Oszczędnościowe").ToList();
                var billingAccountList = billingAccountSelected.ConvertAll(a => (BillingAccount)a);
                billingAccountList.ForEach(a => a.TakeCharche());
                var savingAccountList = savingAccountSelected.ConvertAll(a => (SavingsAccount)a);
                if (billingAccountList.Count >= 1 && savingAccountList.Count >= 1)
                {
                    for (int i = 0; i < savingAccountList.Count; i++)
                    {
                        var cielntBillingAccountToInterest = billingAccountList.Where(a => int.Parse(a.GetBallance()) >= interest).ToList();
                        if (cielntBillingAccountToInterest.Count != 0)
                        {
                            for (int j = 0; j < cielntBillingAccountToInterest.Count; j++)
                            {
                                savingAccountList[i].SetInterest(interest);
                                cielntBillingAccountToInterest[j].ChangeBalance(-interest);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Brak wystarczających środków na kontach rozliczeniowych, aby wykonać transfer na konto oszczędnościowe");
                            break;
                        }

                    }
                    Console.WriteLine("Transfer zakończony");
                }
                else
                {
                    Console.WriteLine("Brak możliwośći przelania pieniędzy na konta oszczędnościowe");
                }
            }
        }
        protected static void Deposit()
        {
            var pesel = GetUserPesel();
            var selectedBilling = GetClientAccounts(pesel);
            if (selectedBilling.Count == 0)
            {
                Console.WriteLine("Podany użytkownik nie posiada kont w tym banku");
            }
            else
            {
                Console.WriteLine("Podaj kwotę do wpłaty: ");
                var valueToDeposit = int.Parse(Console.ReadLine());
                if (valueToDeposit<=0)
                {
                    Console.WriteLine("Wprowadziłeś błędne dane spróbuj jeszcze raz");
                }
                else
                {
                    Console.WriteLine("Podaj Id konta na które mamy wpłacić pieniądze");
                    selectedBilling.ForEach(a => Console.WriteLine($"Konto {a.TypeName()}, Id {a.Id}"));
                    var decision = Console.ReadLine();
                    var accountToAddMoney = selectedBilling.Where(a => a.Id == int.Parse(decision)).ToList();
                    accountToAddMoney.ForEach(a => a.ChangeBalance(valueToDeposit));
                    Console.WriteLine("Dziękujemy za wpłatę, wcisnij dowolny przycisk aby kontynuować");
                }
            }
        }
        protected static void Withdraw()
        {
            var pesel = GetUserPesel();
            var selectedBilling = GetClientAccounts(pesel);
            if (selectedBilling.Count == 0)
            {
                Console.WriteLine("Podany użytkownik nie posiada konta rozliczeniowego w tym banku");
            }
            else
            {
                Console.WriteLine("Podaj kwotę do wypłaty: ");
                var valueToWithdraw = int.Parse(Console.ReadLine());
                if (valueToWithdraw <= 0)
                {
                    Console.WriteLine("Wprowadziłeś błędne dane spróbuj jeszcze raz");
                }
                else
                {
                    var accountPosibleToWithdraw = selectedBilling.Where(a => a.Balance >= valueToWithdraw).ToList();                   
                    if(accountPosibleToWithdraw.Count == 0)
                    {
                        Console.WriteLine("Żadne z twoich kont nie posiada dostępnych środków większych niż wypłata, sprawdź stany kont");
                    }
                    else
                    {
                        Console.WriteLine("Konta z których można wypłacić określoną wyżej kwotę");
                        accountPosibleToWithdraw.ForEach(a => Console.WriteLine($"Konto {a.TypeName()}, Id {a.Id}"));
                        Console.WriteLine("Podaj Id konta z którego zrobimy wypłate");
                        var decision = Console.ReadLine();
                        var accountToWithdraw = accountPosibleToWithdraw.Where(a => a.Id == int.Parse(decision)).ToList();
                        accountPosibleToWithdraw.ForEach(a => a.ChangeBalance(-valueToWithdraw));
                        Console.WriteLine("Dziękujemy za wypłatę, wcisnij dowolny przycisk aby kontynuować");
                    }
                }
            }
        }
        protected static List<Account> GetClientAccounts(string pesel)
        {
            var selected = AccountsList.Where(a => a.Pesel == pesel).ToList();
            return selected;
        }
        protected static string GetUserPesel()
        {
            Console.WriteLine("Podaj swój pesel"); var pesel = Console.ReadLine();
            Console.Clear();
            return pesel;
        }
        
    }
}
