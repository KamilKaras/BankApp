using System;
using System.Text;

namespace Aplikacja_Banku
{
    public abstract class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public long Balance { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        protected static int id;
        public Account(string firstName, string lastName, string pesel)
        {
            Id = ++id;
            AccountNumber = GenerateAccountNumber(Id);
            Balance = 0;
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
        }
        public abstract string TypeName();
        public string GetFullName()
        {
            return $"{FirstName} {LastName}, Nr pesel: {Pesel}";
        }
        public string GetBallance()
        {
            return Balance.ToString();
        }
        public void ChangeBalance(int valueToChange)
        {
            Balance += valueToChange;
        }
        private string GenerateAccountNumber(int idNumber)
        {
            Random rnd = new Random();
            int idLength = idNumber.ToString().Length;
            var accountNumber = new StringBuilder();
            accountNumber.Append(idNumber);
            for (int i = 0; i < 26- idLength; i++)
            {
                int rndValue = rnd.Next(0, 9);
                accountNumber.Append(rndValue);
            }
            return accountNumber.ToString().Insert(2," ").Insert(7," ").Insert(12, " ").Insert(17, " ").Insert(22, " ").Insert(27, " ");
        }
    }
}
