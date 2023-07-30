using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class serverScript : MonoBehaviour
{
    // Start is called before the first frame update
    System.Diagnostics.Process process = new System.Diagnostics.Process();
    void Start()
    {
        string path = Application.dataPath + "/server.py";
        process.StartInfo.FileName = "python";
        process.StartInfo.Arguments = path;
        print(path);
        process.Start();
        StartCoroutine(sendrequest());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator sendrequest() 
    {
        WWWForm form = new WWWForm();
        form.AddField("text", "iweks");
        using (UnityWebRequest webrequest = UnityWebRequest.Post("http://127.0.0.1:5000",form))
        {
            yield return webrequest.SendWebRequest();
            if (webrequest.isNetworkError)
            {
                Debug.Log("webrequest error" + webrequest.error); 
            } else {
                print(webrequest.downloadHandler.text);
            }
        }
        using (UnityWebRequest webrequest = UnityWebRequest.Get("http://127.0.0.1:5000"))
        {
            yield return webrequest.SendWebRequest();
            if (webrequest.isNetworkError)
            {
                Debug.Log("webrequest error" + webrequest.error);
            }
            else
            {
                print(webrequest.downloadHandler.text);
            }
        }
    }
}
