using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Texture2D cursor0;
    public Texture2D cursor1;

    private bool clicked = false;
    private float jarSpeed = 1f;
    private float spawnSpeed = 5f;
    private float timeToPause = 2f; //TODO: create method to decrease this when hitting a checkpoint
    private int numLives = 5;
    private int jamWasted = 0;
    private bool canChangeSpeeds = false;

    public static GameManager instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log(gameObject.name + " Destroyed on Load");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    // Use this for initialization
    void Start () {
        Cursor.SetCursor(cursor0, new Vector2(0,0), CursorMode.Auto); //0.325f, 0.99f
        Cursor.visible = true;
	}

    private void Update()
    {
        HandleMouseCursorOnClick();
        ManageSpeeds();
    }

    /// <summary>
    /// update the cursor image when you click
    /// </summary>
    private void HandleMouseCursorOnClick()
    {
        if (Input.GetMouseButton(0))
        {
            if (clicked == false)
            {
                Cursor.SetCursor(cursor1, new Vector2(0, 0), CursorMode.Auto);
                clicked = true;
            }
        }
        else
        {
            Cursor.SetCursor(cursor0, new Vector2(0, 0), CursorMode.Auto);
            clicked = false;
        }
    }

    #region getters and setters
    public float GetSpeed()
    {
        return jarSpeed;
    }

    public float GetSpawnSpeed()
    {
        return spawnSpeed;
    }

    public float GetTimeToPause()
    {
        return timeToPause;
    }

    public void DecreaseLives()
    {
        numLives--;
    }

    public int GetNumLives()
    {
        return numLives;
    }

    public void IncreaseJamWasted()
    {
        if(jamWasted < 100)
        {
            jamWasted++;
        }
        else
        {
            jamWasted = 0;
        }
    }

    public int GetJamWasted()
    {
        return jamWasted;
    }

    public void SetCanChangeSpeed(bool changed)
    {
        canChangeSpeeds = changed;
    }
    #endregion

    private void ManageSpeeds()
    {
        int points = PointManager.instance.GetPoints();
        if (canChangeSpeeds == true && points > 1 && points % 5 == 0 && points <= 30)
        {
            spawnSpeed -= 0.5f;
            jarSpeed += 0.5f;
            timeToPause -= 0.1f;

            foreach(ConveyorBelt c in FindObjectsOfType<ConveyorBelt>())
            {
                c.ChangePlaySpeed(0.1f);
            }

            canChangeSpeeds = false;
        }
    }
}
