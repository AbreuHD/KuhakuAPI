using Serilog;

namespace Core.Application.Helpers.Logger
{
    public static class LoggerHelper
    {
        public static void CustomLog(CustomLogLevel customLevel, string customMessage, LogLevels level, string Value = "")
        {
            var log = Log.ForContext("CustomLevel", customLevel.ToString());

            if (!string.IsNullOrEmpty(Value))
            {
                log = log.ForContext("Data", Value);
            }

            switch (level)
            {
                case LogLevels.Information:
                    log.Information(customMessage);
                    break;

                case LogLevels.Warning:
                    log.Warning(customMessage);
                    break;

                case LogLevels.Error:
                    log.Error(customMessage);
                    break;

                case LogLevels.Debug:
                    log.Debug(customMessage);
                    break;

                case LogLevels.Fatal:
                    log.Fatal(customMessage);
                    break;

                default:
                    log.Information(customMessage);
                    break;
            }
        }
    }
}