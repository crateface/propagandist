using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contact : MonoBehaviour

{
    public GameObject contactRequest;
    public GameObject contactActions;
    public GameObject prefabReference;
    public string name;
    public string type;
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
        MenuManager.main.addToContactDropdown(name);
    }
    public void decline()
    {        
        MenuManager.main.removeFromContactDropdown(name);
        ContactManager.main.removeContact(this, prefabReference);

    }

    public void shadyFunds()
    {
        ResourceManager.main.updateCredibility(-10);
        ResourceManager.main.updateRevenue(15);
        decline();
    }

    public void promote()
    {
        ResourceManager.main.updateOutreach(15);
        decline();
    }

    public void insure()
    {
        ResourceManager.main.hired("you");
        ResourceManager.main.updateRevenue(-10);
        decline();
    }

    public void hireanalyst(string which)
    {
        ResourceManager.main.hired(which);
        ResourceManager.main.updateRevenue(-15);
        decline();
    }
    
    public void predict()
    {
        ResourceManager.main.updateRevenue(-5);
        ResourceManager.main.updateCredibility(-10);
        ResourceManager.main.updateOutreach(15);
        decline();
    }
}
