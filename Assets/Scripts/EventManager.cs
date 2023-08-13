using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public List<Events> eventPool;
    public static EventManager main;
    public phaseEvents [] phaseList;
    public int currentPhase = 0;
    public Events currentEvent;
    // Start is called before the first frame update
    public void Awake()
    {
        main = this;
    }
    void Start()
    {
        eventPool = new List<Events>();
        foreach (Events newEvent in phaseList[currentPhase].pool)
        {
            eventPool.Add(newEvent);
        }
        refreshEvent();
    }

    void Update()
    {
        
    }

    public void refreshEvent()
    {
        if (eventPool.Count == 0)
        {
            if (currentPhase < phaseList.Length - 1)
            {
                currentPhase++;
                proceed(); 
                return; 
            } else
            {
                MenuManager.main.gameOver("finished the demo");
            }
            
        }
        int index = Random.Range(0, eventPool.Count);
        currentEvent = eventPool[index];
        eventPool.RemoveAt(index);
        MenuManager.main.displayEvent(currentEvent.eventContent);
        MenuManager.main.updateEventCounter(eventPool.Count);
    }

    public void proceed()
    {
        foreach (Events newEvent in phaseList[currentPhase].pool)
        {
            eventPool.Add(newEvent);
        }
        refreshEvent();
    }
}

[System.Serializable]
public class phaseEvents 
{
    public string name;
    public Events[] pool;
}