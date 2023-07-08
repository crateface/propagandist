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
        int [] newValues = API(text);
        int reporter = (int)reporters*100;
        int factChecker = (int)factCheckers * 100;
        int advert = (int)adverts * 100;
        updateRevenue(newValues[0] - reporter/6 - factChecker/6 + advert/3);
        updateOutreach(newValues[1] + reporter/9);
        updateStateSupport(newValues[2]);
        updateCredibility(newValues[3] + factChecker/4 - advert/6);
        articlesPublished += 1;
        //if (revenue + outreach + stateSupport == 0 & youMessedUp == 0) { firebaseManager.main.updateAchievement("sudden collapse"); }
        if (revenue == 0) { youMessedUp += 1; }
        if (outreach == 0) { youMessedUp += 1; }
        if (stateSupport == 0) { youMessedUp += 1; }
        if (revenue * outreach * stateSupport != 0) { youMessedUp = 0; }
        if (youMessedUp >= 3) { gameOver(); }
        MenuManager.main.updateStrikes(youMessedUp);
        EventManager.main.refreshEvent();
        //firebaseManager.main.updateValues(revenue, outreach, stateSupport, credibility);
        ContactManager.main.generateContact();
    }

    public int[] API(string text)
    {
        int [] namehere = {Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)};
        return namehere;
    }

    public void gameOver()
    {
        MenuManager.main.gameOver();
    }
}
