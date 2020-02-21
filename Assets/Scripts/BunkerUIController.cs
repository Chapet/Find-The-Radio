using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunkerUIController : MonoBehaviour
{
    public GameObject bedPanel;
    public GameObject scavengingPanel;
    private GameObject bunkerPanel;
    public GameObject inventoryPanel;
    public GameObject cheatPanel;

    public GameObject backPanel;
    // Start is called before the first frame update
    void Start()
    {
        bunkerPanel = gameObject;
        bedPanel.SetActive(false);
        scavengingPanel.SetActive(false);
        cheatPanel.SetActive(false);
        backPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SleepButtonClicked()
    {
        var fooGroup = Resources.FindObjectsOfTypeAll<GameObject>();
        if (fooGroup.Length > 0)
        {
            var foo = fooGroup[0];
        }
        Debug.Log("Going to bed ... zZzZ ");
        bedPanel.SetActive(true);
        backPanel.SetActive(true);
    }

    public void ScavengeButtonClicked()
    {
        var fooGroup = Resources.FindObjectsOfTypeAll<GameObject>();
        if (fooGroup.Length > 0)
        {
            var foo = fooGroup[0];
        }
        Debug.Log("Scavenge: depart into the unknown ...");
        scavengingPanel.SetActive(true);
        backPanel.SetActive(true);
    }

    public void GoToBunker()
    {
        bunkerPanel.SetActive(true);
        backPanel.SetActive(false);
        bedPanel.SetActive(false);
        scavengingPanel.SetActive(false);
    }

    public void CheatBtnClicked()
    {
        cheatPanel.SetActive(true);
        backPanel.SetActive(true);
    }
}
