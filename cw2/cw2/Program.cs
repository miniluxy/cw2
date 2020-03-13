using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;


namespace cw2{
    class Program{
        static void Main(string[] args){
            createLog();           

            string path = @"data.csv";
            string save = @"żesult.xml";        //literówka?
            string format = "xml";
            try{
                path = args[0];
            }
            catch (IndexOutOfRangeException){
                Log("Standarsowe wejście: " + path);
            }
            try{
                save = args[1];
            }
            catch (IndexOutOfRangeException){
                Log("Standartowe wyjście: " + save);
            }
            try{
                format = args[2];
            }
            catch (IndexOutOfRangeException){
                Log("Standardowy format: " + format);
            }
            var hash = new HashSet<Student>(new MojComparator());
            try{
                using (var stream = new StreamReader(path)){
                    string line = null;
                    while ((line = stream.ReadLine()) != null){
                        string[] read = line.Split(',');

                        bool b = true;
                        foreach (var tmp in read)
                            if (tmp.Equals("")){
                                Log("Wartość NULL");
                                b = false;
                            }
                        if (b)
                            if (read.Length == 9){
                                var readStudies = new Kierunek { name = read[2], mode = read[3] };
                                var st = new Student { imie = read[0], lastName = read[1],
                                    indexNumber = read[4], birthdate = read[5], email = read[6],
                                    momName = read[7], dadName = read[8], studies = readStudies };
                                hash.Add(st);
                            }
                            else
                                Log("Niepoprawne dane. Wyszukaj: " + read.Length + " Potrzebne: 9");
                    }
                }
            }
            catch (FileNotFoundException){
                Log("Nazwa nie istnieje");
            }
            
            Dictionary<string, int> activeStudnets = new Dictionary<string, int>();
            foreach (var s in hash){
                if (!activeStudnets.ContainsKey(s.studies.name))
                    activeStudnets.Add(s.studies.name, 1);
                else
                    activeStudnets[s.studies.name]++;
            }

            if (format.Equals("xml")){
                XmlSerializer serializer = new XmlSerializer(typeof(HashSet<Student>),
                new XmlRootAttribute(@"Utworzono: " + DateTime.Now + new XmlRootAttribute("Author: Thanh Cong Pham Trong")));
                FileStream writer = new FileStream(save + ".xml", FileMode.Create);
                serializer.Serialize(writer, hash);
            }
        
            if (format.Equals("json")){
                Uniwersytet university = new Uniwersytet(hash, activeStudnets);

                var js = JsonConvert.SerializeObject(university, Formatting.Indented);
                File.WriteAllText(save + ".json", js);
            }
        }

        public static void createLog(){
            StreamWriter log = new StreamWriter(@"łog.txt");        //chyba nie literówka?...
            log.Close();
        }
        public static void Log(string msg){
            StreamWriter log = new StreamWriter(@"łog.txt", append: true);
            log.WriteLine("[" + DateTime.Now + "] " + msg);
            log.Flush();
            log.Close();
        }
    }
}