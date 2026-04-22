using MazeGame.Model.Loggers;
using System;
using System.IO;

namespace MazeGame.Model.Observers
{
    public class FileLoggerObserver(string filePath) : ILogObserver
    {
        private readonly string _filePath = filePath;

        public void OnLog(string message)
        {

                string timeStampedMessage = $"[{DateTime.Now:HH:mm:ss}] {message}\n";
                File.AppendAllText(_filePath, timeStampedMessage);
        }
    }
}