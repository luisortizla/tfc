
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
using Com.Mapbox.Geojson;
using Com.Mapbox.Mapboxsdk;
using Com.Mapbox.Mapboxsdk.Maps;
using Com.Mapbox.Services.Android.Navigation.V5.Navigation;
using Com.Mapbox.Services.Api.Directions.V5.Models;
using Xamarin.Essentials;
using static Android.Support.V7.View.ActionMode;


namespace TFCurier.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MapaNavegacionPage : AppCompatActivity, IOnMapReadyCallback
    {
        private MapView mapView;
        private MapboxMap mapboxMap;
        private DirectionsRoute currentRoute;
        public static readonly string LATUSR = "LATITUDUSUARIO";
        public static readonly string LONGUSR = "LONGITUDUSUARIO";

        private static  string  TAF = "DirectionsActivity";
//private NavigationMapRoute navigationMapRoute;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            string x = "sk.eyJ1IjoibHVpc29ydGl6cyIsImEiOiJja2RkbWJ5cWExNnhoMnlwY3hwZXV6ZTE1In0.zdLAf030Z_wllV9R0WSwbw";
            
            Mapbox.GetInstance(this, x);
            SetContentView(Resource.Layout.MapaNavegacionLayout);
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
            var lng = Intent.GetStringExtra(LONGUSR);
            var lat = Intent.GetStringExtra(LATUSR);
            double latituduser = Convert.ToDouble(lat);
            double longituduser = Convert.ToDouble(lng);
            Point origin = Point.FromLngLat(longituduser, latituduser);
            var location = await Geolocation.GetLastKnownLocationAsync();
            Point destination = Point.FromLngLat(-location.Longitude, location.Latitude);

            var response = await NavigationRoute
                .GetBuilder()
                .AccessToken(Mapbox.AccessToken)
                .Origin(origin)
                .Destination(destination)
                .Build()
                .GetRouteAsync();

            
        }

           }
}

