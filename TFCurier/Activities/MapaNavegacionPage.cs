using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;

using Android.OS;

using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using Com.Mapbox.Mapboxsdk;

using Com.Mapbox.Mapboxsdk.Camera;

using Com.Mapbox.Mapboxsdk.Geometry;
using Com.Mapbox.Mapboxsdk.Maps;


namespace TFCurier.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    //public class MapaNavegacionPage : AppCompatActivity
    public class MapaNavegacionPage : AppCompatActivity, IOnMapReadyCallback
    {
        MapView mapView = null;
        MapboxMap mapbox = null;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            string x = "pk.eyJ1IjoibHVpc29ydGl6cyIsImEiOiJja2RkYWYxaDczd282Mnlxdjlyc3gxa2hxIn0.7tciDRw16zViA-0ESUtLWA";
            Mapbox.GetInstance(this, x);
            SetContentView(Resource.Layout.MapaNavegacionLayout);
            mapView = FindViewById<MapView>(Resource.Id.mapView);
            //mapView = new MapView(this);
            mapView.OnCreate(savedInstanceState);
            mapView.GetMapAsync(this);

                 }

        public void OnMapReady(MapboxMap mapbox)
        {
            this.mapbox = mapbox;
            mapbox.SetStyle("mapbox://styles/luisortizs/ckdeotksa59vs1imw35jiqemz");

            double ltd = 14.911008;
            double lng = -92.257768;

            var position = new CameraPosition.Builder()
                           .Target(new LatLng(ltd, lng))
                           .Zoom(13)
                           .Build();

            mapbox.AnimateCamera(CameraUpdateFactory.NewCameraPosition(position));
            
        }

        protected override void OnStart()
        {
            base.OnStart();
            mapView?.OnStart();
        }

        protected override void OnResume()
        {
            base.OnResume();
            mapView?.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();
            mapView?.OnPause();
        }

        protected override void OnStop()
        {
            base.OnStop();
            mapView?.OnStop();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            mapView?.OnSaveInstanceState(outState);
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            mapView?.OnLowMemory();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            mapView?.OnDestroy();
        }
    }
       /* private MapView mapView;
        private MapboxMap mapboxMap;
        private Mapbox mapbox;
       // private NavigationMapRoute mapRoute; 
        public static readonly string LATUSR = "LATITUDUSUARIO";
        public static readonly string LONGUSR = "LONGITUDUSUARIO";
        private PermissionsManager permissionsManager;
       // private LocationComponent locationComponent;
        
        private DirectionsRoute currentRoute;
        private static string TAG = "DirectionsActivity";

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
             Xamarin.Essentials.Platform.Init(this, savedInstanceState);
             string x = "sk.eyJ1IjoibHVpc29ydGl6cyIsImEiOiJja2RkbWJ5cWExNnhoMnlwY3hwZXV6ZTE1In0.zdLAf030Z_wllV9R0WSwbw";

           
             SetContentView(Resource.Layout.MapaNavegacionLayout);
            Mapbox.GetInstance(this, x);
           mapView = FindViewById<MapView>(Resource.Id.mapView);
           mapView.OnCreate(savedInstanceState);
           mapView.GetMapAsync(this);
            
            BuildRoute();
        }

        

        public void OnMapReady(MapboxMap mapboxMap)
           {
               this.mapboxMap = mapboxMap;
               mapboxMap.SetStyle("mapbox://styles/luisortizs/ckdeotksa59vs1imw35jiqemz");
            
        }

        async void BuildRoute()
        {
            MapboxNavigation navigation = new MapboxNavigation(this, "sk.eyJ1IjoibHVpc29ydGl6cyIsImEiOiJja2RkbWJ5cWExNnhoMnlwY3hwZXV6ZTE1In0.zdLAf030Z_wllV9R0WSwbw");
            //var lng = Intent.GetStringExtra(LONGUSR);
            //var lat = Intent.GetStringExtra(LATUSR);
            //double latituduser = Convert.ToDouble(lat);
            //double longituduser = Convert.ToDouble(lng);
            //Point origin = Point.FromLngLat(longituduser, latituduser);
            Point origin = Point.FromLngLat(-92.300546, 14.875183);
            //var location = await Geolocation.GetLastKnownLocationAsync();
            Point destination = Point.FromLngLat(-92.257768, 14.911008);

            var response = await NavigationRoute
                .GetBuilder()
                .AccessToken(Mapbox.AccessToken)
                .Origin(origin)
                .Destination(destination)
                .Build()
                .GetRouteAsync();

            System.Diagnostics.Debug.WriteLine(response);
        }
        protected override void OnStart()
        {
            base.OnStart();
            mapView?.OnStart();
        }

        protected override void OnResume()
        {
            base.OnResume();
            mapView?.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();
            mapView?.OnPause();
        }

        protected override void OnStop()
        {
            base.OnStop();
            mapView?.OnStop();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            mapView?.OnSaveInstanceState(outState);
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            mapView?.OnLowMemory();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            mapView?.OnDestroy();
        }
    }*/
}

    

