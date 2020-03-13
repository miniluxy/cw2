using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace cw2{
    public class Program{
        static HashSet<Student> students = new HashSet<Student>(new MojComparator());
        static StreamWriter strW = new StreamWriter("@łog.txt");

        static void Main(string[] args){
            try{

                var path = "data.csv";
                string savePath = "żezult.xml";
                string format = "xml";
                var lines = File.ReadLines(path);
                var today = DateTime.Now;
                foreach (var line in lines){
                    var words = line.Split(',');
                    if (words.Length == 9){
                        if (!Array.Exists(words, element => element == "")){
                            Student tmp = new Student();
                            Kierunek stud = new Kierunek();
                            stud.name = words[2];
                            stud.mode = words[3];
                            tmp.imie = words[0];
                            tmp.lastName = words[1];
                            tmp.studies = stud;
                            tmp.indexNumber = "s" + words[4];
                            tmp.birthdate = words[5];
                            tmp.email = words[6];
                            tmp.momName = words[7];
                            tmp.dadName = words[8];
                            students.Add(tmp);
                        }
                        else{
                            strW.WriteLine("Puste miejsca w student " + words[4] + " rekord");
                        }
                    }
                    else{
                        strW.WriteLine("Zła notacje w student " + words[4] + "rekord");
                    }
                }
                strW.Close();

                Dictionary<String, int> activeStdCount = new Dictionary<string, int>();
                foreach (var tmp in students){
                    if (!activeStdCount.ContainsKey(tmp.studies.name)){
                        activeStdCount.Add(tmp.studies.name, 1);
                    }
                    else{
                        activeStdCount[tmp.studies.name]++;
                    }

                    if (format.Equals("xml")){
                        Console.WriteLine("xml");
                        XDocument doc = new XDocument(new XElement("university",
                            new XAttribute("createdAt", today),
                            new XAttribute("author", "Vsevolod Doroshenko"),
                            new XElement("studenci",
                                from student in students
                                select new XElement("student",
                                    new XAttribute("indexNumber", student.indexNumber),
                                    new XElement("name", student.imie),
                                    new XElement("secondName", student.lastName),
                                    new XElement("birthdate", student.birthdate),
                                    new XElement("email", student.email),
                                    new XElement("mothersName", student.momName),
                                    new XElement("fathersName", student.dadName),
                                    new XElement("studies",
                                        new XElement("name", student.studies.name),
                                        new XElement("mode", student.studies.mode)
                                    ))),
                            new XElement("Aktywne Kierunki",
                                from asc in activeStdCount
                                select new XElement("Kierunki",
                                    new XAttribute("Nazwa", asc.Key),
                                    new XAttribute("numberOfStudents", asc.Value)
                                ))));
                        doc.Save(savePath + ".xml");
                    }
                    else if (format.Equals("json")){
                        Console.WriteLine("Json");
                        Uniwersytet uni = new Uniwersytet(students, activeStdCount);

                        var json = JsonConvert.SerializeObject(uni, (Newtonsoft.Json.Formatting)Formatting.Indented);
                        File.WriteAllText(savePath + ".json", json);
                    }
                    else{
                        Console.WriteLine("Zły format");
                    }
                }
            }
            catch (FileNotFoundException){
                Console.WriteLine("Nazwa pliku nie istnieje");
                throw;
            }
            catch (ArgumentException){
                Console.WriteLine("Ściezka niepoprawna");
                throw;
            }
        }

    }
}