using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace aaProject.Tools
{
    public class Nutritions
    {
        public static string[] importantNutrients = { "Energy", "Protein", "Calcium", "Sugars, total" };
        public static double Calories;
        public static double Proteins;
        public static double Fats;
        public Nutritions()
        {
            Calories = 0;
            Proteins = 0;
            Fats = 0;
        }
        public static async Task NutAsync(string ProductId) //הרצת משימה
        {

            string url = "https://api.nal.usda.gov/ndb/V2/";


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(@"application/json"));

                HttpResponseMessage response = await client.GetAsync(string.Format("reports?ndbno={0}&type=f&format=json&api_key=Sg3Z65TcedXLaTlOFrhOHGtxfA6KfzZQEcZXoVv8", ProductId));

                HttpContent content = response.Content;
                string result = await content.ReadAsStringAsync();

                JObject jobject = JObject.Parse(result); // "JObject" was undefined untill I installed (nugs) "json".

                foreach (var item in jobject["foods"]) //אז תכלס הוא מבצע את הלולאה הזו-החיצונית פעם אחת בלבד, תכלס יש כאן רק "אוכל"-פוד אחד, מה שביקשנו
                {
                    string x = item["food"]["nutrients"][3]["value"].ToString();
                    Calories = Convert.ToDouble(item["food"]["nutrients"][1]["value"].ToString());
                    Proteins = Convert.ToDouble(item["food"]["nutrients"][3]["value"].ToString());
                    Fats = Convert.ToDouble(item["food"]["nutrients"][4]["value"].ToString());
                    //for (int i = 0; i < item["food"]["nutrients"].Count(); i++)
                    //{
                    //    if (importantNutrients.Contains(item["food"]["nutrients"][i]["name"].ToString()))
                    //    {
                    //        //Console.WriteLine(item["food"]["nutrients"][i]["name"]);
                    //        //Console.WriteLine(item["food"]["nutrients"][i]["unit"]);
                    //        //Console.WriteLine(item["food"]["nutrients"][i]["value"]);
                    //        Energy = 0;
                    //        Proteins = 0;
                    //        Fats = 0;
                    //    }
                    //}

                }

            }

        }

    }
}