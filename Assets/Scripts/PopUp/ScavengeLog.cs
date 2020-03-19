using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScavengeLog : MonoBehaviour
{

    public TextMeshProUGUI logText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string log)
    {
        logText.SetText(log);
    }
}
