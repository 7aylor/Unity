using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DestroyOnFinishAudio : MonoBehaviour
{

    private float startTime;
    private AudioSource audioSource;

    void Awake()
    {
        startTime = Time.time;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time >= startTime + audioSource.clip.length)
        {
            Destroy(gameObject);
        }
    }
}