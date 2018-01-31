using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayerManager : MonoBehaviour
{

    private void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y) * -1;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    SpriteRenderer collidingObjectSprite = collision.gameObject.GetComponent<SpriteRenderer>();

    //    Debug.Log("enter");
    //    if (collidingObjectSprite != null)
    //    {

    //        if (transform.parent.position.y < collision.gameObject.transform.position.y)
    //        {
    //            SendMessageUpwards("AdjustSpriteLayer", collidingObjectSprite.sortingOrder - 1);
    //        }
    //        else
    //        {
    //            if (Caveman_Move.CavemanDirection == Caveman_Move.direction.up)
    //            {
    //                SendMessageUpwards("AdjustSpriteLayer", collidingObjectSprite.sortingOrder + 1);
    //            }
    //            else
    //            {
    //                SendMessageUpwards("AdjustSpriteLayer", collidingObjectSprite.sortingOrder - 1);
    //            }
    //        }
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("exit");
    //    SpriteRenderer collidingObjectSprite = collision.gameObject.GetComponentInParent<SpriteRenderer>();

    //    if (collidingObjectSprite != null)
    //    {
    //        if (transform.parent.position.y < collision.gameObject.transform.position.y)
    //        {
    //            SendMessageUpwards("AdjustSpriteLayer", collidingObjectSprite.sortingOrder + 1);
    //        }
    //        else
    //        {
    //            SendMessageUpwards("AdjustSpriteLayer", collidingObjectSprite.sortingOrder - 1);
    //        }
    //    }
    //}
}
