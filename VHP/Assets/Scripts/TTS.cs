using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using System.Text;

public class TTS : MonoBehaviour
{
    string urls = "D:/tts.mp3";
    public AudioSource audioSource;
    public AudioClip audioClip;
    public void TTS_Talk()
    {
        Debug.Log("테스트 3");
        StartCoroutine(DownloadTheAudio());
        IEnumerator DownloadTheAudio()
        {

            WWW www = new WWW(urls);
            yield return www;

            audioSource.clip = www.GetAudioClip(false, true, AudioType.MPEG);
            audioSource.Play();
            Debug.Log("테스트 4");
        }

    }
}
