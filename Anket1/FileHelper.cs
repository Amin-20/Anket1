using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Anket1.Form1;

namespace Anket1
{
    public class FileHelper
    {
        public static void WriteJsonHuman(Human human)
        {
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter($"{human.Name}.json"))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Newtonsoft.Json.Formatting.Indented;
                    serializer.Serialize(jw, human);
                }
            }
        }

        public static Human ReadJsonHuman(string fileName)
        {
            Human person = new Human();
            var context = File.ReadAllText(fileName);
            person = JsonConvert.DeserializeObject<Human>(context);
            return person;
        }
    }
}
