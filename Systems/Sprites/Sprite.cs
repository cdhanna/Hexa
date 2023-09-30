using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hexa.Systems.Sprites;

public class Sprite
{
    public Texture2D texture;
    public Color color;
    public Vector2 size;
    public Rectangle src;
    public Vector2 origin;
}

public static class SpriteFunctions
{

    public static void DrawSprite(this SpriteBatch sb, Sprite sprite, Vector2 position, Vector2 pixelSize)
    {

        var xRatio = sprite.src.Width / pixelSize.X;
        var yRatio = sprite.src.Height / pixelSize.Y;
        
        sb.Draw(
            texture:sprite.texture, 
            position: position, 
            sourceRectangle: sprite.src,
            color: sprite.color, 
            rotation:0, 
            origin: sprite.origin,
            scale: sprite.size / new Vector2(xRatio, yRatio),
            effects: SpriteEffects.None,
            layerDepth: 1
            );
    }
    
    public static List<Sprite> Splice(Texture2D texture, int columns, int rows)
    {
        var width = texture.Width / columns;
        var height = texture.Height / rows;

        var sprites = new List<Sprite>(columns * rows);
        
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < columns; c++)
            {
                var src = new Rectangle(width * r, height * c, width, height);

                var sprite = new Sprite
                {
                    texture = texture,
                    src = src,
                    color = Color.White,
                    size = Vector2.One,
                    origin = new Vector2(width, height) * .5f
                };
                sprites.Add(sprite);
            }
        }

        return sprites;
    }
}
