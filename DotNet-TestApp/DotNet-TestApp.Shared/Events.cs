using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet_TestApp
{
    public delegate void NavigateExtentDone(object sender, NavigateExtentDoneEventArgs e);

    public delegate void MapPanToDirection(object sender, string direction);

    //public delegate void DrawControlDrawCompleted(object sender, DrawEventArgs e);
}
