using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Handler : MonoBehaviour
{
    public AudioSource Background, PlayerSound, EnemySound, FXPrefab;
    private List<AudioSource> fxSounds = new List<AudioSource>();
    public SoundClass[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        fxSounds.Add(Background);
        fxSounds.Add(PlayerSound);
        fxSounds.Add(EnemySound);
        fxSounds.Add(FXPrefab);
    }
    // Step 4: locate the sound Clip and play it.
    public void PlaySound(int soundSource, int audioIndex)
    {
        fxSounds[soundSource].clip = audioClips[audioIndex].GetClip();
        fxSounds[soundSource].Play();
    }
    //Old playsound function
    public void PlaySound(string sourceName, AudioClip clip)
    {
        if (sourceName == "Player" || sourceName == "player")
        {
            PlayerSound.clip = clip; PlayerSound.Play();
        }
        else if (sourceName == "enemy" || sourceName == "Enemy")
        {
            EnemySound.clip = clip; EnemySound.Play();
        }
        else if (sourceName == "background" || sourceName == "Background")
        {
            Background.clip = clip; Background.Play();
        }
        else if (sourceName == "FX" || sourceName == "fx")
        {
            FXPrefab.clip = clip; FXPrefab.Play();
        }
    }
    // Step 3 : locate the source source
    public void PlaySound(string sourceName, int audioIndex)
    {
        if (sourceName == "Player" || sourceName == "player") PlaySound(1, audioIndex);
        if (sourceName == "Background" || sourceName == "background") PlaySound(0, audioIndex);
        if (sourceName == "enemy" || sourceName == "Enemy") PlaySound(2, audioIndex);
        if (sourceName == "FX" || sourceName == "fx") PlaySound(3, audioIndex);
    }
    // Step 1: Play sound using sourceName and soundclass
    public void PlaySound(string sourceName, string soundclass)
    {
        PlaySound(sourceName, GetSoundClass(soundclass));
    }
    // Step 2: get the sound index using soundClass
    int GetSoundClass(string name)
    {
        int index = 0;
        foreach (SoundClass soundclass in audioClips)
        {
            if (soundclass.Name == name) return index;
            index += 1;
        }
        Debug.Log("soundclass not found");
        return -1;
    }

    public void muteVolume()
    {
        float volume = PlayerSound.volume;
        if (volume < 1)
        {
            PlayerSound.volume = 1f;
            Background.volume = 1f;
            EnemySound.volume = 1f;
            FXPrefab.volume = 1f;
        }
        else
        {
            PlayerSound.volume = 0f;
            Background.volume = 0f;
            EnemySound.volume = 0f;
            FXPrefab.volume = 0f;
        }       
    }
}
//Sound Resource class
[System.Serializable]
public class SoundClass
{
    public string Name;
    public AudioClip[] clips;

    public AudioClip GetClip()
    {
        int clip = Random.Range(0, clips.Length);
        return GetClip(clip);
    }
    public AudioClip GetClip(int index)
    {
        return clips[index];
    }
}
