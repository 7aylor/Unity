using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LidDispenser : MonoBehaviour {

    public GameObject button;
    private Button lidButtonComponent;
    private SpriteRenderer sprite;
    private GameObject jarInRange;

	// Use this for initialization
	void Start () {
        lidButtonComponent = button.GetComponent<Button>();
        sprite = GetComponent<SpriteRenderer>();
        lidButtonComponent.interactable = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Jar>() && collision.isTrigger)
        {
            jarInRange = collision.gameObject;
            lidButtonComponent.interactable = true;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Jar>() && collision.isTrigger)
        {
            jarInRange = null;
            lidButtonComponent.interactable = false;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        }
    }

    public void DispenseLid()
    {
        foreach(Transform jarComponent in jarInRange.transform)
        {
            if(jarComponent.gameObject.tag == "Lid")
            {
                jarComponent.gameObject.SetActive(true);
                lidButtonComponent.interactable = false;
                return;
            }
        }
    }
}
