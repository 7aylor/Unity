using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendNames : MonoBehaviour {

    public static string[] friends =
    {
        "Krassenstein",
        "Rockzom",
        "rettisawesome",
        "mattyk1ns",
        "MySecretWeapon",
        "KatalystKreations",
        "Geki",
        "DoctorArgus",
        "Crunchlaw",
        "IsorGames",
        "StudioFortis",
        "mikeyTshirt",
        "ApothicTV",
        "Juan",
        "JaydogDev",
        "chaotic development",
        "TheYagich01",
        "GalacticGlum",
        "InfamousCupcake",
        "HonestDan",
        "Indie_Mike",
        "LillyByte",
        "Joshachu",
        "juliantoker",
        "StarlightSkyes",
        "PatrickRMC"

    };

    public static List<string> namesInUse = new List<string>();

    public static string GetRandomFriendName()
    {
        int index = Random.Range(0, friends.Length);

        while (namesInUse.Contains(friends[index]))
        {
            index = Random.Range(0, friends.Length);
        }

        namesInUse.Add(friends[index]);

        return friends[index];
    }

    public static void ClearCurrentNameList()
    {
        namesInUse.Clear();
    }
}
