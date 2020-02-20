using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunkerUIController : MonoBehaviour
{
    public GameObject bedPanel;
    private GameObject bunkerPanel;
    public GameObject inventoryPanel;

    public GameObject backPanel;
    // Start is called before the first frame update
    void Start()
    {
        bunkerPanel = gameObject;
        bedPanel.SetActive(false);
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
    public void GoToBunker()
    {
        bunkerPanel.SetActive(true);
        backPanel.SetActive(false);
        bedPanel.SetActive(false);
    }
}
