using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Location;
using Esri.ArcGISRuntime.Symbology;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;

namespace DotNet_TestApp
{
    class ExMap
    {
        MapView _mapView;
        //WebMercator _mercator;
        GraphicsLayer _myLocationLayer;
        GraphicsLayer _redliningGraphicsLayer;

         List<Envelope> _extentHistory = new List<Envelope>();
        int _currentExtentIndex = 0;
        bool _newExtent = true;
        TiltNavigation _autoNavigation;
        //DrawControl _drawControl;
        GeoDrawMode _drawMode;

        public SimpleMarkerSymbol PointMarkerSymbol;
        public SimpleFillSymbol PolygonFillSymbol;
        public SimpleLineSymbol LineSymbol;
        public TextSymbol TextDrawsymbol;

        public event MapPanToDirection PanToChangedEvent;

        public event NavigateExtentDone NavigateExtentDoneEvent;

        public ExMap(MapView esriMapView)
        {
            _mapView = esriMapView;

            ArcGISTiledMapServiceLayer layer = new ArcGISTiledMapServiceLayer();
            layer.ServiceUri = "http://server.arcgisonline.com/arcgis/rest/services/ESRI_StreetMap_World_2D/MapServer";

            _myLocationLayer = new GraphicsLayer();
            _redliningGraphicsLayer = new GraphicsLayer();

            _mapView.Map.Layers.Add(layer);
            _mapView.Map.Layers.Add(_redliningGraphicsLayer);
            _mapView.Map.Layers.Add(_myLocationLayer);

            _mapView.ExtentChanged += _mapView_ExtentChanged;
            _mapView.NavigationCompleted += _mapView_NavigationCompleted;

            //////////Init DrawControl//////////
            //_drawControl = new DrawControl(_mapView);
            //_drawMode = GeoDrawMode.None;
            //_drawControl.SetDrawMode(DrawMode.None);
            //_drawControl.DrawCompletedEvent += _drawControl_DrawCompletedEvent;

            ///////Init default Draw Symbols////////
            PointMarkerSymbol = new SimpleMarkerSymbol();

            PointMarkerSymbol.Color = Colors.Red;
            PointMarkerSymbol.Size = 15;
            PointMarkerSymbol.Style = SimpleMarkerStyle.Circle;

            PolygonFillSymbol = new SimpleFillSymbol();
            PolygonFillSymbol.Outline = new SimpleLineSymbol();
            PolygonFillSymbol.Outline.Color = Colors.Red;   
            PolygonFillSymbol.Outline.Width = 3;
            PolygonFillSymbol.Color = Color.FromArgb(100, 255, 0, 0);

            LineSymbol = new SimpleLineSymbol();
            LineSymbol.Color = Colors.Red;
            LineSymbol.Width = 5;
            LineSymbol.Style = SimpleLineStyle.Solid;

            TextDrawsymbol = new TextSymbol();
            TextDrawsymbol.Text = "Text";
            TextDrawsymbol.Font = new SymbolFont("Arial", 15);


        }

        void _mapView_NavigationCompleted(object sender, EventArgs e)
        {
            //////////////Next and previous extents handling///////////////////
            if (_extentHistory.Count == 0)
            {
                _extentHistory.Add(_mapView.Extent.Clone());
                return;
            }

            if (_newExtent)
            {
                _currentExtentIndex++;

                if (_extentHistory.Count - _currentExtentIndex > 0)
                    _extentHistory.RemoveRange(_currentExtentIndex, (_extentHistory.Count - _currentExtentIndex));

                _extentHistory.Add(_mapView.Extent.Clone());

                if (NavigateExtentDoneEvent != null)
                {
                    NavigateExtentDoneEventArgs args = new NavigateExtentDoneEventArgs();
                    args.navigateExtentOn = NavigateExtentFocus.Both;
                    args.nextEnabled = false;
                    args.previousEnabled = true;

                    NavigateExtentDoneEvent(this, args);
                }
            }
            else
            {
                _newExtent = true;
            }
           
        }

        void _mapView_ExtentChanged(object sender, EventArgs e)
        {

           

            ////////////////////////////////////////////////////////////////////////
        }

        public void PrivousExtent()
        {
            if (_currentExtentIndex != 0)
            {
                _currentExtentIndex--;

                if (_currentExtentIndex == 0)
                {
                    if (NavigateExtentDoneEvent != null)
                    {
                        NavigateExtentDoneEventArgs args = new NavigateExtentDoneEventArgs();
                        args.navigateExtentOn = NavigateExtentFocus.Previous;
                        args.previousEnabled = false;

                        NavigateExtentDoneEvent(this, args);
                    }
                }

                _newExtent = false;

                _mapView.SetView(_extentHistory[_currentExtentIndex]);

                if (NavigateExtentDoneEvent != null)
                {
                    NavigateExtentDoneEventArgs args = new NavigateExtentDoneEventArgs();
                    args.navigateExtentOn = NavigateExtentFocus.Next;
                    args.nextEnabled = true;

                    NavigateExtentDoneEvent(this, args);
                }

            }

        }

        public void NextExtent()
        {
            if (_currentExtentIndex < _extentHistory.Count - 1)
            {
                _currentExtentIndex++;

                if (_currentExtentIndex == (_extentHistory.Count - 1))
                {

                    if (NavigateExtentDoneEvent != null)
                    {
                        NavigateExtentDoneEventArgs args = new NavigateExtentDoneEventArgs();
                        args.navigateExtentOn = NavigateExtentFocus.Next;
                        args.nextEnabled = false;

                        NavigateExtentDoneEvent(this, args);
                    }
                }

                _newExtent = false;


                _mapView.SetView(_extentHistory[_currentExtentIndex]);

                if (NavigateExtentDoneEvent != null)
                {
                    NavigateExtentDoneEventArgs args = new NavigateExtentDoneEventArgs();
                    args.navigateExtentOn = NavigateExtentFocus.Previous;
                    args.previousEnabled = true;

                    NavigateExtentDoneEvent(this, args);
                }
            }


        }

        public void SetDrawMode(GeoDrawMode mode)
        {
            //_drawMode = mode;

            //switch (mode)
            //{ 
            //    case GeoDrawMode.None:
            //        _drawControl.SetDrawMode(DrawMode.None);
            //        break;
            //    case GeoDrawMode.Point:
            //    case GeoDrawMode.Text:
            //        _drawControl.SetDrawMode(DrawMode.Point);
            //        break;
            //    case GeoDrawMode.Line:
            //        _drawControl.SetDrawMode(DrawMode.Polyline);
            //        break;
            //    case GeoDrawMode.Polygon:
            //        _drawControl.SetDrawMode(DrawMode.Polygon);
            //        break;
            //}
        }

        public void StopDrawControl()
        {
            //_drawControl.DisableDrawControl();
        }

       

        public void FullExtent()
        {
            _mapView.SetViewAsync(_mapView.Map.Layers[0].FullExtent);
        }

        public void EnableMyLocation()
        {
            _mapView.LocationDisplay = new LocationDisplay();
            _mapView.LocationDisplay.IsEnabled = true;
            _mapView.LocationDisplay.AutoPanMode = AutoPanMode.Default;

            _mapView.LocationDisplay.LocationProvider = new SystemLocationProvider();
        }

        public void ClearGraphicsLayer()
        {
            _myLocationLayer.Graphics.Clear();
            _redliningGraphicsLayer.Graphics.Clear();
        }

        public void EnableAutoNav()
        {
            _autoNavigation = new TiltNavigation(_mapView);

            _autoNavigation.PanToEvent += _autoNavigation_PanToEvent;
            _autoNavigation.Start();
        }

        public void SetAutoNavBase()
        {
            if (_autoNavigation != null)
                _autoNavigation.Start();
        }

        public void DisableAutoNav()
        {
            if (_autoNavigation != null)
            {
                _autoNavigation.Stop();

                _autoNavigation.PanToEvent -= _autoNavigation_PanToEvent;
                _autoNavigation = null;
            }
        }



        /////////////////////////////////Private Methods/////////////////////////////
        //void _drawControl_DrawCompletedEvent(object sender, DrawEventArgs e)
        //{
        //    Graphic graphic = new Graphic();

        //    if (e.Geometry != null)
        //        graphic.Geometry = e.Geometry;

        //    if (_drawMode == GeoDrawMode.Point && e.Geometry is MapPoint)
        //        graphic.Symbol = PointMarkerSymbol;
        //    else if (_drawMode == GeoDrawMode.Text && e.Geometry is MapPoint)
        //        graphic.Symbol = TextDrawsymbol;
        //    else if (_drawMode == GeoDrawMode.Line && e.Geometry is Polyline)
        //        graphic.Symbol = LineSymbol;
        //    else if (_drawMode == GeoDrawMode.Polygon && e.Geometry is Polygon)
        //        graphic.Symbol = PolygonFillSymbol;

        //    _redliningGraphicsLayer.Graphics.Add(graphic);
        //}

      
        void _autoNavigation_PanToEvent(object sender, string direction)
        {
            if (PanToChangedEvent != null)
                PanToChangedEvent(this, direction);
        }

        Envelope GetCenterExtent(MapPoint point)
        {
            Envelope extent = new Envelope();

            if (_mapView.Map.Layers[0] == null)
                return extent;

            double XRatio = Math.Abs((_mapView.Map.Layers[0].FullExtent as Envelope).XMax / 100000);
            double YRatio = Math.Abs((_mapView.Map.Layers[0].FullExtent as Envelope).YMax / 100000);
            double xMinNew = point.X - XRatio;
            double xMaxNew = point.X + XRatio;
            double yMinNew = point.Y - YRatio;
            double yMaxNew = point.Y + YRatio;

            extent = new Envelope(xMinNew, yMinNew, xMaxNew, yMaxNew);

            return extent;
        }
    }
}
