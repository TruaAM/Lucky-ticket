using System;

namespace Lucky_ticket
{
    abstract class TicketService
    {
        protected TicketService nextService;

        public void SetNext(TicketService nextService)
        {
            this.nextService = nextService;
        }

        public abstract void CheckNumber(string number);
    }

    class LengthService : TicketService
    {
        public override void CheckNumber(string number)
        {           
            if (number.Length < 3 || number.Length > 8)
            {
                Console.WriteLine("Unexceptable number! Its length shouldn't be less than 4 or more than 8!");
            }
            else
            {
                if (nextService != null)
                {
                    nextService.CheckNumber(number);
                }
            }
        }
    }

    class DigitService : TicketService
    {
        public override void CheckNumber(string number)
        {
            if (!IsDigitsOnly(number))
            {
                Console.WriteLine("Not all elemnts in number are digits!!!");
            }
            else
            {
                if (nextService != null)
                {
                    nextService.CheckNumber(number);
                }
            }
        }

        private bool IsDigitsOnly(string number)
        {
            foreach (char elem in number)
            {
                if (elem < '0' || elem > '9')
                {
                    return false;
                }
            }
            return true;
        }
    }

    class OddEvenService : TicketService
    {
        public override void CheckNumber(string number)
        {
            if (number.Length % 2 != 0)
            {
                number = "0" + number;
                Console.WriteLine("This is odd number. Adding 0 at start: {0}", number);
            }
            if (nextService != null)
            {
                nextService.CheckNumber(number);
            }
        }
    }

    class LuckyService : TicketService
    {
        public override void CheckNumber(string number)
        {
            string first = number.Substring(0, (number.Length / 2));
            string last = number.Substring((number.Length / 2), (number.Length / 2));

            Console.WriteLine("Is sum of {0} equal to sum of {1}?", first, last);

            if(GetSum(first) == GetSum(last))
            {
                Console.WriteLine("Congrats!!! It is a lucky ticket!");
            }
            else
            {
                Console.WriteLine("Sorry! You lost! Try next time!");
            }

            if (nextService != null)
            {
                nextService.CheckNumber(number);
            }
        }

        private int GetSum(string number)
        {
            int sum = 0;
            foreach (char elem in number)
            {
                sum += Convert.ToInt32(elem);
            }
            return sum;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TicketService t0 = new LengthService();
            TicketService t1 = new DigitService();
            TicketService t2 = new OddEvenService();
            TicketService t3 = new LuckyService();
            t0.SetNext(t1);
            t1.SetNext(t2);
            t2.SetNext(t3);

            string input;
            while (true)
            {
                Console.WriteLine("\nType number to find your lucky ticket. If you want to exit type 'q':");
                input = Console.ReadLine();
                if (input == "q") break;

                t0.CheckNumber(input);
            }
        }
    }
}
