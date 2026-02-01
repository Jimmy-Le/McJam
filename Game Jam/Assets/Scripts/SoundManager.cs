using System.Diagnostics;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private SoundLibrary sfxLibrary;
    [SerializeField] private AudioSource sfx2DSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySound3D(AudioClip clip, Vector3 pos)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos);
        }
    }

    public void PlaySound3D(string soundName, Vector3 pos)
    {
        PlaySound3D(sfxLibrary.GetClipFromName(soundName), pos);
    }

    // === FIXED Version ===
    public void PlaySound2D(string soundName, float volume = 1f, float delay = 0f)
    {
        AudioClip clip = sfxLibrary.GetClipFromName(soundName);



        // If user passes a negative volume, treat it as decibels (like -10 dB)
        float finalVolume = volume < 0 ? Mathf.Pow(10f, volume / 20f) : volume;

        // Clamp volume between 0–1 to avoid errors
        finalVolume = Mathf.Clamp01(finalVolume);

        if (delay <= 0f)
        {
            sfx2DSource.PlayOneShot(clip, finalVolume);
        }
        else
        {
            StartCoroutine(PlayDelayedSound(clip, finalVolume, delay));
        }
    }

    private System.Collections.IEnumerator PlayDelayedSound(AudioClip clip, float volume, float delay)
    {
        yield return new WaitForSeconds(delay);
        sfx2DSource.PlayOneShot(clip, volume);
    }
}