using System;
using Newtonsoft.Json;

namespace cw2
{
    [Serializable]
    public class Student
    {
        [JsonProperty("imie")]
        public string imie { get; set; }
        [JsonProperty("lastName")]
        public string lastName { get; set; }
        [JsonProperty("studies")]
        public Kierunek studies { get; set; }
        [JsonProperty("indexNumber")]
        public string indexNumber { get; set; }
        [JsonProperty("birthday")]
        public string birthdate { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("momName")]
        public string momName { get; set; }
        [JsonProperty("dadName")]
        public string dadName { get; set; }

        public override string ToString()
        {
            return "Index: " 
                + indexNumber
                + "\nimię: " + imie
                + "\nNazwisko: " + lastName
                + "\nKierunek: " + studies
                + "\nData urodzenia: " + birthdate
                + "\nEmail: " + email
                + "\nImię matki: " + momName
                + "\nImię ojca: " + dadName
                + "\n";
        }
    }
}