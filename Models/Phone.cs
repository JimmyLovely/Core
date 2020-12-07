namespace NetCore.Models
{
    public class Phone
    {
        public Phone()
        {
        }

        public string Read(string name)
        {
            System.Console.WriteLine(name, "start to read book");
            return (name + "start to read book");
        }

        public string Play(string name)
        {
            System.Console.WriteLine(name, "start to play game");
            return (name + "start to play game");
        }

        public string money(string name)
        {
            System.Console.WriteLine(name, "start to work for money");
            return (name + "start to work for mone");
        }
    }
}