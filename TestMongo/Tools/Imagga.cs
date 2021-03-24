using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace TestMongo.Tools
{
    public class Imagga
    {
        public static async Task<bool> ImgCheckAsync(string imageUrl)//static async Task RunAsync
        {
            string apiKey = "acc_90c468681f354c8";
            string apiSecret = "d140a997c3e7a58dd734dfd73bf7adc9";
            //string imageUrl = "https://assets.marthastewart.com/styles/wmax-300/d33/vanilla-icecream-0611med107092des/vanilla-icecream-0611med107092des_vert.jpg?itok=_pkpvvKV"; //picture for example of wrong picture  https://purepng.com/public/uploads/large/purepng.com-mercedesmercedes-benzmercedesmercedes-luxury-vehiclesbusescoachestrucks-1701527525213re9ql.png
            // string imageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR5UeD0EH6Ew4zIq9m2ppQIpweqJdTFJIJwVwEvD2MOGbXuDeOF&s";
            string basicAuthValue = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", apiKey, apiSecret)));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.imagga.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(@"application/json"));
                client.DefaultRequestHeaders.Add("Authorization", String.Format("Basic {0}", basicAuthValue));

                HttpResponseMessage response = await client.GetAsync(String.Format("tags?image_url={0}{1}", "https:/", imageUrl));
                HttpContent content = response.Content;
                string result = await content.ReadAsStringAsync();

                JObject jobject = JObject.Parse(result);

                var selectedTages = (from t in jobject["result"]["tags"]
                                     select new { confidence = (double)t["confidence"], tag = (string)t["tag"]["en"] });


                double chance = 0;
                List<string> tags = new List<string>() { "ice", "dessert", "delicious", "sweet", "treat", "snack", "food", "cold", "cream", "cone" };
                foreach (var item in selectedTages)
                {
                    if (item.tag == "ice cream" && item.confidence >= 50)
                    {
                        return true;
                        //Console.WriteLine("True, this is an ice cream!");
                        //Environment.Exit(0);
                    }
                    //dessert,delicious
                    if (tags.Contains(item.tag) && item.confidence >= 50)
                        chance += item.confidence;
                }
                if (chance >= 90)
                    return true;
                else
                    return false;
                // Console.WriteLine("Come on! This is not an ice cream at all!");
                //return "no Idea!!";
            }
        }
    }
}