using System;

namespace Hexa.Systems.MapTech;


public struct HexCubeCoord
{
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
    
    
    public HexCubeCoord(int q, int r, int s)
    {
        this.q = q;
        this.r = r;
        this.s = s;
        AssertValid();
    }

    public static HexCubeCoord operator +(HexCubeCoord a, HexCubeCoord b) => new HexCubeCoord(b.q + a.q, b.r + a.r, b.s + a.s);
    public static HexCubeCoord operator -(HexCubeCoord b, HexCubeCoord a) => new HexCubeCoord(b.q - a.q, b.r - a.r, b.s - a.s);

    public static int Distance(HexCubeCoord a, HexCubeCoord b)
    {
        var vec = a - b;
        return (Math.Abs(vec.q) + Math.Abs(vec.r) + Math.Abs(vec.s)) / 2;
    }

    private static readonly float ROOT_3_BY3 = (float)Math.Sqrt(3)/3f;
    public static HexCubeCoord FromPixel(int x, int y, int size)
    {
        var q = (int)(((2 / 3f * x) / size) + .5f);
        var r = (int)(((-1 / 3f * x + ROOT_3_BY3 * y) / size) + .5f);
        return new HexCubeCoord(q, r, -q - r);
    }
    
    public void AssertValid()
    {
        if (q + r + s == 0) return;
        throw new InvalidOperationException("invalid non zero coord");
    }
        
    public HexOddQCoord ToOddQ() => new HexOddQCoord
    {
        col = q,
        row = r + (q - (q&1)) / 2
    };
}

public struct HexOddQCoord
{
    public int row, col;
    
    public HexCubeCoord ToCube()
    {
        var q = col;
        var r = row - (col - (col & 1)) / 2;
        return new HexCubeCoord
        {
            q = q,
            r = r,
            s = -q - r
        };
    }
}
