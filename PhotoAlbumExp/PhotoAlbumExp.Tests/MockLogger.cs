using Microsoft.Extensions.Logging;
using PhotoAlbumExp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbumExp.Tests
{
    /// <summary>
    /// Mock logger class
    /// </summary>
    public class MockLogger : ILogger<PhotoAlbumDataAccess>
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            
        }
    }
}
