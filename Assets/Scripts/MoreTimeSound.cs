using UnityEngine;

public class MoreTimeSound : MonoBehaviour
{
    public AudioClip moreTimeClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        
        audioSource.playOnAwake = false;
        audioSource.clip = moreTimeClip; 
    }

    public void play()
    {
        audioSource.Play();
    }
}
