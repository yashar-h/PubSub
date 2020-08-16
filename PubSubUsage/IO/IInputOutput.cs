namespace PubSubUsage.IO
{
    public interface IInputOutput
    {
        string Read();
        void Write(string outputMessage);
    }
}
