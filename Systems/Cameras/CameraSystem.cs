using Hexa.Systems.InputUtil;
using Hexa.Systems.Transforms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hexa.Systems.Cameras;

public class CameraSystem
{
    public Matrix cameraMatrix;
    public Transform target;
    private GraphicsDevice _device;
    private InputSystem _inputSystem;

    private Vector2 dragAnchor;
    private Transform dragStart;

    public void Init(GraphicsDevice device, InputSystem inputSystem)
    {
        _inputSystem = inputSystem;
        _device = device;
    }
    
    public void Update(GameTime gameTime)
    {
        
        // handle input.
        if (_inputSystem.IsLeftMouseNew)
        {
            // grab mouse position and save it as the anchor
            dragAnchor = _inputSystem.MousePosition;
            dragStart = target;
            
        } else if (_inputSystem.IsLeftMouse)
        {
            var diff = _inputSystem.MousePosition - dragAnchor;

            target = dragStart;
            target.position += (diff);
        }
        
        target.scale = Vector2.One;
        var z = _inputSystem.GetScrollRatio(100);
        z *= .8f;
        target.scale += z * Vector2.One;
        // snap.

        cameraMatrix = Matrix.Identity;
        cameraMatrix *= Matrix.CreateScale(target.scale.X, target.scale.Y, 1);

        cameraMatrix *= Matrix.CreateTranslation(target.position.X, target.position.Y, 0);

        var sizeMatrix = Matrix.CreateTranslation(_device.Viewport.Width * .5f, _device.Viewport.Height * .5f, 0);

        cameraMatrix *= sizeMatrix;

    }
    
    
}