using UnityEngine.UI;
using UnityEngine;

public class AudioManagerCS : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MusicPref = "MusicPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";
    private int firstPlayInt;
    public Slider musicSlider, soundEffectsSlider;
    private float musicVolumeFloat, soundEffectsVolumeFloat;
    public AudioSource[] musicAudio;
    public AudioSource[] sfxAudio;

    private void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        // Check if we play the game for the first time
        if (firstPlayInt == 0)
        {
            // Set and save default values for the volume
            musicVolumeFloat = 0.1f;
            soundEffectsVolumeFloat = 0.3f;
            musicSlider.value = musicVolumeFloat;
            soundEffectsSlider.value = soundEffectsVolumeFloat;
            PlayerPrefs.SetFloat(MusicPref, musicVolumeFloat);
            PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsVolumeFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            // Get previously saved values and apply it for the volume
            musicVolumeFloat = PlayerPrefs.GetFloat(MusicPref);
            soundEffectsVolumeFloat = PlayerPrefs.GetFloat(SoundEffectsPref);

            // Set sliders to the same values
            musicSlider.value = musicVolumeFloat;
            soundEffectsSlider.value = soundEffectsVolumeFloat;
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(MusicPref, musicSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);
    }

    private void OnApplicationFocus(bool inFocus)
    {
        // If we pause or minimize or exit the game - save the volume settings
        if (inFocus == false)
        {
            SaveSoundSettings();
        }
    }

    private void OnApplicationQuit()
    {
        SaveSoundSettings();
    }

    public void UpdateAudio()
    {
        // Set volume of all audio clips in the array to slider value
        for (int i = 0; i < musicAudio.Length; i++)
        {
            musicAudio[i].volume = musicSlider.value;
        }
        for (int i = 0; i < sfxAudio.Length; i++)
        {
            sfxAudio[i].volume = soundEffectsSlider.value;
        }
    }

    // Test sfx, delete after
    public void TestSFX()
    {
        sfxAudio[0].Play();
    }
}
