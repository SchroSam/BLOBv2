using UnityEngine;

public class JanitorSwingAudio : MonoBehaviour
{
    public AudioSource audioSource;  // Assign your AudioSource
    public AudioClip SwingClip;   // Assign your swing sound

    // This function will be called from the animation
    public void PlayFootstep()
    {
        if (audioSource && SwingClip)
        {
            audioSource.PlayOneShot(SwingClip);
        }
    }
}