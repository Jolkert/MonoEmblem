namespace MonoEmblem;

internal static class Program
{
	public static MonoEmblemGame GameInstance { get; private set; } = null!;

	private static void Main()
	{
		GameInstance = new MonoEmblemGame(WindowMode.BorderlessWindow);
		GameInstance.Run();
	}
}