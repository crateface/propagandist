/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;

public class firebaseManager : MonoBehaviour
{
    string userID;
    DatabaseReference refe;
    public static firebaseManager main;

    public void Awake()
    {
        main = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        refe = FirebaseDatabase.DefaultInstance.RootReference;
        createUser();
        readEvents(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createUser()
    {
        refe.Child("Users").Child(userID).Child("name").SetValueAsync("some");
        Debug.Log("user made");
    }

    public void updateValues(float revenue, float outreach, float stateSupport, float credibility)
    {
        refe.Child("Users").Child(userID).Child("revenue").SetValueAsync(revenue);
        refe.Child("Users").Child(userID).Child("outreach").SetValueAsync(outreach);
        refe.Child("Users").Child(userID).Child("stateSupport").SetValueAsync(stateSupport);
        refe.Child("Users").Child(userID).Child("credibility").SetValueAsync(credibility);
    }

    public void updateAchievement(string achievement)
    {
        refe.Child("Users").Child(userID).Child("achievements").Child(achievement).SetValueAsync(true);
    }

    public void readEvents(int phase)
    {
        var a = refe.Child("Events").Child("Phase " + phase).GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            { Debug.Log(task.Exception.Message); }
            else if (task.IsCompleted)
            { DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.Value);
            }
        });
        Debug.Log(a);
    }
}*/
