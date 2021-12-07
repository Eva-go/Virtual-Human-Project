using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using System.Text;


public class TextOutput : MonoBehaviour
{

    public AudioSource audioSource;
    public InputField inputField;
    //public AudioSource audioSource;
    //public AudioClip audioClip;
    Dictionary<string, string> Conversation = new Dictionary<string, string>();
    
    string TTS_Connect;
    string urls = "D:/tts.mp3";
    private void Start()
    {
        Conversation.Add("�ȳ��ϼ���", "�ȳ��ϼ���!");
        Conversation.Add("���Ŵ� ��� �ϳ���?", "������ Ű����ũ�� ���Ÿ� �Ͻø� �˴ϴ�");
        Conversation.Add("�̸��� ������?", "���� ������Դϴ�.");
        Conversation.Add("������ ��� �ֳ���?", "�����ż� ���鿡 �ֽ��ϴ�.");
        Conversation.Add("���ͳν� �����ϰ�;��", "���ͳν� ��ȭ ��ǥ �½��ϱ�?");
        Conversation.Add("�� �½��ϴ�", "���� �ֽ��ϴ�. �����մϴ�.");
        //gameObject.GetComponent<TextOutput>().enabled = true;
    }
    public void EndChat()
    {
        

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("�׽�Ʈ ");
            if (Conversation.TryGetValue(inputField.text, out string description))
            {
                Debug.Log("�׽�Ʈ 1");
                TTS_Connect = description;
                TTS_Load();
               
            }
            //gameObject.GetComponent<TextOutput>().enabled = false;
            inputField.text = "";
            //TTS_Talk();
            GameObject.Find("TTS").GetComponent<TTS>().TTS_Talk();
        }

    }
    /*public void TTS_Talk()
    {
        Debug.Log("�׽�Ʈ 3");
        StartCoroutine(DownloadTheAudio());
        IEnumerator DownloadTheAudio()
        {
            
            WWW www = new WWW(urls);
            yield return www;

            audioSource.clip = www.GetAudioClip(false, true, AudioType.MPEG);
            audioSource.Play();
            Debug.Log("�׽�Ʈ 4");
        }

    }*/
    public void TTS_Load()
    {
        string VoiceName = "WOMAN_READ_CALM";
        string text = TTS_Connect;
        string url = "https://kakaoi-newtone-openapi.kakao.com/v1/synthesize";

  
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "application/xml";
        request.Headers.Add("Authorization", "	e94c705e30531e322c0ff34290ab3e97");
        byte[] byteDataParams = Encoding.UTF8.GetBytes("<speak><voice name='" + VoiceName + "'>" + text + "</voice></speak>");
        request.ContentLength = byteDataParams.Length;
        Stream st = request.GetRequestStream();
        st.Write(byteDataParams, 0, byteDataParams.Length);
        st.Close();
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string status = response.StatusCode.ToString();
        File.Delete(urls);
        using (Stream output = File.OpenWrite(urls))
        using (Stream input = response.GetResponseStream())
        {
            input.CopyTo(output);
        }
        Debug.Log("�׽�Ʈ 2");




        //SW.Close();

    }
        
}

