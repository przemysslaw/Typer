using System;

namespace SDK
{
    public class Require
    {
        public static void NotNull(object parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void NotNullNotEmpty(string parameter, string parameterName)
        {
            if (String.IsNullOrEmpty(parameter) || String.IsNullOrWhiteSpace(parameter))
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void GreaterEqual<TExceptionToThrow>(DateTime leftOperand, DateTime rightOperand) where TExceptionToThrow : Exception, new()
        {
            if (!(leftOperand >= rightOperand))
            {
                throw new TExceptionToThrow();
            }
        }

        public static void Greater<TExceptionToThrow>(DateTime leftOperand, DateTime rightOperand) where TExceptionToThrow : Exception, new()
        {
            if (!(leftOperand > rightOperand))
            {
                throw new TExceptionToThrow();
            }
        }

        public static void GreaterEqual<TExceptionToThrow>(int leftOperand, int rightOperand) where TExceptionToThrow : Exception, new()
        {
            if (!(leftOperand >= rightOperand))
            {
                throw new TExceptionToThrow();
            }
        }
    }
}
