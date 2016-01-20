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
    [Activity(Label = "EditActivity")]
    public class EditActivity : Activity
    {
        Location _location;
        EditText _nameEditText;
        EditText _descrEditText;
        EditText _phoneEditText;
        EditText _latEditText;
        EditText _longEditText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.Edit);

            if (Intent.HasExtra("locationid"))
            {
                string locationId = Intent.GetStringExtra("locationid");
                _location = LocationsData.Instance.Locations.FirstOrDefault(x => x.Id == locationId);
            }
            else
                _location = new Location();

            _nameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
            _descrEditText = FindViewById<EditText>(Resource.Id.descrEditText);
            _phoneEditText = FindViewById<EditText>(Resource.Id.phoneEditText);
            _latEditText = FindViewById<EditText>(Resource.Id.latEditText);
            _longEditText = FindViewById<EditText>(Resource.Id.longEditText);

            LocationToUI();

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.EditMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnResume()
        {
            base.OnResume();
            LocationToUI();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Resource.Id.actionSave:
                    SaveLocation();
                    return true;
                case Resource.Id.actionDelete:
                    DeleteLocation();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected void DeleteLocation()
        {
            LocationsData.Instance.Delete(_location);

            Intent returnIntent = new Intent();
            Intent.PutExtra("locationdeleted", true);
            SetResult(Result.Ok, returnIntent);
            Finish();
        }

        protected void SaveLocation()
        {
            UIToLocation();
            LocationsData.Instance.Save(_location);

            Intent returnIntent = new Intent();
            Intent.PutExtra("locationdeleted", false);
            SetResult(Result.Ok, returnIntent);
            Finish();
        }

        protected void LocationToUI()
        {
            _nameEditText.Text = _location.Name;
            _descrEditText.Text = _location.Description;
            _phoneEditText.Text = _location.Phone;
            _latEditText.Text = _location.Latitude.ToString();
            _longEditText.Text = _location.Longitude.ToString();
        }

        protected void UIToLocation()
        {
            _location.Name = _nameEditText.Text;
            _location.Description = _descrEditText.Text;
            _location.Phone = _phoneEditText.Text;

            if (!string.IsNullOrEmpty(_latEditText.Text))
                _location.Latitude = Double.Parse(_latEditText.Text);
            else
                _location.Latitude = null;

            if (!string.IsNullOrEmpty(_longEditText.Text))
                _location.Longitude = Double.Parse(_longEditText.Text);
            else
                _location.Longitude = null;
        }
    }
}