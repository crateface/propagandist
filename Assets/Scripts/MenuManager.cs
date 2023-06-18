using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject resourceMenu;
    public GameObject contactMenu;
    // Start is called before the first frame update
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
}
