using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DotNet_TestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool isMapFullScrn = false;

        Storyboard _beamImgDirectionStoryboard;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            ExMap = new ExMap(esriMap);

            GeoMap.PanToChangedEvent += GeoMap_PanToChangedEvent;
            _geoMap.NavigateExtentDoneEvent += _geoMap_NavigateExtentDoneEvent;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void btnFullExtent_MapNav_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void btnPrevExtent_MapNav_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void btnNextExtent_MapNav_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void btnAutoNav_MapNav_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void btnRedDrawPoint_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void btnRedDrawLine_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void btnRedDrawPolygon_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void btnRedDrawText_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void btnRedSettings_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void btnMapFullScrn_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void btnAutoNavBase_MapNav_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void btnMeasurSet_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void AppBarButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void AppBarButton_PointerPressed_1(object sender, PointerRoutedEventArgs e)
        {

        }

        private void btnClearMapGraphics_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }
    }
}
