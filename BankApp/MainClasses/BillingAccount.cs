using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_Banku
{
    public class BillingAccount : Account
    {
        public BillingAccount(string firstName, string lastName, string pesel) : base(firstName, lastName, pesel)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
        }
        public void TakeCharche()
        { 
            if(Balance == 0)
            {
                Console.WriteLine($"Brak środków na konncie głównym {FirstName} {LastName}, Id : {Id} opłata za prowadzenie zostanie pobrana w następnym miesiącu.");
            }
            else
            {
                Balance -= 5;
                Console.WriteLine($"Pobrano opłate za prowadzenie konta {FirstName} {LastName}, Id : {Id} w wysokości 5 zł");
            }
        }
        public override string TypeName()
        {
            return "Rozliczeniowe";
        }
    }
}
