using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hexa.Systems.InputUtil;

public class InputSystem
{
    private MouseState oldMouse;
    
    public bool IsLeftMouseNew { get; private set; }
    public bool IsLeftMouse { get; private set; }
    
    public Vector2 MousePosition { get; private set; }
    
    public int RawScroll { get; private set; }
    
    
    public void Update()
    {
        var ms = Mouse.GetState();

        IsLeftMouseNew = (ms.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed);
        IsLeftMouse = ms.LeftButton == ButtonState.Pressed;
        MousePosition = ms.Position.ToVector2();
        RawScroll = ms.ScrollWheelValue;
        
        oldMouse = ms;
    }

    public float GetScrollRatio(int size) // produce between -1 and 1
    {
        var x = RawScroll / (size * 10f);
        return Math.Clamp(x, -1, 1);
    }

    
}