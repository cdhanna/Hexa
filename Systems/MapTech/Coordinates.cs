using System;
using Microsoft.Xna.Framework;

namespace Hexa.Systems.MapTech;


public struct HexCubeCoord
{
    public bool Equals(HexCubeCoord other)
    {
        return q == other.q && r == other.r && s == other.s;
    }

    public override bool Equals(object obj)
    {
        return obj is HexCubeCoord other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(q, r, s);
    }

    public int q, r, s;

    public static HexCubeCoord Zero { get; } = new HexCubeCoord();
    public static HexCubeCoord Top { get; } = new HexCubeCoord(0, -1, 1);
    public static HexCubeCoord Low { get; } = new HexCubeCoord(0, 1, -1);
    public static HexCubeCoord LeftTop { get; } = new HexCubeCoord(-1, 0, 1);
    public static HexCubeCoord LeftLow { get; } = new HexCubeCoord(-1, 1, 0);
    public static HexCubeCoord RightTop { get; } = new HexCubeCoord(1, -1, 0);
    public static HexCubeCoord RightLow { get; } = new HexCubeCoord(1, 0, -1);


    public HexCubeCoord()
    {
        q = 0;
        r = 0;
        s = 0;
    }

    public HexCubeCoord(int q, int r)
    {
        this.q = q;
        this.r = r;
        this.s = -q - r;
        AssertValid();
    }
    
    
    public HexCubeCoord(int q, int r, int s)
    {
        this.q = q;
        this.r = r;
        this.s = s;
        AssertValid();
    }

    public static HexCubeCoord operator +(HexCubeCoord a, HexCubeCoord b) => new HexCubeCoord(b.q + a.q, b.r + a.r, b.s + a.s);
    public static HexCubeCoord operator -(HexCubeCoord b, HexCubeCoord a) => new HexCubeCoord(b.q - a.q, b.r - a.r, b.s - a.s);

    public static bool operator ==(HexCubeCoord b, HexCubeCoord a) => a.Equals(b);

    public static bool operator !=(HexCubeCoord b, HexCubeCoord a) => !(b == a);

    public static int Distance(HexCubeCoord a, HexCubeCoord b)
    {
        var vec = a - b;
        return (Math.Abs(vec.q) + Math.Abs(vec.r) + Math.Abs(vec.s)) / 2;
    }

    private static readonly float ROOT_3 = (float)Math.Sqrt(3);
    private static readonly float ROOT_3_BY3 = (float)Math.Sqrt(3)/3f;
    private static readonly float ROOT_3_BY2 = (float)Math.Sqrt(3)/2f;

    public static Vector2 ToPixel(HexCubeCoord coord, int size)
    {
        var x = size * (3f / 2 * coord.q);
        var y = size * (ROOT_3_BY2 * coord.q + ROOT_3 * coord.r);
        return new Vector2(x, y);
    }
    
    public static HexCubeCoord FromPixel(int x, int y, int size)
    {
        var q = (((2 / 3f * x) / size));
        var r = (((-1 / 3f * x + ROOT_3_BY3 * y) / size) );

        var s = -q - r;
        var coord = Round(q, r, s);
        
        return coord;
    }

    public static HexCubeCoord Round(float q, float r, float s)
    {

        var qi = (int)Math.Round(q);
        var ri = (int)Math.Round(r);
        var si = (int)Math.Round(s);
        var qDiff = Math.Abs(qi - q);
        var rDiff = Math.Abs(ri - r);
        var sDiff = Math.Abs(si - s);
    
        if (qDiff > rDiff && qDiff > sDiff)
        {
            qi = -ri - si;
        } else if (rDiff > sDiff)
        {
            ri = -qi - si;
        }
        else
        {
            si = -qi - ri;
        }
    
        return new HexCubeCoord(qi, ri, si);
    }
    
    
    public void AssertValid()
    {
        if (q + r + s == 0) return;
        throw new InvalidOperationException("invalid non zero coord");
    }
        
    // public HexOddQCoord ToOddQ() => new HexOddQCoord
    // {
    //     col = q,
    //     row = r + (q - (q&1)) / 2
    // };
}

// public struct HexOddQCoord
// {
//     public int row, col;
//     
//     public HexCubeCoord ToCube()
//     {
//         var q = col;
//         var r = row - (col - (col & 1)) / 2;
//         return new HexCubeCoord
//         {
//             q = q,
//             r = r,
//             s = -q - r
//         };
//     }
// }
