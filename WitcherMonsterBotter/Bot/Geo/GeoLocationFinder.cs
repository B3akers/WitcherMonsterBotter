using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Bot.Geo
{
    public class CityData
    {
        public string city { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string country { get; set; }
        public string iso2 { get; set; }
        public string admin_name { get; set; }
        public string capital { get; set; }
        public string population { get; set; }
        public string population_proper { get; set; }
    }

    public static class GeoLocationFinder
    {
        private readonly static Random _rng = new Random();
        private readonly static HttpClient _httpClient = new HttpClient();
        private readonly static Dictionary<string, List<CityData>> _countryCities = new Dictionary<string, List<CityData>>();

        private static List<CityData> GetCities(string country)
        {
            var countryCode = country.ToLower();
            if (countryCode == "as"
                || countryCode == "do"
                || countryCode == "in"
                || countryCode == "is")
                countryCode = "_" + countryCode;

            if (!_countryCities.TryGetValue(country, out var cityList))
            {
                var resourceData = Properties.Resources.ResourceManager.GetObject(countryCode);
                if (resourceData == null)
                    cityList = new List<CityData>();
                else
                    cityList = JsonConvert.DeserializeObject<List<CityData>>(Encoding.UTF8.GetString((byte[])resourceData));

                _countryCities[country] = cityList;
            }

            return cityList;
        }

        public static async Task<Tuple<bool, double, double>> FindLocation(bool preferUrban, int[] allowedHours)
        {
            try
            {
                var utcTime = DateTime.UtcNow;

                List<string> allowedCountries = new();
                foreach (var timeCountry in _timezoneCountryMap)
                {
                    var spanTime = TimeSpan.FromMinutes(Math.Abs(timeCountry.Value) * 60f);
                    var localTime = timeCountry.Value >= 0f ? utcTime.Add(spanTime) : utcTime.Subtract(spanTime);

                    if (allowedHours.Contains(localTime.Hour))
                        allowedCountries.Add(timeCountry.Key);
                }

                if (allowedCountries.Count <= 0)
                    return new Tuple<bool, double, double>(false, 0.0, 0.0);

                if (preferUrban)
                {
                    var cities = GetCities(allowedCountries[_rng.Next(0, allowedCountries.Count)]);

                    if (cities.Count > 0)
                    {
                        var city = cities.OrderBy(x => x.population).FirstOrDefault();
                        return new Tuple<bool, double, double>(true, double.Parse(city.lat, CultureInfo.InvariantCulture), double.Parse(city.lng, CultureInfo.InvariantCulture));
                    }
                }

                var randomPlace = JObject.Parse(await _httpClient.GetStringAsync($"https://api.3geonames.org/?randomland={allowedCountries[_rng.Next(0, allowedCountries.Count)]}&json=1"));

                return new Tuple<bool, double, double>(true, (double)randomPlace["nearest"]["latt"], (double)randomPlace["nearest"]["longt"]);
            }
            catch
            {
                return new Tuple<bool, double, double>(false, 0.0, 0.0);
            }
        }

        private static readonly Dictionary<string, float> _timezoneCountryMap = new Dictionary<string, float>()
        {
            {"AF",4.5f},
            {"AL",2},
            {"DZ",1},
            {"AS",-11},
            {"AD",2},
            {"AO",1},
            {"AI",-4},
            {"AG",-4},
            {"AR",-3},
            {"AM",4},
            {"AW",-4},
            {"AT",2},
            {"AZ",4},
            {"BS",-4},
            {"BH",3},
            {"BD",6},
            {"BB",-4},
            {"BY",3},
            {"BE",2},
            {"BZ",-6},
            {"BJ",1},
            {"BM",-3},
            {"BT",6},
            {"BO",-4},
            {"BQ",-4},
            {"BA",2},
            {"BW",2},
            {"IO",6},
            {"BN",8},
            {"BG",3},
            {"BF",0},
            {"BI",2},
            {"KH",7},
            {"CM",1},
            {"CV",-1},
            {"KY",-5},
            {"CF",1},
            {"TD",1},
            {"CX",7},
            {"CC",6.5f},
            {"CO",-5},
            {"KM",3},
            {"CG",1},
            {"CK",-10},
            {"CR",-6},
            {"HR",2},
            {"CU",-4},
            {"CW",-4},
            {"CY",3},
            {"CZ",2},
            {"CI",0},
            {"DK",2},
            {"DJ",3},
            {"DM",-4},
            {"DO",-4},
            {"EG",2},
            {"SV",-6},
            {"GQ",1},
            {"ER",3},
            {"EE",3},
            {"ET",3},
            {"FK",-3},
            {"FO",1},
            {"FJ",12},
            {"FI",3},
            {"FR",2},
            {"GF",-3},
            {"TF",5},
            {"GA",1},
            {"GM",0},
            {"GE",4},
            {"DE",2},
            {"GH",0},
            {"GI",2},
            {"GR",3},
            {"GD",-4},
            {"GP",-4},
            {"GU",10},
            {"GT",-6},
            {"GG",1},
            {"GN",0},
            {"GW",0},
            {"GY",-4},
            {"HT",-4},
            {"VA",2},
            {"HN",-6},
            {"HK",8},
            {"HU",2},
            {"IS",0},
            {"IN",5.5f},
            {"IR",4.5f},
            {"IQ",3},
            {"IE",1},
            {"IM",1},
            {"IL",3},
            {"IT",2},
            {"JM",-5},
            {"JP",9},
            {"JE",1},
            {"JO",3},
            {"KE",3},
            {"KP",9},
            {"KR",9},
            {"KW",3},
            {"KG",6},
            {"LA",7},
            {"LV",3},
            {"LB",3},
            {"LS",2},
            {"LR",0},
            {"LY",2},
            {"LI",2},
            {"LT",3},
            {"LU",2},
            {"MO",8},
            {"MK",2},
            {"MG",3},
            {"MW",2},
            {"MY",8},
            {"MV",5},
            {"ML",0},
            {"MT",2},
            {"MH",12},
            {"MQ",-4},
            {"MR",0},
            {"MU",4},
            {"YT",3},
            {"MD",3},
            {"MC",2},
            {"ME",2},
            {"MS",-4},
            {"MA",1},
            {"MZ",2},
            {"MM",6.5f},
            {"NA",2},
            {"NR",12},
            {"NP",5.75f},
            {"NL",2},
            {"NC",11},
            {"NI",-6},
            {"NE",1},
            {"NG",1},
            {"NU",-11},
            {"NF",11},
            {"MP",10},
            {"NO",2},
            {"OM",4},
            {"PK",5},
            {"PW",9},
            {"PS",3},
            {"PA",-5},
            {"PY",-4},
            {"PE",-5},
            {"PH",8},
            {"PN",-8},
            {"PL",2},
            {"PR",-4},
            {"QA",3},
            {"RO",3},
            {"RW",2},
            {"RE",4},
            {"BL",-4},
            {"SH",0},
            {"KN",-4},
            {"LC",-4},
            {"MF",-4},
            {"PM",-2},
            {"VC",-4},
            {"WS",13},
            {"SM",2},
            {"ST",0},
            {"SA",3},
            {"SN",0},
            {"RS",2},
            {"SC",4},
            {"SL",0},
            {"SG",8},
            {"SX",-4},
            {"SK",2},
            {"SI",2},
            {"SB",11},
            {"SO",3},
            {"ZA",2},
            {"GS",-2},
            {"SS",2},
            {"LK",5.5f},
            {"SD",2},
            {"SR",-3},
            {"SJ",2},
            {"SZ",2},
            {"SE",2},
            {"CH",2},
            {"SY",3},
            {"TW",8},
            {"TJ",5},
            {"TZ",3},
            {"TH",7},
            {"TL",9},
            {"TG",0},
            {"TK",13},
            {"TO",13},
            {"TT",-4},
            {"TN",1},
            {"TR",3},
            {"TM",5},
            {"TC",-4},
            {"TV",12},
            {"UG",3},
            {"UA",3},
            {"AE",4},
            {"GB",1},
            {"UY",-3},
            {"UZ",5},
            {"VU",11},
            {"VE",-4},
            {"VN",7},
            {"VG",-4},
            {"VI",-4},
            {"WF",12},
            {"EH",1},
            {"YE",3},
            {"ZM",2},
            {"ZW",2},
            {"AX",3}
        };

    }
}
