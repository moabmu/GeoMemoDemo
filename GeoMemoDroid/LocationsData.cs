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
using System.IO;
using Newtonsoft.Json;

namespace GeoMemoDroid
{
    public class LocationsData
    {
        private static LocationsData _instance;
        public List<Location> Locations { get; protected set; }

        public static LocationsData Instance
        {
            get
            {
                //if (_instance == null)
                //    _instance = new LocationsData();
                //return _instance;

                return _instance ?? (_instance = new LocationsData());
            }
        }

        private LocationsData()
        {
            if (!File.Exists(GetFilename()))
            {
                Locations = new List<Location>();
                SeedLocations();
                File.WriteAllText(GetFilename(), JsonConvert.SerializeObject(Locations));
            }
            string serializedLocations = File.ReadAllText(GetFilename());
            Locations = JsonConvert.DeserializeObject<List<Location>>(serializedLocations);

        }

        private void SeedLocations()
        {
            Locations.Add(new Location()
            {
                Id = "1",
                Name = "Indian Ocean Restaurant",
                Description = "The most perfect indian restaurant in the city, also has chinese dishes",
                Phone = "+966143009876",
                Latitude = 24.805842,
                Longitude = 46.716657
            });

            Locations.Add(new Location()
            {
                Id = "2",
                Name = "Microsoft Office Riyadh",
                Description = "Microsoft Arabia Office in Riyadh",
                Phone = "+966112988555",
                Latitude = 24.805842,
                Longitude = 46.716657
            });

            Locations.Add(new Location()
            {
                Id = "3",
                Name = "Majed's house",
                Description = "In Albander district",
                Phone = "+966143009876",
                Latitude = 24.805842,
                Longitude = 46.716657
            });
        }

        private string GetFilename()
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Locations.json");
        }

        public void Save(Location location)
        {
            if(Locations != null)
            {
                if (!Locations.Contains(location))
                    Locations.Add(location);
                File.WriteAllText(GetFilename(), JsonConvert.SerializeObject(Locations));
            }
        }

        public  void Delete(Location location)
        {
            if(Locations != null)
            {
                Locations.Remove(location);
                File.WriteAllText(GetFilename(), JsonConvert.SerializeObject(Locations));
            }
        }
    }
}