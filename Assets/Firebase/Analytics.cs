using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analytics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Firebase.Analytics.FirebaseAnalytics
            .LogEvent(
                Firebase.Analytics.FirebaseAnalytics.EventJoinGroup,
                Firebase.Analytics.FirebaseAnalytics.ParameterGroupId,
                "Test Analytics"
            );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
