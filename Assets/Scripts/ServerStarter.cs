using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ServerStarter : MonoBehaviour
{
    public static System.Diagnostics.Process process;
    public bool checkingForServer;
    void Start()
    {
        if (process == null)
        {
            LoadServer();
        }
    }
    public void LoadServer()
    {
        process = new System.Diagnostics.Process();
        string path = Application.streamingAssetsPath + System.IO.Path.AltDirectorySeparatorChar + "server.py";
        process.StartInfo.FileName = "python";
        process.StartInfo.Arguments = path;
        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
        print(path);
        process.Start();
    }
    public void CheckServer()
    {
        StartCoroutine(sendrequest());
    }
    void SeverUp()
    {
        SceneLoader.main.loadScene(1);
    }
    IEnumerator sendrequest() 
    {
        using (UnityWebRequest webrequest = UnityWebRequest.Get("http://127.0.0.1:5000"))
        {
            yield return webrequest.SendWebRequest();
            if (webrequest.isNetworkError)
            {
                CheckServer();
            }
            else
            {
                SeverUp();
            }
        }
    }
}
