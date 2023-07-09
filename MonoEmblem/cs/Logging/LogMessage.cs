using System;

namespace MonoEmblem.Logging;
public readonly record struct LogMessage(DateTimeOffset Timestamp, Severity Severity, string Message, Exception? Exception = null)
{
	public LogMessage(Severity severity, string message, Exception? exception = null) :
		this(DateTimeOffset.Now, severity, message, exception)
	{ }

	public override string ToString() =>
		$"{Timestamp:HH:mm:ss.fff} [{Severity}] {Message}{(Exception is null ? "" : $" :: {Exception.Message} ({Exception.StackTrace})")}";
}