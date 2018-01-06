using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsDeck : MonoBehaviour {

    public GameObject[] possibleFriends;
    private List<int> spawnedFriendsIndex = new List<int>();

	// Use this for initialization
	void Start () {
        InitFriendDeck();
	}

    public void InitFriendDeck()
    {
        ClearPlaySpace();
        for(int i = 0; i < 5; i++)
        {
            SpawnUniqueFriendInDeck();
        }
    }

    private void SpawnUniqueFriendInDeck()
    {
        Debug.Log("spawn friend");

        int index = Random.Range(0, possibleFriends.Length);

        while (spawnedFriendsIndex.Contains(index))
        {
            index = Random.Range(0, possibleFriends.Length);
        }

        GameObject newFriend = Instantiate(possibleFriends[index], transform);
        spawnedFriendsIndex.Add(index);

    }

    public void ClearPlaySpace()
    {
        spawnedFriendsIndex.Clear();
        GameObject[] friends = GameObject.FindGameObjectsWithTag("Friend");

        if(friends.Length > 0)
        {
            foreach (GameObject friend in friends)
            {
                Destroy(friend);
            }
        }
    }

}
