using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HandType {

    public static List<string> ActiveHandType = new List<string>{ "Apple", "Beans", "Broccoli", "Bulldozer", "Cauliflower", "Corn", "House", "Leek", "Onion", "Pumpkin", "Strawberry", "Well", "Wheat" };
    public static List<string> PassiveHandType = new List<string> { "Bank", "Family", "Irrigation", "Fertilizer" };
    public static Dictionary<string, string> Crops = new Dictionary<string, string> { { "Apple", "Fall" }, {"Beans", "Summer"}, { "Broccoli", "Winter" }, { "Cauliflower", "Spring"}, { "Corn", "Fall"}, { "Leek", "Winter"}, { "Onion", "Summer"}, { "Pumpkin", "Fall"}, { "Strawberry", "Spring"}, { "Wheat", "Summer"} };

}