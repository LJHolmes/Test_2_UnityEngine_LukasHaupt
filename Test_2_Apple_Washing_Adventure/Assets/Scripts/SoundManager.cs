using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource AppleWashedSound;

    public void ToggleSound()
    {
        AppleWashedSound.mute = !AppleWashedSound.mute;
    }

    public void PlayAppleWashSound()
    {
        AppleWashedSound.Play();
    }
}
