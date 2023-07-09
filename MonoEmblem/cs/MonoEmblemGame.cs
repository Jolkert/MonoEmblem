using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoEmblem.Content;
using MonoEmblem.Control;
using MonoEmblem.Data;
using MonoEmblem.Entities;
using MonoEmblem.Entities.Data;
using MonoEmblem.Extension;
using MonoEmblem.Graphics;
using MonoEmblem.Logging;
using MonoEmblem.Tiles;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.Xna.Framework.Graphics.GraphicsAdapter;
using IDrawable = MonoEmblem.Graphics.IDrawable;

namespace MonoEmblem;
public partial class MonoEmblemGame : Game
{
	private static readonly Color CursorColor = new Color(0xff_99eeff);

	private readonly ResourceManager _resources;
	private readonly GraphicsDeviceManager _graphics;
	private SpriteBatchExt _spriteBatch = null!;
	private readonly List<IDrawable> _drawables = new List<IDrawable>();
	private readonly Camera _camera;

	private Map _testMap = null!;
	private Cursor _cursor = null!;

	public Point Dimensions { get; }
	public ILogger Logger => _spriteBatch;

	public MonoEmblemGame(WindowMode displayMode = WindowMode.Windowed, int width = 1280, int height = 720)
	{
		_graphics = new GraphicsDeviceManager(this);
		_resources = new ResourceManager(Content, new Dictionary<Type, string>()
		{
			{ typeof(TilesetData), "tilesets" },
			{ typeof(MapData), "maps" }
		})
		{
			AssetsRoot = "content/assets",
			DataRoot = "content/data"
		};

		IsMouseVisible = true;

		switch (displayMode)
		{
			case WindowMode.Windowed:
				_graphics.PreferredBackBufferWidth = width;
				_graphics.PreferredBackBufferHeight = height;
				_graphics.IsFullScreen = false;
				break;

			case WindowMode.Fullscreen:
				_graphics.IsFullScreen = true;
				break;

			case WindowMode.BorderlessWindow:
				_graphics.PreferredBackBufferWidth = DefaultAdapter.CurrentDisplayMode.Width;
				_graphics.PreferredBackBufferHeight = DefaultAdapter.CurrentDisplayMode.Height;
				_graphics.IsFullScreen = false;
				Window.IsBorderless = true;
				break;
		}

		Dimensions = new Point(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
		_camera = new Camera(Dimensions);
	}

	protected override void Initialize() => base.Initialize();
	protected override void LoadContent()
	{
		_testMap = Map.FromJson("test", _resources);
		_testMap.TileDimensions = new Point(128, 128);
		_testMap.CenterInScreen(Dimensions);

		_testMap.AddUnit(new Unit(_resources.LoadAsset<Texture2D>("textures/units/carbink"),
			_testMap,
			new Point(9, 5),
			Team.Player,
			new Stats(
				MaxHp: 40,
				Str: 11,
				Mag: 5,
				Dex: 10,
				Spd: 10,
				Def: 10,
				Res: 8,
				Lck: 8,
				Con: 8,
				Mov: 8
			)
		));


		_cursor = new Cursor(_resources.LoadAsset<Texture2D>("textures/overlay_tiles/cursor"), _testMap)
		{
			Position = new Point(0, 0)
		};
		_cursor.Sprite.SetDimensions(128, 128);
		_cursor.Sprite.Tint = CursorColor;

		_spriteBatch = new SpriteBatchExt(GraphicsDevice, _resources.LoadAsset<SpriteFont>("fonts/arial"), _resources) { MaxQueueSize = 30 };
		_spriteBatch.Info("Content loaded!");

		_drawables.Add(_testMap);
		_drawables.Add(_cursor);
	}

	private const float CameraSpeed = 10f;
	protected override void Update(GameTime gameTime)
	{// dont draw here
		ProcessInput();
		base.Update(gameTime);
	}
	private void ProcessInput()
	{
		if (Input.IsKeyDown(Keys.Delete))
			Exit();

		// camera
		if (Input.IsKeyDown(Keys.Right))
			_camera.Translate(Vector2.UnitX * -CameraSpeed);
		if (Input.IsKeyDown(Keys.Left))
			_camera.Translate(Vector2.UnitX * CameraSpeed);
		if (Input.IsKeyDown(Keys.Up))
			_camera.Translate(Vector2.UnitY * CameraSpeed);
		if (Input.IsKeyDown(Keys.Down))
			_camera.Translate(Vector2.UnitY * -CameraSpeed);

		// recenter camera
		if (Input.IsKeyDown(Keys.OemComma))
			_camera.CenterAt(Dimensions / new Point(2, 2));

		// move cursor
		if (Input.IsKeyRising(Keys.W))
			_cursor.Position -= new Point(0, 1);
		if (Input.IsKeyRising(Keys.A))
			_cursor.Position -= new Point(1, 0);
		if (Input.IsKeyRising(Keys.S))
			_cursor.Position += new Point(0, 1);
		if (Input.IsKeyRising(Keys.D))
			_cursor.Position += new Point(1, 0);

		if (Input.IsKeyRising(Keys.Space))
			_testMap.Interact(_cursor.Position);
		if (Input.IsKeyDown(Keys.Escape))
			_testMap.Deselect();

		Input.AdvanceFrame();
	}

	protected override void Draw(GameTime gameTime)
	{// only draw here
		GraphicsDevice.Clear(Color.DarkGray);

		_spriteBatch.Begin(
			samplerState: SamplerState.PointClamp,
			transformMatrix: _camera.TransformMatrix
		);

		foreach (var drawable in _drawables)
			drawable.Draw(gameTime, _spriteBatch);

		_spriteBatch.End();
		base.Draw(gameTime);
	}
}