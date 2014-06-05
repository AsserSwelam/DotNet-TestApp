using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet_TestApp
{
    public class NavigateExtentDoneEventArgs : EventArgs
    {
        public NavigateExtentFocus navigateExtentOn;
        public bool previousEnabled;
        public bool nextEnabled;

    }
}
