using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Logging
{
    public class CustomLogger : ILogger
    {
        readonly string loggerName;

        readonly CustomLoggerProviderConfig loggerConfig;



        public CustomLogger(string name, CustomLoggerProviderConfig config)
        {
            loggerName = name;
            loggerConfig = config;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {

            string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

            ExcreverTextoNoArquivo(mensagem);

        }

        private void ExcreverTextoNoArquivo(string mensagem)
        {
            string caminhoArquivo = @"/Users/horleyleitaomonteiro/Desktop/C#/dados/log/Macaratti_Log.txt";

            using (StreamWriter streamWriter = new StreamWriter(caminhoArquivo, true))
            {
                try
                {
                    streamWriter.WriteLine(mensagem);
                    streamWriter.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}