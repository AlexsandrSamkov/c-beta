using System;
using System.Runtime.CompilerServices;

namespace day01
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 5)
            {
                Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
                return;
            }
            if (args.Length > 5)
            {
                Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
                return;
            }
            double sum, rate, payment, resTerm, resPay;
            int term, selectMount ;
            Double.TryParse(args[0],out sum); 
            Double.TryParse(args[1],out rate); 
            Int32.TryParse(args[2], out term);
            Int32.TryParse(args[3], out selectMount);
            Double.TryParse(args[4], out payment);
            if (sum <= 0 | rate <= 0 | payment <= 0 | term <= 0  | selectMount <= 0) 
            {
               Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
               return;
            }
            resPay = overpaymentPay(sum, rate, term, selectMount, payment);
            resTerm = overpaymentTerm(sum, rate, term, selectMount, payment);
            if (resPay == resTerm)
            {
                Console.WriteLine("Уменьшение платежа равно уменьшению срока");
            }
            else if (resPay > resTerm)
            {
                Console.WriteLine("Уменьшение срока выгоднее уменьшения платежа");
            }
            else if (resTerm > resPay)
            {
                Console.WriteLine("Уменьшение платежа выгоднее уменьшения срока");
            }
            Console.WriteLine("Переплата при уменьшении платежа: " + Math.Round(resPay,2) + "р.");
            Console.WriteLine("Переплата при уменьшении срока: " + Math.Round(resTerm,2) + "р.");
           if (resPay < resTerm)
            {
                Console.WriteLine("Уменьшение платежа выгоднее уменьшения срока на "
                                  + Math.Round(resTerm - resPay,2) + "р.");
            }
            else if (resTerm < resPay)
            {
                Console.WriteLine("Уменьшение срока выгоднее уменьшения платежа на "
                                  + Math.Round(resPay - resTerm ,2) + "р.");
            }
        }

        static int ftDayYear()
        {
            DateTime thisDay = DateTime.Today;
            return (DateTime.IsLeapYear(thisDay.Year) ? 366 : 365);
        }
        static double overpaymentPay(double sum, double rate, int term, int selectMount, double payment)
        {
            double loanCost, perMouthRate, per, perAll;
            DateTime thisDay = DateTime.Today;
            int yearDay, mouthDay, maxMouth;
            perAll = 0;
            maxMouth = term;
            perMouthRate = rate / 12 / 100;
            loanCost = (sum * perMouthRate * Math.Pow(1 + perMouthRate, term)) / (Math.Pow(1 + perMouthRate, term) - 1);
            for (int j = 1; j <= maxMouth; j++)
            {
                
                if (selectMount == j - 1)
                {
                    sum -= payment;
                    term -= j - 1;
                    loanCost = (sum * perMouthRate * Math.Pow(1 + perMouthRate, term))
                               / (Math.Pow(1 + perMouthRate, term) - 1);
                }
                yearDay = ftDayYear();
                mouthDay =  DateTime.DaysInMonth(thisDay.Year, thisDay.Month);
                per = (sum * rate * mouthDay / 100.0 / yearDay);
                thisDay = thisDay.AddMonths(1);
                sum -= loanCost - per;
                perAll += per;
            }
            return perAll;
        }
        static double overpaymentTerm(double sum, double rate, int term, int selectMount, double payment)
        {
            double loanCost, perMouthRate, per, perAll;
            DateTime thisDay = DateTime.Today;
            int yearDay, mouthDay, maxMouth;
            perAll = 0;
            maxMouth = term;
            perMouthRate = rate / 12 / 100;
            loanCost = (sum * perMouthRate * Math.Pow(1 + perMouthRate, term)) / (Math.Pow(1 + perMouthRate, term) - 1);
            for (int j = 1; j <= maxMouth; j++)
            {
                
                if (selectMount == j - 1)
                {
                    sum -= payment;
                }
                yearDay = ftDayYear();
                mouthDay =  DateTime.DaysInMonth(thisDay.Year, thisDay.Month);
                per = (sum * rate * mouthDay / 100.0 / yearDay);
                thisDay = thisDay.AddMonths(1);
                if (sum - loanCost <= 0)
                {
                    return (perAll += per);
                }
                sum -= loanCost - per;
                perAll += per;
            }
            return perAll;
        }
    }
}