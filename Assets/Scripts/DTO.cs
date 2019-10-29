using System.Collections;
using System.Collections.Generic;

public class DTO
{
    public int Number { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public void CopyTo(DTO other)
    {
        other.Number = this.Number;
        other.X = this.X;
        other.Y = this.Y;
    }
}
