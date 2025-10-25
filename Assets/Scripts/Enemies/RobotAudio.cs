using UnityEngine;

public class RobotAudio : MonoBehaviour
{
    public AudioSource audioSource;  // Assign your AudioSource
    public AudioClip footstepClip;   // Assign your footstep sound

    // This function will be called from the animation
    public void PlayFootstep()
    {
        if (audioSource && footstepClip)
        {
            audioSource.PlayOneShot(footstepClip);
        }
    }
}
