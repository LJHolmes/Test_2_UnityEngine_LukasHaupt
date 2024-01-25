using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource WashedScoredSound;
    public AudioSource CollectedScoreSound;

    public Color SoundOn;
    public Color SoundOff;

    public Button SoundButton;

    public void ToggleSound()
    {
        WashedScoredSound.mute = !WashedScoredSound.mute;
        CollectedScoreSound.mute = !CollectedScoreSound.mute;

        if (WashedScoredSound.mute)
        {
            SoundButton.image.color = SoundOff;
        }
        else
        {
            SoundButton.image.color = SoundOn;
        }
    }

    public void PlayWashedScoredSound()
    {
        WashedScoredSound.Play();
    }

    public void PlayCollectedScoreSound()
    {
        CollectedScoreSound.Play();
    }
}
