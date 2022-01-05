using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_Banku
{
    public class Printer : IPrinter
    {
        public static void Print(Account account)
        {
            Console.WriteLine($"Konto {account.TypeName()}");
            Console.WriteLine(account.GetFullName());
            Console.WriteLine($"ID:  {account.Id}");
            Console.WriteLine($"Dostępne środki: {account.GetBallance()} zł");
            Console.WriteLine($"Nr konta: {account.AccountNumber}");
            Console.WriteLine();
        }
    }
}

