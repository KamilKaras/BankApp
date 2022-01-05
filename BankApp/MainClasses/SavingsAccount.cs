using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_Banku
{
    public class SavingsAccount : Account
    {
        public SavingsAccount(string firstName, string lastName, string pesel) :base (firstName,lastName,pesel)
        {
            Balance = 100;
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
        }
        public void SetInterest(int interest)
        {
            ChangeBalance(interest);
        }
        public override string TypeName()
        {
            return "Oszczędnościowe";
        }
    }
}
