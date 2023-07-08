using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject resourceMenu;
    public GameObject contactMenu;
    public GameObject gameOverScreen;
    public Image revenueBar;
    public Image outreachBar;
    public Image stateSupportBar;
    public Image credibilityBar;
    public Image revenueBar2;
    public Image outreachBar2;
    public Image stateSupportBar2;
    public Image credibilityBar2;
    public static MenuManager main;
    public TMP_InputField articleTitle;
    public Slider reporters;
    public Slider factCheckers;
    public Slider adverts;
    public TextMeshProUGUI strikesText;
    public TextMeshProUGUI eventText;
    public TextMeshProUGUI eventCounterText;
    
    // Start is called before the first frame update

    public void Awake()
    {
        main = this;
    }
    void Start()
    {
        openMain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openMain()
    {
        mainMenu.SetActive(true);
        resourceMenu.SetActive(false);
        contactMenu.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    public void openResource()
    {
        mainMenu.SetActive(false);
        resourceMenu.SetActive(true);
        contactMenu.SetActive(false);
    }

    public void openContact()
    {
        mainMenu.SetActive(false);
        resourceMenu.SetActive(false);
        contactMenu.SetActive(true);
    }

    public void gameOver()
    {
        mainMenu.SetActive(false);
        resourceMenu.SetActive(false);
        contactMenu.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void updateBars(float revenue, float outreach, float stateSupport, float credibility)
    {
        revenueBar.fillAmount = revenue / 100; 
        outreachBar.fillAmount = outreach / 100;
        stateSupportBar.fillAmount = stateSupport / 100;
        credibilityBar.fillAmount = credibility / 100;
        revenueBar2.fillAmount = revenue / 100;
        outreachBar2.fillAmount = outreach / 100;
        stateSupportBar2.fillAmount = stateSupport / 100;
        credibilityBar2.fillAmount = credibility / 100;
    }

    public void recordTitle()
    {
        if (articleTitle.text == "")
        { return; }
        ResourceManager.main.publish(articleTitle.text,reporters.value,factCheckers.value,adverts.value);
        articleTitle.text = "";
    }

    public void updateStrikes(int strikes)
    {
        strikesText.text = "Strikes: " + strikes;

    }
    
    public void displayEvent(string currentEvent)
    {
        eventText.text = currentEvent;
    }

    public void updateEventCounter(int eventsRemaining)
    {
        eventCounterText.text = "Events until next phase: " + eventsRemaining;
    }

    public void updateContacts(GameObject[] contacts)
    {

    }
}
