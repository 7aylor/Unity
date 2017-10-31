using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public Material[] colors;
    public GameObject explosion;
    public Material startColor;
    public bool canPickUp = false;
    public bool pickedUp = false;
    public AudioClip combine, pickup, drop;

    private Player player;
    private Renderer r;
    private Color highlight = Color.red;
    private Rigidbody rb;
    private float lockedY = 1;
    private AudioSource audioSource;


    void Start () {
        r = GetComponent<Renderer>();
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        //Checks to see if we are in the first training level to spawn one type of color
        //for each item
        if (LevelManager.instance.GetCurrentSceneName() == "Level_01")
        {
            Debug.Log("Color set");
            ColorController c = FindObjectOfType<ColorController>();
            startColor = c.startColor;
            r.material = startColor;
        }
        else
        {
            SetRandomColor();
        }
    }

    private void Update()
    {
        //rotate the item for visual effect
        transform.Rotate(new Vector3(0.5f, 0.5f, 0.5f));

        PickUpItem();
    }


    /// <summary>
    /// Used on start to choose a random color for the item.
    /// </summary>
    private void SetRandomColor()
    {
        int index = Random.Range(0, colors.Length);
        startColor = colors[index];
        r.material = startColor;
    }

    /// <summary>
    /// Determines if an item can be picked up and performs that action, also dropping an item
    /// </summary>
    private void PickUpItem()
    {
        //if we hit the pickup button
        if (Input.GetButtonDown("PickUp"))
        {
            //if we are within range and hightlighted, and the mouse is clicked, pick up item
            if (canPickUp == true)
            {
                player.pickedUpItems.Add(gameObject);
                gameObject.transform.parent = player.transform;
                pickedUp = true;
                canPickUp = false;
                r.material = startColor;
                rb.velocity = Vector3.zero;
                PositionPickedUpItem();
                audioSource.clip = pickup;
                audioSource.Play();
            }
            else if (pickedUp == true)
            {
                gameObject.transform.parent = null;
                pickedUp = false;
                audioSource.clip = drop;
                audioSource.Play();
            }
        }
    }

    /// <summary>
    /// Places a newly picked up item right in front of the Player
    /// </summary>
    private void PositionPickedUpItem()
    {
        transform.position = player.transform.position;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 1.5f);
        rb.velocity = Vector3.zero;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Item collisionItem = collision.gameObject.GetComponent<Item>();

        if (collisionItem != null && collisionItem.startColor == startColor) {
            FindObjectOfType<VignetteController>().CombineItemsEvent();

            ParticleSystemRenderer psr = explosion.GetComponent<ParticleSystemRenderer>();
            psr.material = startColor;

            audioSource.clip = combine;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            Instantiate(explosion, transform.position, Quaternion.identity);

            gameObject.GetComponent<MeshRenderer>().enabled = false;
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<ParticleSystem>().Stop();
            collision.gameObject.GetComponent<ParticleSystem>().Stop();

            StartCoroutine(DestroyItems(collision.gameObject, gameObject, audioSource.clip.length));
        }
    }

    private IEnumerator DestroyItems(GameObject thisItem, GameObject collisionItem, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(collisionItem);
        Destroy(thisItem);
    }

    private void OnCollisionExit(Collision collision)
    {
        if(pickedUp == true && collision.gameObject.tag == "Wall")
        {
            Invoke("PositionPickedUpItem", 1f);
        }
    }
}
