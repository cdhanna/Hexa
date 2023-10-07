using System;
using System.Collections.Generic;
using System.Linq;
using Hexa.Systems.Cameras;
using Hexa.Systems.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Hexa.Systems.MapTech;


public class MapSystem
{
    private Texture2D _tileTexture;
    private List<Sprite> _sprites;

    public int size = 100;
    // public int hexWidth = 100;
    // public int hexHeight = 100;

    // private readonly float ROOT_3 = MathF.Sqrt(3);
    
    public float horizDist => 1.5f * size;
    public float vertDist => (float)Math.Sqrt(3) * size;

    public Vector2 worldCenter = new Vector2(0,0);
    
    public List<HexCubeCoord> coords = new List<HexCubeCoord>();
    private CameraSystem _camSystem;


    public void Init(ContentManager content, CameraSystem camSystem)
    {
        _camSystem = camSystem;
        _tileTexture = content.Load<Texture2D>("tiles/Flat/Terrain 1 - Flat - Black Outline 2px - 128x128");
        _sprites = SpriteFunctions.Splice(_tileTexture, 3, 4);
        
        
        // coords.Add(new HexCubeCoord());
        // coords.Add(HexCubeCoord.Top);
        // coords.Add(HexCubeCoord.LeftTop);
        // coords.Add(HexCubeCoord.LeftLow);
        // coords.Add(HexCubeCoord.Low);
        // coords.Add(HexCubeCoord.RightLow);
        // coords.Add(HexCubeCoord.RightTop);
        // coords.Add(HexCubeCoord.RightTop+HexCubeCoord.RightLow);

        for (var x = -10; x < 10; x++)
        for (var y = -10; y < 10; y++)
        {
            {
                var coord = HexCubeCoord.FromPixel((int)(x * horizDist), (int)(y * vertDist), size);
                coords.Add(coord);
            }
        }

        // for (var q = -3; q < 3; q ++)
        // {
        //     for (var r = -3; r < 3; r++)
        //     {
        //         var coord = new HexCubeCoord(q, r);
        //         coords.Add(coord);
        //     }
        // }
        
        // coords.Add(new HexCubeCoord(0,0,0));
        // coords.Add(new HexCubeCoord(2, -1));
        

        // var q = coords.Select(x => x.ToOddQ()).ToList();
        // foreach (var n in q)
        // {
        //     Console.WriteLine($"{n.col}, {n.row}");
        // }

    }
    

    public void Draw(SpriteBatch sb)
    {
        sb.Begin(transformMatrix: _camSystem.cameraMatrix);

        
        var mousePos = Game1.camSystem.ScreenPointToWorld(Game1.inputSystem.MousePosition);

        var mouseCoord = HexCubeCoord.FromPixel((int)mousePos.X, (int)mousePos.Y, size);
        // Console.WriteLine(mouseCoord.q + "," + mouseCoord.r);

        var x = new Sprite()
        {
            color = Color.Red,
            origin = Vector2.One * .5f, size = new Vector2(1, 1), src = new Rectangle(0, 0, 1, 1),
            texture = Game1.pixel
        };
        foreach (var coord in coords)
        {
            // var pos = ToWorldSpace(coord);
            var pos = HexCubeCoord.ToPixel(coord, size);

            var dist = HexCubeCoord.Distance(coord, HexCubeCoord.Zero);
            var index = (dist%2) == 0 ? 5 : 3;

            _sprites[index].color = Color.Gray;
            if (mouseCoord == coord)
            {
                _sprites[index].color = Color.White;
            }

            
            sb.DrawSprite(_sprites[index], pos, 1*new Vector2( horizDist / .75f,  vertDist));

            x.color = Color.Red;
            sb.DrawSprite(x, pos, new Vector2(5, 5));
            
            
            // x.color = Color.GreenYellow;
            // sb.DrawSprite(x, pixelLoc + Vector2.UnitY * 5, new Vector2(5, 5));

        }
        
        
       
        sb.DrawSprite(x, mousePos, new Vector2(30, 30));

        // sb.DrawSprite(_sprites[1], new Vector2(0, 0));
        
        sb.End();
        
    }
    
}