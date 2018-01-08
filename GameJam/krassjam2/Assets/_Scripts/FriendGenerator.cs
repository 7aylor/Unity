//using System;

//using UnityEngine;

//public class FriendGenerator : MonoBehaviour
//{
//    public static Friend[] allMyFriends;
//    public TextAsset jsonFile;

//    private void Awake()
//    {
//        allMyFriends = JsonConvert.DeserializeObject<Friend[]>(jsonFile.text);
//    }

//    public static string GetRandomFriend()
//    {
//        return allMyFriends[UnityEngine.Random.Range(0, allMyFriends.Length)].channel;
//    }
//}

//[Serializable]
//public struct Friend
//{
//    public string channel;
//}