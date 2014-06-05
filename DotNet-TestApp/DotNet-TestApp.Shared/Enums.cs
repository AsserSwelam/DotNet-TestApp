using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet_TestApp
{
    public enum NavigateExtentFocus
    {
        Both = 0,
        Previous = 1,
        Next = 2 
    }

    public enum Quarter
    {
        TopLeft = 0,
        TopRight = 2,
        DownLeft = 3,
        DownRight = 4
    }

    public enum Directions
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
        NorthEast = 4,
        NorthWest = 5,
        SouthEast = 6,
        SouthWest = 7,
        None = 8
    }

    public enum GeoDrawMode
    {
        Point = 0,
        Line = 1,
        Polygon = 2,
        Text = 3,
        None = 4
    }
}
