using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField] private GameObject innerMaze;
    [SerializeField] private AudioSource mazeSound;

    public void PlayRotateSound()
    {
        mazeSound.Play();
    }
}
