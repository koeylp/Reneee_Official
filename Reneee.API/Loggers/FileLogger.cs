namespace Reneee.API.Loggers
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath;
        private static object _lock = new object();

        public FileLogger(string filePath)
        {
            _filePath = filePath;
            var logDirectory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                    if (!File.Exists(_filePath))
                    {
                        using (var stream = File.Create(_filePath)) { }
                    }

                    File.AppendAllText(_filePath, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: {logLevel.ToString()}: {formatter(state, exception)}{Environment.NewLine}");
                }
            }
        }
    }

    public class FileLoggerProvider(string filePath) : ILoggerProvider
    {
        private readonly string _filePath = filePath;

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_filePath);
        }

        public void Dispose()
        {
        }
    }
}
