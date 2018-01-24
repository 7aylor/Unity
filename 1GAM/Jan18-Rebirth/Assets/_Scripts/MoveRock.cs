using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRock : MonoBehaviour {

    public float speedX;
    public float speedY;
    public float rotationSpeed;
    public float decayRate;


    private Vector2 throwDir;

	// Use this for initialization
	void Start () {
        Caveman_Move.direction rockDir = Caveman_Move.CavemanDirection;


        if(rockDir == Caveman_Move.direction.down)
        {
            throwDir = Vector2.down * Time.deltaTime;
        }
        else if (rockDir == Caveman_Move.direction.up)
        {
            throwDir = Vector2.up * Time.deltaTime;
        }
        else if (rockDir == Caveman_Move.direction.left)
        {
            throwDir = Vector2.left * Time.deltaTime;
        }
        else
        {
            throwDir = Vector2.right * Time.deltaTime;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(speedX > 2f)
        {

            if(Caveman_Move.CavemanDirection == Caveman_Move.direction.left || Caveman_Move.CavemanDirection == Caveman_Move.direction.right)
            {
                transform.Translate(throwDir * speedX, Space.World);
            }
            else
            {
                transform.Translate(throwDir * speedY, Space.World);
            }
            
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed, Space.World);
            speedX = Mathf.Pow(speedX, decayRate);
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
