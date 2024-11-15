using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] AudioSource bgm;

    private void Awake()
    {
        bgm = GetComponent<AudioSource>();
    }

    public void PlayBGM()
    {
        bgm.Play();
    }
}
