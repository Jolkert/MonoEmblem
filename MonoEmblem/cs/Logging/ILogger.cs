using System;

namespace MonoEmblem.Logging;
public interface ILogger
{
	void Log(LogMessage message);

	void Info(string message);
	void Info(object obj);

	void Debug(string message);
	void Debug(object obj);

	void Verbose(string message);
	void Verbose(object obj);

	void Warn(string message, Exception? exception);
	void Warn(object obj, Exception? exception);

	void Error(string message, Exception? exception);
	void Error(object obj, Exception? exception);

	void Critical(string message, Exception? exception);
	void Critical(object obj, Exception? exception);
}
