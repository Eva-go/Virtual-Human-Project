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
        Conversation.Add("안녕하세요", "안녕하세요!");
        Conversation.Add("예매는 어떻게 하나요?", "오른쪽 키오스크에 예매를 하시면 됩니다");
        Conversation.Add("이름이 뭐에요?", "저는 김소희입니다.");
        Conversation.Add("매점은 어디에 있나요?", "나가셔서 정면에 있습니다.");
        Conversation.Add("이터널스 예매하고싶어요", "이터널스 영화 한표 맞습니까?");
        Conversation.Add("네 맞습니다", "여기 있습니다. 감사합니다.");
        //gameObject.GetComponent<TextOutput>().enabled = true;
    }
    public void EndChat()
    {
        

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("테스트 ");
            if (Conversation.TryGetValue(inputField.text, out string description))
            {
                Debug.Log("테스트 1");
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
        Debug.Log("테스트 2");




        //SW.Close();

    }
        
}

