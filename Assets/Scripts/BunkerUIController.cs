using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunkerUIController : MonoBehaviour
{
    public GameObject BedPanel;
    // Start is called before the first frame update
    void Start()
    {
        BedPanel.SetActive(false);
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
        BedPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
