using System.Collections.Generic;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using UnityEngine;

public class GptManager : MonoBehaviour
{
    OpenAIClient openAI;
    List<Message> messages;

    void Start()
    {
        messages = new List<Message>
        {
            new Message(Role.System, "You are a supported through my adventures!"), // Starting prompt
        };

        openAI = new OpenAIClient();

        SubmitChat("Give me a random fact");
    }

    async void SubmitChat(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) { return; }

        messages.Add(new Message(Role.User, input));                                // Add your string input to the message conversation
        var chatRequest = new ChatRequest(
            messages,
            Model.GPT3_5_Turbo, maxTokens: 50
        );                                                                          // Create a chat request with specified parameters

        var response = await openAI.ChatEndpoint.GetCompletionAsync(chatRequest);   // Get a response
        messages.Add(new Message(Role.Assistant, response.FirstChoice));            // Add the response to our continuing conversation
        Debug.Log(response.FirstChoice);                                            // Print out response to Unity console
    }

}