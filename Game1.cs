using Hexa.Systems.Cameras;
using Hexa.Systems.InputUtil;
using Hexa.Systems.MapTech;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hexa;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public static CameraSystem camSystem = new CameraSystem();
    public static MapSystem mapSystem = new MapSystem();
    public static InputSystem inputSystem = new InputSystem();

    public static Texture2D pixel;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData(new Color[]{Color.White});

        camSystem.Init(GraphicsDevice, inputSystem);
        mapSystem.Init(Content, camSystem);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        
        
        inputSystem.Update();
        camSystem.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        mapSystem.Draw(_spriteBatch);
        base.Draw(gameTime);
    }
}
