﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace ConsumeREST
{
    /*
     * An exercise in parsing JSON from a web service
     * 
     * Awesome tool for converting JSON objects to C# classes
     *  http://json2csharp.com/
     *  
     * *note the converted classes will have incorrect variable names. to fix this read the following:
     * 
     * An important obstacle when converting JSON objects to C# classes you can not use - in a variable name
     * 
     *   Will not work 
     *         public int last-updated { get; set; }
     * 
     *   Works
     *         [JsonProperty("last-updated")]
     *         public int last_updated { get; set; }
     *         
     * 
     *  Try to create a MVC program and use the generated classes as the models. Then when parsing the JSON try
     *      var obj = JsonConvert.DeserializeObject<Data Contex class here>(t.getUser());
     *      
     *  time_played is a unix timestamp. Test results here http://www.unixtimestamp.com/index.php until I implement a conversion. 
     */
    class Program
    {
        
        private const string URL = "http://us.battle.net/api/d3/profile/";
        private string urlParameters = "Alkaizer-1727/";
        private string json;
        static void Main(string[] args)
        {
            Program t = new Program();

            // Just pass in a json string, it will do the rest. It Deserializes the object and assigns it to the correct classes, with in the context (DataContainer).
            var obj = JsonConvert.DeserializeObject<DataContainer>(t.getUser());
            //Console.WriteLine(t.getUser());
            Console.ReadLine();
            // Assign a debug break poing 
        }

        /*
         *  Should really return a JSON object, string works too.
         *  If the data is really large the method should return async or Task
         */
        public String getUser()
        {
            WebClient client = new WebClient();
            byte[] result = client.DownloadData("http://us.battle.net/api/d3/profile/Alkaizer-1727/");
            String json = Encoding.ASCII.GetString(result);
            return json;


        }

    }

    // To hold the "context" of all of the JSON objects from Blizzard's D3 Profile request
    public class DataContainer
    {
        public List<Hero> heroes { get; set; }
        public Stats stats { get; set; }
        public TimePlayed timePlayed { get; set; }
        public Kills kills { get; set; }
        public Progression progression { get; set; }
    }



    /*
     * Generated by http://json2csharp.com/
     * Had to edit some variable names due to -
     * The following are used to parse Blizzard's Diablo 3 Api.
     * http://blizzard.github.io/d3-api-docs/
     * http://us.battle.net/api/d3/profile/reorx-1616/
     * http://us.battle.net/api/d3/data/item/Co0BCJGGgegCEgcIBBXUuESJHUu1-UsdrLzTUx1mIwZQHZsGAMsdOydvJB2BgcbEMIsCOMQBQABIAVASWARgkQNqKAoMCAAQ78rmiYCAgOA0EhgI1cauHRIHCAQVhSeUsTCPAjgAQAGQAQCAAUaNAXv5Tl2lATsnbyStAeYV2w21AX_5Tl24Aamt5fQGwAFSGNec2usOUAJYAKAB15za6w6gAZee6uAOoAG8wJWsDw
     * http://us.battle.net/api/d3/profile/reorx-1616/hero/57540046
     */
    public class Hero
    {
        public int paragonLevel { get; set; }
        public bool seasonal { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public int level { get; set; }
        public bool hardcore { get; set; }
        public int gender { get; set; }
        public bool dead { get; set; }
        public string @class { get; set; }
        [JsonProperty("last-updated")]
        public int last_updated { get; set; }

        public override string ToString()
        {
            return this.paragonLevel + " " + this.level + " " + this.name;
        }
}
    public class Stats
    {
        public int life { get; set; }
        public double damage { get; set; }
        public double toughness { get; set; }
        public double healing { get; set; }
        public int armor { get; set; }
        public int strength { get; set; }
        public int dexterity { get; set; }
        public int vitality { get; set; }
        public int intelligence { get; set; }
        public int physicalResist { get; set; }
        public int fireResist { get; set; }
        public int coldResist { get; set; }
        public int lightningResist { get; set; }
        public int poisonResist { get; set; }
        public int arcaneResist { get; set; }
        public double damageIncrease { get; set; }
        public double critChance { get; set; }
        public double damageReduction { get; set; }
    }
    public class Kills
{
    public int monsters { get; set; }
    public int elites { get; set; }
    public int hardcoreMonsters { get; set; }
}

public class TimePlayed
{
    public double barbarian { get; set; }
    public double crusader { get; set; }
    [JsonProperty("demon-hunter")]
    public double demon_hunter { get; set; }
    public double monk { get; set; }
    [JsonProperty("witch-doctor")]
    public double witch_doctor { get; set; }
    public double wizard { get; set; }
}

public class Progression
{
    public bool act1 { get; set; }
    public bool act2 { get; set; }
    public bool act3 { get; set; }
    public bool act4 { get; set; }
    public bool act5 { get; set; }
}
}
