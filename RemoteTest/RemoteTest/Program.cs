using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteTest
{
    class Program
    {
        static void Main()
        {
            bool Cal = true;

            while (Cal == true)
            {
                Console.WriteLine("Enter to calculate: ");
                string expression = Console.ReadLine();

                try
                {
                    double result = DoBracket(expression);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("Error occured while calculating. Please check the entered equation.");
                    }
                    else
                    {
                        Console.WriteLine("The answer is " + result);
                    }
                }
                catch
                {
                    Console.WriteLine("Error occured while calculating. Please check the entered equation.");
                }

                Console.WriteLine("---------------------------------------------------------------------------------------\n");

                Console.Write("Press 'y' and Enter to continue, or press any other key and Enter to close the system: ");
                if (Console.ReadLine() == "y")
                {
                    Console.WriteLine("\n");
                }
                else
                {
                    Cal = false;
                }
            }
        }

        public static double DoBracket(string equation)
        {
            List<string> OperationOrder = new List<string>(4) { "/", "*", "-", "+" };

            while (equation.LastIndexOf("(") > -1) //if found last open bracket
            {
                int lastopenbracket = equation.LastIndexOf("(");
                int closeforlast = equation.IndexOf(")", lastopenbracket);
                double total = Calculate(equation.Substring(lastopenbracket + 1, closeforlast - lastopenbracket - 1)); //remove bracket(get equation after open bracket and before close)

                equation = equation.Substring(0, lastopenbracket) + total.ToString() + equation.Substring(closeforlast + 1); //get equation before open bracket and after close bracket
            }
            return Calculate(equation);
        }

        public static double Calculate(string sum)
        {
            List<string> OperationOrder = new List<string>(4) { "/", "*", "-", "+" };
            List<string> formula = new List<string>();
            string math = "";
            for (int i = 0; i < sum.Length; i++)
            {
                string current = sum.Substring(i, 1); //get first char
                if (OperationOrder.IndexOf(current) > -1) //if is operator 
                {
                    if (math != "")
                    {
                        formula.Add(math); //add digit from else
                    }
                    formula.Add(current); //add operator to equation
                    math = "";
                }
                else
                {
                    math += current;
                }
            }
            formula.Add(math); //add to list

            double total = double.NaN;
            foreach (string op in OperationOrder)
            {
                while (formula.IndexOf(op) > -1) //found operator
                {
                    int operatorsign = formula.IndexOf(op);
                    double digitb4operator = Convert.ToDouble(formula[operatorsign - 1]);
                    double digitafter = Convert.ToDouble(formula[operatorsign + 1]);

                    switch (op)
                    {
                        case "/":
                            total = digitb4operator / digitafter;
                            break;
                        case "*":
                            total = digitb4operator * digitafter;
                            break;
                        case "-":
                            total = digitb4operator - digitafter;
                            break;
                        case "+":
                            total = digitb4operator + digitafter;
                            break;
                    }

                    formula[operatorsign] = total.ToString(); //replace operator with answer
                    formula.RemoveAt(operatorsign - 1); //remove digit before sign
                    formula.RemoveAt(operatorsign); // remove digit after
                }
            }
            return total;
        }
    }
}
