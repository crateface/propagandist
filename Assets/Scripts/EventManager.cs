using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Events[] startingEvents;
    public Events[] secondPhase;
    public List<Events> eventPool;
    public static EventManager main;
    // Start is called before the first frame update
    public void Awake()
    {
        main = this;
    }
    void Start()
    {
        eventPool = new List<Events>();
        foreach (Events newEvent in startingEvents)
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
        { proceedNextPhase(); return; }
        int index = Random.Range(0, eventPool.Count);
        Events currentEvent = eventPool[index];
        eventPool.RemoveAt(index);
        MenuManager.main.displayEvent(currentEvent.eventContent);
        MenuManager.main.updateEventCounter(eventPool.Count);
    }

    public void proceedNextPhase()
    {
        
    }
}
