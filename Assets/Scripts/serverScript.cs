using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class serverScript : MonoBehaviour
{

    public static serverScript main;
    // Start is called before the first frame update
    System.Diagnostics.Process process = new System.Diagnostics.Process();

    private void Awake()
    {
        main = this;
    }
    void Start()
    { string path = Application.dataPath + "/server.py";
        process.StartInfo.FileName = "python";
        process.StartInfo.Arguments = "server.py";
        process.StartInfo.WorkingDirectory = Application.dataPath;
        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
        Debug.Log(File.Exists(path));
        print(path);
        process.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sendTitle(string title)
    {
        StartCoroutine(sendrequest(title));

    }
    IEnumerator sendrequest(string title) 
    {

        WWWForm form = new WWWForm();
        form.AddField("text", title);
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
                string result = webrequest.downloadHandler.text;
                info downloadedInfo = JsonUtility.FromJson<info>(result);
                Debug.Log(downloadedInfo.stateSupport);
                Debug.Log(downloadedInfo.TrueFalse);
                //result = result.Replace("\"","");
                Debug.Log(downloadedInfo.polarity);
                ResourceManager.main.calculateValues(downloadedInfo.TrueFalse, downloadedInfo.stateSupport, downloadedInfo.polarity);

            }
        }
    }
   
}
[System.Serializable]
public class info 
{
    public string TrueFalse;
    public float stateSupport;
    public float polarity;
}
