using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> musicList;
    private List<AudioClip> newMusicList;

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PickNewSong(audioSource.clip);
            audioSource.Play();
        }
    }

    private void PickNewSong(AudioClip currentSong)
    {
        newMusicList = new List<AudioClip>();
        foreach (AudioClip music in musicList)
        {
            if(music.name != currentSong.name)
            {
                newMusicList.Add(music);
            }
        }
        AudioClip newClip = musicList[Random.Range(0, newMusicList.Count + 1)];
        audioSource.clip = newClip;
    }
}
