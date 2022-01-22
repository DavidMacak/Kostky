using System;

namespace Kostky
{
    public static class GetInput
    {
        public static string StringType()
        {
            string output;
            bool success;

            do
            {
                output = Console.ReadLine();

                if(String.IsNullOrWhiteSpace(output) && String.IsNullOrEmpty(output))
                {
                    success = false;
                }
                else
                {
                    success = true;
                }

            } while (!success);

            return output;
        }

        public static int IntType()
        {
            int output;
            bool success;

            do
            {
                success = Int32.TryParse(Console.ReadLine(), out output);

            } while (!success);

            return output;
        }

        public static int IntType(int minNum)
        {
            int output;
            bool success;

            do
            {
                success = Int32.TryParse(Console.ReadLine(), out output);

                if(output >= minNum)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }

            } while (!success);

            return output;
        }

        public static bool IsEmpty()
        {
            string input;
            bool output;
                
            input = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(input) && String.IsNullOrEmpty(input))
            {
                output = true;
            }
            else
            {
                output = false;
            }


            return output;

        } 
    }
}
