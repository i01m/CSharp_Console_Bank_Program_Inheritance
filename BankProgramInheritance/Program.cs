using System;

namespace BankProgramInheritance
{
    public interface IAccount
    {
        void PayInFunds(decimal amount);
        bool WithdrawFunds(decimal amount);
        decimal GetBalance();
    }

        public class CustomerAccount : IAccount
    {
        protected decimal balance = 0; //protected = all child classes can access that variable

        public void PayInFunds(decimal amount)
        {
            balance = balance + amount;
        }

        public virtual bool WithdrawFunds(decimal amount) //virtual=this method will be overritten in child class (see BabyAccount method)
        {
            if (balance < amount)
            { return false; }
            balance = balance - amount;
            return true;
        }

        public decimal GetBalance()
        { return balance; }

    }

    public class BabyAccount : CustomerAccount, IAccount //it inherits all methods from CustomerAccount
    {
        
        public override bool WithdrawFunds(decimal amount) //it overrides=modifies method from his parent
        {
            if (amount > 10)
            { return false; }
            if (amount > balance)
            { return false; }
            balance = balance - amount;
            return true;
        }

        //public new bool WithdrawFunds(decimal amount) //another way to override method - create NEW one (no need for words 
                                                      //VIRTUAL(in parent) and OVERRIDE(in child method)    
           
        

    }
    class Program
    {
        const int MAX_CUST = 100;

        static void Main(string[] args)
        {
            IAccount[] accounts = new IAccount[MAX_CUST];

            accounts[0] = new CustomerAccount();
            accounts[0].PayInFunds(50);
            Console.WriteLine("customer 1 balance : $" + accounts[0].GetBalance());

            accounts[1] = new BabyAccount();
            accounts[1].PayInFunds(9);
            Console.WriteLine("customer 2(babyAccount) balance is : $" + accounts[1].GetBalance());

            Console.WriteLine();
            if (accounts[0].WithdrawFunds(40))
            { Console.WriteLine("customer1 : withdraw OK"); }
            else { Console.WriteLine("customer1 : specified amount is not available"); }

            if (accounts[1].WithdrawFunds(88)) //babyAccount could have enough funds, but cant withdraw more than $10 at a time
            { Console.WriteLine("customer2 : withdraw OK"); }
            else { Console.WriteLine("customer2 : withdraw amount reached the limit"); }

            Console.ReadKey();

        }
    }
}
