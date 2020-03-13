
using System;
using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json;

namespace cw2
{
    [Serializable]
    public class Uniwersytet
    {
        [JsonProperty("Autor:")] public String author = "Thanh Cong Pham Trong";

        [JsonProperty("Utworzone: ")] public String date;

        [JsonProperty("Studenci:")] public HashSet<Student> students;

        [JsonProperty("Aktywne kierunki: ")] public Dictionary<string, int> asc;

        public Uniwersytet(HashSet<Student> students, Dictionary<string, int> asc)
        {
            date = DateTime.Now.ToString("dd.MM.yyyy");

            this.students = students;
            this.asc = asc;



        }


    }
}