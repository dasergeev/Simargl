using Microsoft.Extensions.Logging;
using Simargl.Embedded.Tunings.TuningsEditor.Core;
using System.Windows.Controls;

namespace Simargl.Embedded.Tunings.TuningsEditor.Logging;

internal class UILoggerProvider(Heart heart/*, MainWindow mainWindow*/) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new UiLogger(heart, /*mainWindow.MainControl.LogView.LogBox,*/ categoryName);
    }

    public void Dispose() { }
    
    private class UiLogger(Heart heart, /*TextBox output,*/ string category) : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null!;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId,
            TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            category = category.Split('.')[^1];
            var msg = $"{DateTime.Now:HH:mm:ss} [{logLevel}] {category}: {formatter(state, exception)}";
            if (exception != null)
                msg += Environment.NewLine + exception;

            heart.LogQueue.Enqueue(msg);

            //output.Dispatcher.Invoke(() =>
            //{
            //    output.AppendText(msg + Environment.NewLine);
            //    output.ScrollToEnd();
            //});
        }
    }
}
