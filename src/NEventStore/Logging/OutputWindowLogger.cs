namespace NEventStore.Logging
{
    using System;
    using System.Diagnostics;

    public class OutputWindowLogger : NEventStoreBaseLogger
    {
        private static readonly object Sync = new object();
        private readonly Type _typeToLog;

        public OutputWindowLogger(Type typeToLog, LogLevel logLevel = LogLevel.Info) : base(logLevel)
        {
            _typeToLog = typeToLog;
        }

        public override void Verbose(string message, params object[] values)
        {
            DebugWindow("Verbose", message, values);
        }

        public override void Debug(string message, params object[] values)
        {
            DebugWindow("Debug", message, values);
        }

        public override void Info(string message, params object[] values)
        {
            TraceWindow("Info", message, values);
        }

        public override void Warn(string message, params object[] values)
        {
            TraceWindow("Warn", message, values);
        }

        public override void Error(string message, params object[] values)
        {
            TraceWindow("Error", message, values);
        }

        public override void Fatal(string message, params object[] values)
        {
            TraceWindow("Fatal", message, values);
        }

        protected virtual void DebugWindow(string category, string message, params object[] values)
        {
            lock (Sync)
            {
                System.Diagnostics.Debug.WriteLine(category, message.FormatMessage(_typeToLog, values));
            }
        }

        protected virtual void TraceWindow(string category, string message, params object[] values)
        {
            lock (Sync)
            {
#if !NETSTANDARD1_6
                Trace.WriteLine(category, message.FormatMessage(_typeToLog, values));
#else
                System.Diagnostics.Debug.WriteLine(category, message.FormatMessage(_typeToLog, values));
#endif
            }
        }
    }
}