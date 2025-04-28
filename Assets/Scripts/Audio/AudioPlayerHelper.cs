using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerHelper : MonoBehaviour
{
    public AudioSource audioSource;
    public KeyCode keyCode = KeyCode.P;

    //Debug para testar audio
    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            Play();
        }
    }

    //Função para executar áudio
    public void Play()
    {
        audioSource.Play();
    }
}
