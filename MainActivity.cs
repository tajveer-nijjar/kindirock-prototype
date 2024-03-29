﻿using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace GridLayoutDemo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private List<int> _data;
        public event EventHandler<RecyclerAdapterClickEventArgs> ItemClick;

        public MainActivity()
        {
            _data = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            

            // Get our button from the layout resource,
            // and attach an event to it
            RecyclerView recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            var spanCount = IsTablet(this) ? 2 : 1;
            var gridLayoutManager = new GridLayoutManager(this, spanCount: spanCount);
            recyclerView.SetLayoutManager(gridLayoutManager);

            recyclerView.HasFixedSize = true;
            var adapter = new RecyclerAdapter(_data);
            recyclerView.SetAdapter(adapter);
            adapter.ItemClick += (e, args) =>
            {
                Toast.MakeText(this, args.Position.ToString(), ToastLength.Short).Show();
            };

            var surfaceOrientation = WindowManager.DefaultDisplay.Rotation;

            if (surfaceOrientation == SurfaceOrientation.Rotation90 || surfaceOrientation == SurfaceOrientation.Rotation270)
            {
                if (IsTablet(this))
                {
                    spanCount = 3;
                }
                else
                {
                    spanCount = 2;
                }

                gridLayoutManager.SpanCount = spanCount;
                adapter.NotifyDataSetChanged();
            }
        }

        private bool IsTablet(Context context)
        {
            Display display = ((Activity)context).WindowManager.DefaultDisplay;
            DisplayMetrics displayMetrics = new DisplayMetrics();
            display.GetMetrics(displayMetrics);

            var wInches = displayMetrics.WidthPixels / (double)displayMetrics.DensityDpi;
            var hInches = displayMetrics.HeightPixels / (double)displayMetrics.DensityDpi;

            double screenDiagonal = Math.Sqrt(Math.Pow(wInches, 2) + Math.Pow(hInches, 2));
            return (screenDiagonal >= 6.0);
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}