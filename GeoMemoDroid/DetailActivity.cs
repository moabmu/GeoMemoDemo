using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GeoMemoDroid
{
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : Activity
    {
        Location _location;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            this.SetContentView(Resource.Layout.Detail);

            if (Intent.HasExtra("locationid"))
            {
                string locationId = Intent.GetStringExtra("locationid");
                _location = LocationsData.Instance.Locations.FirstOrDefault(x => x.Id == locationId);
            }
            else
                _location = new Location();
            //LocationToUI();

            FindViewById<ImageButton>(Resource.Id.callButton).Click += callButton_Click;
        }

        protected override void OnResume()
        {
            base.OnResume();
            LocationToUI();
            FindViewById<ImageButton>(Resource.Id.callButton).Visibility = (_location.Phone == String.Empty) ? ViewStates.Gone : ViewStates.Visible;
        }

        private void callButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Intent.ActionDial, Android.Net.Uri.Parse(string.Format("tel:{0}", _location.Phone.ToString()))); //$"tel:{_location.Phone}"));
            StartActivity(intent);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.DetailMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.actionEdit:
                    Intent editIntent = new Intent(this, typeof(EditActivity));
                    editIntent.PutExtra("locationid", _location.Id);
                    StartActivityForResult(editIntent, 1);
                    return true;

                case Resource.Id.actionPhotos:

                    Intent urlIntent = new Intent(Intent.ActionView);
                    urlIntent.SetData(Android.Net.Uri.Parse(String.Format("http://www.bing.com/images/search?q={0}", _location.Name))); //$"http://www.bing.com/images/search?q={_location.Name}"));
                    StartActivity(urlIntent);
                    return true;

                case Resource.Id.actionDirections:

                    if ((_location.Latitude.HasValue) && (_location.Longitude.HasValue))
                    {
                        Intent mapIntent = new Intent(Intent.ActionView,
                        Android.Net.Uri.Parse(String.Format("geo:{0},{1}", _location.Latitude, _location.Longitude)));//$"geo:{_location.Latitude},{_location.Longitude}"));
                        StartActivity(mapIntent);
                    }
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }

        }

        protected void LocationToUI()
        {
            this.FindViewById<TextView>(Resource.Id.nameTextView).Text = _location.Name;
            this.FindViewById<TextView>(Resource.Id.descrTextView).Text = _location.Description;
            this.FindViewById<TextView>(Resource.Id.phoneTextView).Text = _location.Phone;
            this.FindViewById<TextView>(Resource.Id.latTextView).Text = _location.Latitude.ToString();
            this.FindViewById<TextView>(Resource.Id.longTextView).Text = _location.Longitude.ToString();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == 1 && resultCode == Result.Ok)
            {
                if (data.GetBooleanExtra("locationdeleted", false))
                    Finish();
            }
            else
                base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}