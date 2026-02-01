using UnityEngine;
using System.Collections;

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

    public void PlaySound2D(string soundName, float volume = 1f, float delay = 0f)
    {
        AudioClip clip = sfxLibrary.GetClipFromName(soundName);
        if (clip == null) return;

        // Interpret negative values as decibels (e.g. -10f means 10 dB quieter)
        float finalVolume = volume < 0 ? Mathf.Pow(10f, volume / 20f) : volume;
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

    private IEnumerator PlayDelayedSound(AudioClip clip, float volume, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (clip == null) yield break;
        sfx2DSource.PlayOneShot(clip, volume);
    }
}