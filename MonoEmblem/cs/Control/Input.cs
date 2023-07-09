using Microsoft.Xna.Framework.Input;
using System;

namespace MonoEmblem.Control;
public static class Input
{
	private static InputState PreviousFrame;
	private static InputState CurrentFrame;
	
	// TODO: consider swapping the "Rising" and "Falling" methods
	// they were written with "rising edge" and "falling edge" in mind, but seem physically reversed -morgan 2023-07-05
	public static bool IsKeyDown(Keys key) => CurrentFrame.Keyboard.IsKeyDown(key);
	public static bool IsKeyUp(Keys key) => CurrentFrame.Keyboard.IsKeyUp(key);
	public static bool IsKeyRising(Keys key) => PreviousFrame.Keyboard.IsKeyUp(key) && CurrentFrame.Keyboard.IsKeyDown(key);
	public static bool IsKeyFalling(Keys key) => PreviousFrame.Keyboard.IsKeyDown(key) && CurrentFrame.Keyboard.IsKeyUp(key);

	public static bool IsMouseDown(MouseButton button = MouseButton.Left) => CurrentFrame.IsMouseDown(button);
	public static bool IsMouseUp(MouseButton button = MouseButton.Left) => CurrentFrame.IsMouseUp(button);
	public static bool IsMouseRising(MouseButton button = MouseButton.Left) => PreviousFrame.IsMouseUp(button) && CurrentFrame.IsMouseDown(button);
	public static bool IsMouseFalling(MouseButton button = MouseButton.Left) => PreviousFrame.IsMouseDown(button) && CurrentFrame.IsMouseUp(button);

	public static void AdvanceFrame()
	{
		PreviousFrame = CurrentFrame;
		CurrentFrame = new InputState(Keyboard.GetState(), Mouse.GetState());
	}

	public readonly record struct InputState(KeyboardState Keyboard, MouseState Mouse)
	{
		public bool IsMouseDown(MouseButton button) => ButtonState.Pressed == button switch
		{
			MouseButton.Left => Mouse.LeftButton,
			MouseButton.Right => Mouse.RightButton,
			MouseButton.Middle => Mouse.MiddleButton,
			_ => throw new ArgumentException($"Invalid mouse button [{button}]!")
		};
		public bool IsMouseUp(MouseButton button) => !IsMouseDown(button);
	}
}
