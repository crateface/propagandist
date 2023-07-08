using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactManager : MonoBehaviour
{
    GameObject[] contactArray;
    public List<GameObject> contactPrefabs;
    public Transform[] contactPanels;
    public static ContactManager main;
    public void Awake()
    {
        main = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        contactArray = new GameObject[4];
        generateContact();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateContact()
    {
        if (contactPrefabs.Count <= 0)
        {
            return;
        }
        int chance = Random.Range(1, 100);
        if (chance>0) 
        {
            int index = Random.Range(0, contactPrefabs.Count - 1);
            GameObject selected = contactPrefabs[index];
            contactPrefabs.Remove(selected);
            for (int i = 0; i<contactArray.Length; i++)
            {
                if (contactArray[i] == null)
                {
                    GameObject instance = Instantiate(selected, contactPanels[i].position, Quaternion.identity, contactPanels[i]);
                    instance.GetComponent<contact>().prefabReference = selected;
                    contactArray[i] = instance;
                    break;
                }
            }
        }
    }

    public void removeContact(GameObject contactRemoval, GameObject originalPrefab)
    {
        for (int i = 0; i < contactArray.Length; i++)
        {
            if (contactArray[i] == contactRemoval)
            {
                contactPrefabs.Add(originalPrefab);
                Destroy(contactArray[i]);
                contactArray[i] = null;
                break;
            }
        }
    }
}
