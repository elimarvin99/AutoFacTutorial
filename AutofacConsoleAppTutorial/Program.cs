using Autofac;
namespace AutofacConsoleAppTutorial
{
    public interface IOutput
    {
        void Write(string content);
    }
    public class ConsoleOutput : IOutput
    {
        public void Write(string content)
        {
            Console.WriteLine(content);
        }
    }
    public interface IDateWriter
    {
        void WriteDate();
    }
    public class TodayWriter : IDateWriter
    {
        private IOutput _output;
        public TodayWriter(IOutput output)
        {
            this._output = output;
        }
        public void WriteDate() //is abstracted to write the date for today (today write class) but the write date interface can be implemented in other classes
        {
            this._output.Write(DateTime.Today.ToShortDateString());
        }
    }
    public class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();
            Container = builder.Build();

            WriteDate();

        }
        public static void WriteDate()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IDateWriter> (); 
                writer.WriteDate(); 
            }
        }
    }
}

