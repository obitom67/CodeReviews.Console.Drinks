﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static Drinks.DrinksType;

namespace Drinks
{
    internal class DrinkInfo
    {
        public async void GetDrinkInfo(string userDrink)
        {
            var options = new RestClientOptions("https://www.thecocktaildb.com/api/json/v1/1");
            var client = new RestClient(options);
            var request = new RestRequest($"/search.php?s={userDrink}");
            var drinksJson = client.Get<JsonObject>(request);

            DrinksType.Root root = JsonConvert.DeserializeObject<Root>(drinksJson.ToJsonString());

            var drinkType = root.drinks.GetType();
            foreach(var prop in root.drinks[0].GetType().GetProperties())
            {
                var printName = prop.Name;
                var printValue = prop.GetValue(root.drinks[0], null);
                if (printValue != null)
                {
                    AnsiConsole.WriteLine($"{prop.Name}:{prop.GetValue(root.drinks[0], null)}");
                }
                
            }


        }
    }
}