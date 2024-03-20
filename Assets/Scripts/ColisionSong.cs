using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    public AudioClip collisionClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        
        audioSource.playOnAwake = false;
        audioSource.clip = collisionClip; 
    }

    public void play()
    {
        audioSource.Play();
    }
}
