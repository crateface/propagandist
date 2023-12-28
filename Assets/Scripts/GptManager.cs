using System.Collections.Generic;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using UnityEngine;
using UnityEngine.UI;

public class GptManager : MonoBehaviour
{
    public Button publishButton;

    OpenAIClient openAI;
    List<Message> messages;

    public static GptManager main; 

    void Awake() 
    {
        main = this;
    }

    void Start()
    {
        messages = new List<Message>
        {
            new Message(Role.System, "Theme of newspaper: Russian-Ukraine war." + "\n" +
                                    "Credibility - 1 True or 0 false, whether the title is similar to previous newspaper articles regarding the war, aka the ethos" + "\n" +
                                    "State Support - In a scale for 0-1, how does the title support (positive appeal) towards the Ukraine" + "\n" +
                                    "Polarity - In a scale of 0-1, how \"angry\" is the title aka the pathos anger" + "\n\n" +
                                    "You are going to wait for a response. I'm going to provide you a newspaper article title and a contact. You will then provide me with credibility, Ukraine state support, polarity ONLY in JSON format. So variables for credibility, support, polarity in one JSON file." + "\n" +
                                    "Example format:" + "\n" +
                                    "{" + "\n" +
                                    "\"credibility\": 1.0," + "\n" +
                                    "\"state_support\": 0.4," + "\n" +
                                    "\"polarity\": 0.2" + "\n" +
                                    "}" + "\n\n" +
                                    "Do you understand?"), // Starting prompt
        };

        openAI = new OpenAIClient();
    }

    public async void SubmitChat(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) { return; }
        publishButton.interactable = false;

        messages.Add(new Message(Role.User, "Title: " + title));                    // Add your string input to the message conversation
        var chatRequest = new ChatRequest(
            messages,
            Model.GPT3_5_Turbo
        );                                                                          // Create a chat request with specified parameters

        var response = await openAI.ChatEndpoint.GetCompletionAsync(chatRequest);   // Get a response
        messages.Add(new Message(Role.Assistant, response.FirstChoice));
        try
        {
            GetArticleJSONInfo(response.FirstChoice +"fdsfdfdsfdsfs");
        }
        catch(System.Exception e)
        {
            Debug.Log("Error JSON Format: " + e.Message);

            messages.Add(new Message(Role.User, "Can you just give me the JSON format and THATS IT."));
            response = await openAI.ChatEndpoint.GetCompletionAsync(chatRequest);

            GetArticleJSONInfo(response.FirstChoice);
        }

        publishButton.interactable = true;
        Debug.Log(response.FirstChoice);                                            // Print out response to Unity console
    }

    ArticleInfo GetArticleJSONInfo(string input)
    {
        ArticleInfo info = JsonUtility.FromJson<ArticleInfo>(input);
        ResourceManager.main.calculateValues(info.credibility, info.state_support, info.polarity);
        return info;
    }
}

[System.Serializable]
public class ArticleInfo
{
    public float credibility;
    public float state_support;
    public float polarity;
}