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
    public class LocationsAdapter : BaseAdapter<Location>
    {
        private Activity _context;
        public LocationsAdapter(Activity context)
        {
            _context = context;
        }

        public override Location this[int position]
        {
            get
            {
                return LocationsData.Instance.Locations[position];
            }
        }

        public override int Count
        {
            get
            {
                return LocationsData.Instance.Locations.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = _context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = LocationsData.Instance.Locations[position].Name;
            return view;
        }
    }
}