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

    public void publish(string text, float reporters, float factCheckers, float adverts)
    {
        if (text.Split(' ').Length < 5)
        { calculateValues("Fake", 0, 0); }
        else { serverScript.main.sendTitle(text); }
        currentAdverts = adverts;
        currentFactCheckers = factCheckers;
        currentReporters = reporters;
    }

    public void calculateValues(string FakeorReal, float stateSentiment, float angerMeasure)
    {
        int credVal = 0;
        int reporter = (int)currentReporters*100;
        int factChecker = (int)currentFactCheckers * 100;
        int advert = (int)currentAdverts * 100;
        updateRevenue(Random.Range(-10, 10) - reporter/6 - factChecker/6 + advert/3);
        updateOutreach(Random.Range(-10, 10) + reporter/9);
        updateStateSupport((int)(stateSentiment * 50));
        if (FakeorReal == "Fake")
        { credVal = -10;}
        else { credVal = 10; }
        updateCredibility(credVal + factChecker/4 - advert/6);
        articlesPublished += 1;
        //if (revenue + outreach + stateSupport == 0 & youMessedUp == 0) { firebaseManager.main.updateAchievement("sudden collapse");
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
}
