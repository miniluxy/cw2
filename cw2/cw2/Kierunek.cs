using System;
using Newtonsoft.Json;

namespace cw2 { 
    [Serializable]
    public class Kierunek
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("type")]
        public string mode { get; set; }

        public override string ToString()
        {
            return "Name: " + name + " tryb: " + mode;

        }
    }
}