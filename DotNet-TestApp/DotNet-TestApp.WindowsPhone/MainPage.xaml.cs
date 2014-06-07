using Esri.ArcGISRuntime.Location;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
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


        ExMap _exMap;
        bool _graphicsOnMap = false;
        double _nextBtnOpacity = 0.3;
        double _prevBtnOpacity = 0.3;
        double _extentBtnOpacity = 1.0;
        string _storyBoardDirection = string.Empty;

        

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            _exMap = new ExMap(myMap);

            _exMap.PanToChangedEvent += _exMap_PanToChangedEvent;
            _exMap.NavigateExtentDoneEvent += _exMap_NavigateExtentDoneEvent;
        }

        void _exMap_NavigateExtentDoneEvent(object sender, NavigateExtentDoneEventArgs e)
        {
            if (e.navigateExtentOn == NavigateExtentFocus.Both)
            {
                if (e.nextEnabled)
                    _nextBtnOpacity = 1;
                else
                    _nextBtnOpacity = 0.3;

                if (e.previousEnabled)
                    _prevBtnOpacity = 1;
                else
                    _prevBtnOpacity = 0.3;

            }
            else if (e.navigateExtentOn == NavigateExtentFocus.Next)
            {
                if (e.nextEnabled)
                    _nextBtnOpacity = 1;
                else
                    _nextBtnOpacity = 0.3;
            }
            else
            {
                if (e.previousEnabled)
                    _prevBtnOpacity = 1;
                else
                    _prevBtnOpacity = 0.3;
            }
        }

        void _exMap_PanToChangedEvent(object sender, string direction)
        {
            RunStoryBoard(direction);
        }

      
        public void MapFullExtent()
        {
            if (_exMap != null)
                _exMap.FullExtent();
        }

       
        public void ClearMapGraphics()
        {
            _exMap.ClearGraphicsLayer();
            _graphicsOnMap = false;
        }

        public void EnableAutoNav()
        {
            _exMap.EnableAutoNav();
        }

        public void SetAutoNavBase()
        {
            _exMap.SetAutoNavBase();
        }

        public void StopAutoNav()
        {
            _exMap.DisableAutoNav();
        }

        public void SetDrawMode(string mode)
        {
            if (string.IsNullOrEmpty(mode))
                return;

            _graphicsOnMap = true;
            switch (mode)
            {
                case "point":
                    _exMap.SetDrawMode(GeoDrawMode.Point);
                    break;
                case "line":
                    _exMap.SetDrawMode(GeoDrawMode.Line);
                    break;
                case "polygon":
                    _exMap.SetDrawMode(GeoDrawMode.Polygon);
                    break;
                case "text":
                    _exMap.SetDrawMode(GeoDrawMode.Text);
                    break;
                case "none":
                    _exMap.SetDrawMode(GeoDrawMode.None);
                    break;
            }
        }

        public void StopDrawControl()
        {
            _exMap.StopDrawControl();
        }

        private void RunStoryBoard(string direction)
        {
            if (_beamImgDirectionStoryboard == null)
                _beamImgDirectionStoryboard = new Storyboard();

            _beamImgDirectionStoryboard.Stop();

            switch (direction)
            {
                case "W":
                    _beamImgDirectionStoryboard = beamImgW_SB;
                    beamImgW_SB.Begin();
                    break;
                case "E":
                    _beamImgDirectionStoryboard = beamImgE_SB;
                    _beamImgDirectionStoryboard.Begin();
                    break;
                case "N":
                    _beamImgDirectionStoryboard = beamImgN_SB;
                    _beamImgDirectionStoryboard.Begin();
                    break;
                case "S":
                    _beamImgDirectionStoryboard = beamImgS_SB;
                    _beamImgDirectionStoryboard.Begin();
                    break;
                case "NE":
                    _beamImgDirectionStoryboard = beamImgNE_SB;
                    _beamImgDirectionStoryboard.Begin();
                    break;
                case "SE":
                    _beamImgDirectionStoryboard = beamImgSE_SB;
                    _beamImgDirectionStoryboard.Begin();
                    break;
                case "NW":
                    _beamImgDirectionStoryboard = beamImgNW_SB;
                    _beamImgDirectionStoryboard.Begin();
                    break;
                case "SW":
                    _beamImgDirectionStoryboard = beamImgSW_SB;
                    _beamImgDirectionStoryboard.Begin();
                    break;
                case "none":
                    _beamImgDirectionStoryboard.Stop();
                    break;
                default: break;
            }
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
            MapFullExtent();
        }

        private void btnPrevExtent_MapNav_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //_exMap.PrivousExtent();
        }

        private void btnNextExtent_MapNav_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //_exMap.NextExtent();
        }

        private void btnAutoNav_MapNav_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (btnAutoNav_MapNav.Opacity != 1)
                return;

            ShowHideAutoNavigationArrows(true);

            EnableAutoNav();

            btnAutoNav_MapNav.Opacity = 0.3;
        }

        private void ShowHideAutoNavigationArrows(bool isVisible)
        {
            if (isVisible)
            {
                imgE.Visibility = Windows.UI.Xaml.Visibility.Visible;
                imgN.Visibility = Windows.UI.Xaml.Visibility.Visible;
                imgNE.Visibility = Windows.UI.Xaml.Visibility.Visible;
                imgNW.Visibility = Windows.UI.Xaml.Visibility.Visible;
                imgS.Visibility = Windows.UI.Xaml.Visibility.Visible;
                imgSE.Visibility = Windows.UI.Xaml.Visibility.Visible;
                imgSW.Visibility = Windows.UI.Xaml.Visibility.Visible;
                imgW.Visibility = Windows.UI.Xaml.Visibility.Visible;
                btnAutoNavBase_MapNav.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                imgE.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                imgN.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                imgNE.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                imgNW.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                imgS.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                imgSE.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                imgSW.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                imgW.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                btnAutoNavBase_MapNav.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

        }

        private void btnRedDrawPoint_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SetDrawMode("point");

            ResetDrawButtons(btnRedDrawPoint);
        }

        private void btnRedDrawLine_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SetDrawMode("line");

            ResetDrawButtons(btnRedDrawLine);
        }

        private void btnRedDrawPolygon_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SetDrawMode("polygon");

            ResetDrawButtons(btnRedDrawPolygon);
        }

        private void btnRedDrawText_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SetDrawMode("text");

            ResetDrawButtons(btnRedDrawText);
        }

        private void btnRedSettings_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/View/RedSetPivotPage.xaml", UriKind.Relative));
        }


        private void btnMeasurSet_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        
        private void ResetDrawButtons(Image img)
        {
            btnRedDrawPoint.Opacity = 1;
            btnRedDrawLine.Opacity = 1;
            btnRedDrawPolygon.Opacity = 1;
            btnRedDrawText.Opacity = 1;

            if (img != null)
                (img as Image).Opacity = 0.3;
        }

        private void btnClearMapGraphics_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ClearMapGraphics();
        }

        private void AppBarButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            //////disable autp nav
            StopAutoNav();
            btnAutoNav_MapNav.Opacity = 1;
            ShowHideAutoNavigationArrows(false);

            /////Disable Drawing/////
            ResetDrawButtons(null);
            StopDrawControl();

            //Disable Loaction
            myMap.LocationDisplay.IsEnabled = false;
        }

        private void AppBarButtonLocation_Click(object sender, RoutedEventArgs e)
        {
            _exMap.EnableMyLocation();
        }

        private void btnMapFullScrn_Click(object sender, RoutedEventArgs e)
        {
            if (!isMapFullScrn)
            {
                mapContent.SetValue(Grid.RowProperty, 0);
                mapContent.SetValue(Grid.RowSpanProperty, 2);

                btnMapFullScrn.Content = "v";

                functionsPivot.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                isMapFullScrn = true;
            }
            else
            {
                mapContent.SetValue(Grid.RowProperty, 1);
                mapContent.SetValue(Grid.RowSpanProperty, 1);

                btnMapFullScrn.Content = "^";

                functionsPivot.Visibility = Windows.UI.Xaml.Visibility.Visible;

                isMapFullScrn = false;
            }
        }

        private void btnAutoNavBase_MapNav_Click(object sender, RoutedEventArgs e)
        {
            SetAutoNavBase();
        }
    }
}
