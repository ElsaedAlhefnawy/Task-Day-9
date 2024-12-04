using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Day7
{
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string Name = "Unnamed Account", double Balance = 0.0)
        {
            this.Name = Name;
            this.Balance = Balance;
        }
        public static double operator +(Account Account1 , Account Account2)
        {
            return Account1.Balance + Account2.Balance;
        }


        public virtual bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
       
    }

    public class CheckingAccount : Account
    {
        public const double Fee = 1.50;
        public CheckingAccount (string Name = "UnNamed Accoun" , double Balance = 0.0 ) : base(Name , Balance)
        {
            
        }
        public override bool Withdraw(double amount)
        {
            if (base.Withdraw(amount-Fee))
            {
                Console.WriteLine($"Every withdrawal transaction will be assessed 1.50$ :: {Withdraw(amount)} Withdrawed");
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"Cheacking account Info :: Name : {Name} , Balance : {Balance}";
        }
    }
    public class SavingAccount : Account
    {
        public double Rate { get; set; }
        public SavingAccount(string Name = "NuNamed Account" , double Balance = 0.0 , double Rate = 0.02) :base (Name,Balance)
        {
            this.Rate = Rate;
        }
        public override bool Deposit(double amount)
        {
            base.Deposit(amount);
            amount += (amount * Rate);
            return true;
        }
        public override string ToString()
        {
            return $"Saving Account Info :: Name : {Name} , Balance : {Balance} , Rate : {Rate}";
        }
    }
    public class TrustAccount : SavingAccount
    {
        public TrustAccount(string Name = "UnNamed Account" , double Balance = 0.0 , double Rate = 0.02) : base(Name,Balance,Rate)
        {

        }
        public int Bonus = 50;
        public int Count_Withdraws = 0;
        public const int NumWithdraws_Year = 3;
        public const double InteristRate = 0.2;

        public override bool Deposit(double amount)
        {
            if (amount >= 5000)
            {
                Console.WriteLine($"{Bonus:c} Bonus added to withdraws greater than 5000$");
                base.Deposit(amount+amount);
            }
            else
            {
                base.Deposit(amount);
            }
                return true;
        }
        public override bool Withdraw(double amount)
        {
            if (Count_Withdraws >= NumWithdraws_Year)
            {
                Console.WriteLine("You can't withdraw more than 3 time per year :: limit has reached");
                return false;
            }
            else if (amount > Balance*InteristRate)
            {
                Console.WriteLine($"Sorry : withdraw must be less than 20% of the account balance ");
                return false;
            }
            else if (base.Withdraw(amount))
            {
                Count_Withdraws++;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"Trust Account Info :: Name : {Name} , Balance : {Balance} , Rate : {InteristRate * 100 :c} , {NumWithdraws_Year-Count_Withdraws} withdraws left for this year";
        }

    }
     
}
