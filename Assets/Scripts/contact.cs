using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contact : MonoBehaviour

{
    public GameObject contactRequest;
    public GameObject contactActions;
    public GameObject prefabReference;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void accept()
    {
        contactRequest.SetActive(false);
        contactActions.SetActive(true);
    }
    public void decline()
    {
        ContactManager.main.removeContact(gameObject, prefabReference);

    }

    public void shadyFunds()
    {
        ResourceManager.main.updateCredibility(-10);
        ResourceManager.main.updateRevenue(15);
    }
}
