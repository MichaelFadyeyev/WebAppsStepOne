using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebAppStepOne.Models;

namespace WebAppStepOne.Services
{
    public class WeatherService
    {
        string? location_name;
        string? region;
        string? country;
        string? local_time;
        readonly string apiUriCurrent = "http://api.weatherapi.com/v1/current.xml?key=8315c71fdcb74298a0c84902210307&q=";
        readonly string apiUriForecast = "http://api.weatherapi.com/v1/forecast.xml?key=8315c71fdcb74298a0c84902210307&q=";
        readonly string forecast_string = "&days=1&aqi=no&alerts=no";

        XElement? root;

        public void SetLocation(string location_name)
        {
            root = GetRoot(apiUriCurrent + location_name);

            this.location_name = location_name;

            region = GetProperty("location", "region");
            country = GetProperty("location", "country");
            local_time = GetProperty("location", "localtime");
        }

        public Location GetCurrent()
        {

            root = GetRoot(apiUriCurrent + location_name);

#pragma warning disable CS8601 // Possible null reference assignment.
            Location location = new Location()
            {
                Name = location_name,
                Region = region,
                Country = country,
                Time = local_time,

                TempC = GetProperty("current", "temp_c"),
                Wind = GetProperty("current", "wind_kph"),
                WindDirection = GetProperty("current", "wind_dir"),
                Cloud = GetProperty("current", "cloud"),
                Visibility = GetProperty("current", "vis_km"),
            };
#pragma warning restore CS8601 // Possible null reference assignment.

            return location;

        }


        private XElement? GetRoot(string locationUri)
        {
            WebClient client = new();
            byte[] data = client.DownloadData(locationUri);
            string content = Encoding.UTF8.GetString(data);
            XDocument doc = XDocument.Parse(content);
            return doc.Element("root");
        }

        public List<Location> GetForecast()
        {
            List<Location> forecast = new();


            //root = GetRoot(apiUriCurrent + location_name + forecast_string);
            root = GetRoot(apiUriForecast + location_name + forecast_string);
            List<XElement> hoursForecast = root.Element("forecast").Element("forecastday").Elements("hour").ToList();


            for (int t = 0; t < hoursForecast.Count; t += 3)
            {
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                root = hoursForecast[t];
                forecast.Add(new Location()
                {
                    Name = location_name,
                    Region = region,
                    Country = country,

                    Time = GetTime(root.Element("time").Value),
                    TempC = root.Element("temp_c").Value,
                    Wind = root.Element("wind_kph").Value,
                    WindDirection = root.Element("wind_dir").Value,
                    Cloud = root.Element("cloud").Value,
                    Visibility = root.Element("vis_km").Value
                });
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8601 // Possible null reference assignment.
            }


            return forecast;
        }

        string GetProperty(string parent, string target)
        {
            return root.Element(parent).Element(target).Value;
        }

        string GetTime(string time)
        {
            string time_reg = @"\d{2}:\d{2}$";
            if (!Regex.IsMatch(time, time_reg)) return time;
            return Regex.Match(time, time_reg).ToString();

        }






    }
}
