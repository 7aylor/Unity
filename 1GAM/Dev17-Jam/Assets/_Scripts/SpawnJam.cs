using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnJam : MonoBehaviour {

    public Color[] jamColors;
    public GameObject jam;
    public static int jamIndex = 0;
    public static int prevJamIndex = jamIndex;
    public Sprite[] images;
    public GameObject jamIndicator;
    private bool canChangeJam;
    private NotifiationManager notificationManager;
    private JamDestroyer jamDestroyer;

    private void Start()
    {
        jamDestroyer = FindObjectOfType<JamDestroyer>();
        notificationManager = FindObjectOfType<NotifiationManager>();
        canChangeJam = false;
    }

    private void Update()
    {
            if (Input.GetButton("Strawberry"))
            {
                ChangeJamType(0);
            }
            else if (Input.GetButton("Raspberry"))
            {
                ChangeJamType(1);
            }
            else if (Input.GetButton("Grapes"))
            {
                ChangeJamType(2);
            }
            else if (Input.GetButton("Peach"))
            {
                ChangeJamType(3);
            }
        
        //else if()
        //{
        //    notificationManager.UpdateNotificationText("Please wait for old Jam Type to be cleared");
        //}
    }

    public void DispenseJam()
    {
        GameObject j = Instantiate(jam, transform);
        j.GetComponent<SpriteRenderer>().color = jamColors[jamIndex];
        j.GetComponent<Jam>().JamIndex = jamIndex;

        switch (jamIndex)
        {
            case 0:
                j.name = "Strawberry";
                break;
            case 1:
                j.name = "Raspberry";
                break;
            case 2:
                j.name = "Grape";
                break;
            case 3:
                j.name = "Peach";
                break;
        }
    }

    public virtual void ChangeJamType(int index)
    {
        prevJamIndex = jamIndex;
        jamIndex = index;
        jamIndicator.GetComponent<Image>().sprite = images[index];
        //jamDestroyer.EnableCollider();
    }


}
