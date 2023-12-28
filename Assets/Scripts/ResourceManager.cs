using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    int revenue;
    int outreach;
    int stateSupport;
    int credibility;
    public static ResourceManager main;
    int articlesPublished;
    int youMessedUp;
    float currentReporters;
    float currentFactCheckers;
    float currentAdverts;
    bool militaryAnalyst;
    bool politicalAnalyst;
    bool insurance;
    string currentSource;

    public void Awake()
    {
        main = this;
    }
    void Start()
    {
        revenue = 25;
        outreach = 10;
        stateSupport = 50;
        credibility = 50;
        articlesPublished = 0;
        MenuManager.main.updateBars(revenue, outreach, stateSupport, credibility);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateRevenue(int change)
    {
        revenue += change;
        revenue = Mathf.Clamp(revenue, 0, 100);
        MenuManager.main.updateBars(revenue, outreach, stateSupport, credibility);
    }
    public void updateOutreach(int change)
    {
        outreach += change;
        if (insurance) { outreach += 3; }
        outreach = Mathf.Clamp(outreach, 0, 100);
        MenuManager.main.updateBars(revenue, outreach, stateSupport, credibility);
    }
    public void updateStateSupport(int change)
    {
        stateSupport += change;
        stateSupport = Mathf.Clamp(stateSupport, 0, 100);
        MenuManager.main.updateBars(revenue, outreach, stateSupport, credibility);
    }
    public void updateCredibility(int change)
    {
        credibility += change;
        credibility = Mathf.Clamp(credibility, 0, 100);
        MenuManager.main.updateBars(revenue, outreach, stateSupport, credibility);
    }

    public void publish(string text, float reporters, float factCheckers, float adverts, string source)
    {
        currentAdverts = adverts;
        currentFactCheckers = factCheckers;
        currentReporters = reporters;
        currentSource = source;
        if (text.Split(' ').Length < 5)
            calculateValues(0, 0, 0);
        else 
            GptManager.main.SubmitChat(text);
        // serverScript.main.sendTitle(text);
    }

    public void calculateValues(float cred, float support, float polarity)
    {
        string type;
        int credVal = 0;
        int reporter = (int)currentReporters*100;
        int factChecker = (int)currentFactCheckers * 100;
        int advert = (int)currentAdverts * 100;
        int temp = 0;
        
        if (ContactManager.main.findContact(currentSource) == null)
            type = "notype"; 
        else 
            type = ContactManager.main.findContact(currentSource).type; 

        string eventAffilation = EventManager.main.currentEvent.affilation;
        string eventType = EventManager.main.currentEvent.eventType;

        if ((type == eventAffilation || type == eventType) && type != "notype") 
            updateCredibility(5); 
        else { updateCredibility(-15); }

        if (cred == 0)
        {  
            credVal = -7; 
            temp = Random.Range(credVal, 0); 
        }
        else 
        { 
            credVal = 7; 
            temp = Random.Range(0, credVal); 
        }

        updateOutreach(reporter/9 + (int)polarity*5 - temp);
        updateRevenue(outreach/10 - reporter/9 - factChecker/9 + advert/4);
        updateStateSupport((int)(support * 10));
        updateCredibility(credVal + factChecker/6 - advert/6 + Random.Range(-3,1));

        if (revenue == 0) { youMessedUp += 1; }
        if (outreach == 0) { youMessedUp += 1; }
        if (stateSupport == 0) { youMessedUp += 1; }
        if (revenue * outreach * stateSupport != 0) { youMessedUp = 0; }
        if (youMessedUp >= 3) { gameOver("Ran out of resources"); }

        MenuManager.main.updateStrikes(youMessedUp);
        EventManager.main.refreshEvent();
        ContactManager.main.generateContact();
    }

    public void gameOver(string whyGameEnd)
    {
        MenuManager.main.gameOver(whyGameEnd);
    }

    public void hired(string whoHire)
    {
        if (whoHire == "you")
        {
            insurance = true;
        } 
        else if (whoHire == "political")
        {
            politicalAnalyst = true;
        }
        else if (whoHire == "military")
        {
            militaryAnalyst = true;
        }
    }

    public int getOutreach()
    {
        return outreach;
    }
}
