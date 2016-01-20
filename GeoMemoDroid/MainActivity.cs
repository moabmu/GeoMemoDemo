using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace GeoMemoDroid
{
    [Activity(Label = "GeoMemoDroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //int count = 1;
        LocationsAdapter _adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            #region Commented-out code
            //// Get our button from the layout resource,
            //// and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            #endregion

            _adapter = new LocationsAdapter(this);
            FindViewById<ListView>(Resource.Id.locationsListView).Adapter = _adapter;
            FindViewById<ListView>(Resource.Id.locationsListView).ItemClick += LocationsListView_ItemClick;
        }

        protected override void OnResume()
        {
            base.OnResume();
            _adapter.NotifyDataSetChanged();
        }

        protected void LocationsListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(DetailActivity));
            intent.PutExtra("locationid", _adapter[e.Position].Id);
            StartActivity(intent);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.MainMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.actionNew:
                    // Navigate to Edit activity
                    StartActivity(typeof(EditActivity));
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}

