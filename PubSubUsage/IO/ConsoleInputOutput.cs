using System;

namespace PubSubUsage.IO
{
    public class ConsoleInputOutput : IInputOutput
    {
        public string Read()
        {
            return Console.ReadLine();
        }

        public void Write(string outputMessage)
        {
            Console.WriteLine(outputMessage);
        }
    }
}
