using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEmblem.Content;
using MonoEmblem.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoEmblem;
public class SpriteBatchExt : SpriteBatch, IDisposable, ILogger
{
	private readonly ResourceManager _resources;
	private readonly Texture2D _rectangle;
	private static readonly Color _boxColor = new Color(Color.Black, .4f);

	public SpriteFont DefaultFont { get; set; }
	public Dictionary<Severity, Color> SeverityColors { get; } = new()
	{
		{ Severity.Info, Color.White },
		{ Severity.Debug, Color.Magenta },
		{ Severity.Verbose, Color.Cyan },
		{ Severity.Warning, Color.Yellow },
		{ Severity.Error, Color.Red },
		{ Severity.Critical, Color.DarkRed },
	};
	public int MaxQueueSize { get; set; } = 20;

	public SpriteBatchExt(GraphicsDevice graphics, SpriteFont defaultFont, ResourceManager resources) : base(graphics)
	{
		DefaultFont = defaultFont;
		_resources = resources;

		_rectangle = new Texture2D(graphics, 1, 1);
		_rectangle.SetData(new[] { Color.White });
	}

	public new void End()
	{
		base.End();
		Begin();
		WriteLogs();
		base.End();
	}

	public void Draw(Texture2D texture, Vector2 position, float scale = 1f, float rotation = 0f, Color? color = null) =>
		Draw(texture, position, null, color ?? Color.White, rotation, new Vector2(0, 0), scale, SpriteEffects.None, 0f);
	public void Draw(Texture2D texture, Vector2 position, int newWidth, float rotation = 0f, Color? color = null) =>
		Draw(texture, position, (float)newWidth / texture.Width, rotation, color);

	#region Logging
	private readonly Queue<LogMessage> _logQueue = new();
	private void WriteLogs()
	{
		int width = (int)DefaultFont.MeasureString(_logQueue.MaxBy((msg) => msg.Message.Length).ToString()).X;
		Draw(_rectangle, new Rectangle(0, 0, width, _logQueue.Count * DefaultFont.LineSpacing), _boxColor);

		int posY = 0;
		foreach (LogMessage message in _logQueue)
		{
			DrawString(DefaultFont, message.ToString(), new Vector2(0, posY), SeverityColors[message.Severity]);
			posY += DefaultFont.LineSpacing;
		}
	}

	public void Log(LogMessage message)
	{
		_logQueue.Enqueue(message);
		if (_logQueue.Count > MaxQueueSize)
			_logQueue.Dequeue();
	}

	public void Info(string text) => Log(new LogMessage(Severity.Info, text));
	public void Info(object obj) => Info(obj?.ToString() ?? "null");

	public void Debug(string text) => Log(new LogMessage(Severity.Debug, text));
	public void Debug(object obj) => Debug(obj?.ToString() ?? "null");

	public void Verbose(string text) => Log(new LogMessage(Severity.Verbose, text));
	public void Verbose(object obj) => Verbose(obj?.ToString() ?? "null");

	public void Warn(string text, Exception? exception = null) => Log(new LogMessage(Severity.Warning, text, exception));
	public void Warn(object obj, Exception? exception = null) => Warn(obj?.ToString() ?? "null", exception);

	public void Error(string text, Exception? exception = null) => Log(new LogMessage(Severity.Error, text, exception));
	public void Error(object obj, Exception? exception = null) => Error(obj?.ToString() ?? "null", exception);

	public void Critical(string text, Exception? exception = null) => Log(new LogMessage(Severity.Critical, text, exception));
	public void Critical(object obj, Exception? exception = null) => Critical(obj?.ToString() ?? "null", exception);
	#endregion
}