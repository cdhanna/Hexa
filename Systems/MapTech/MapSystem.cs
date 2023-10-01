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

    public int hexWidth = 100;
    public int hexHeight = 100;

    // private readonly float ROOT_3 = MathF.Sqrt(3);
    
    public float horizDist => .75f * hexWidth;
    public float vertDist => hexHeight;

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
                var coord = HexCubeCoord.FromPixel(x * hexWidth, y * hexHeight, hexWidth);
                coords.Add(coord);
            }
        }


        var q = coords.Select(x => x.ToOddQ()).ToList();
        foreach (var n in q)
        {
            Console.WriteLine($"{n.col}, {n.row}");
        }

    }

    public Vector2 ToWorldSpace(HexCubeCoord coord)
    {
        var oddr = coord.ToOddQ();

        var pos = new Vector2(
            oddr.col * horizDist,
            oddr.row * vertDist);

        if ((oddr.col % 2) == 0)
        {
            pos -= Vector2.UnitY * hexHeight * .5f;
        }
        
        pos -= worldCenter;

        return pos;
    }
    

    public void Draw(SpriteBatch sb)
    {
        sb.Begin(transformMatrix: _camSystem.cameraMatrix);

        foreach (var coord in coords)
        {
            var pos = ToWorldSpace(coord);

            var dist = HexCubeCoord.Distance(coord, HexCubeCoord.Zero);
            var index = (dist%2) == 0 ? 5 : 3;
            
            
            sb.DrawSprite(_sprites[index], pos, new Vector2(hexWidth, hexHeight));

        }
        // sb.DrawSprite(_sprites[1], new Vector2(0, 0));
        
        sb.End();
        
    }
    
}