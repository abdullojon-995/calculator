public static class Calculator
{
    public static int EvaluateExpression(string expression)
    {
        Stack<int> numbers = new Stack<int>();
        Stack<char> operators = new Stack<char>();

        for (int i = 0; i < expression.Length; i++)
        {
            if (expression[i] == ' ')
                continue;

            if (Char.IsDigit(expression[i]))
            {
                int number = 0;

                while (i < expression.Length && Char.IsDigit(expression[i]))
                {
                    number = number * 10 + (expression[i] - '0');
                    i++;
                }

                numbers.Push(number);
                i--;
            }
            else if (expression[i] == '(')
            {
                operators.Push(expression[i]);
            }
            else if (expression[i] == ')')
            {
                while (operators.Count > 0 && operators.Peek() != '(')
                {
                    int temp = ApplyOperation(operators.Pop(), numbers.Pop(), numbers.Pop());
                    numbers.Push(temp);
                }

                operators.Pop();
            }
            else if (expression[i] == '+' || expression[i] == '-' || expression[i] == '*' || expression[i] == '/')
            {
                while (operators.Count > 0 && HasPrecedence(expression[i], operators.Peek()))
                {
                    int temp = ApplyOperation(operators.Pop(), numbers.Pop(), numbers.Pop());
                    numbers.Push(temp);
                }

                operators.Push(expression[i]);
            }
        }

        while (operators.Count > 0)
        {
            int temp = ApplyOperation(operators.Pop(), numbers.Pop(), numbers.Pop());
            numbers.Push(temp);
        }

        return numbers.Pop();
    }

    static int ApplyOperation(char op, int b, int a)
    {
        switch (op)
        {
            case '+':
                return a + b;
            case '-':
                return a - b;
            case '*':
                return a * b;
            case '/':
                if (b == 0)
                {
                    throw new DivideByZeroException("Cannot divide by zero");
                }
                return a / b;
            default:
                return 0;
        }
    }

    static bool HasPrecedence(char op1, char op2)
    {
        if (op2 == '(' || op2 == ')')
        {
            return false;
        }
        if ((op1 == '*' || op1 == '/') && (op2 == '+' || op2 == '-'))
        {
            return false;
        }
        return true;
    }
}
