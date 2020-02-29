using System;
using UnityEngine;


//all this class com from Firebas tutorial

public class CrashlyticsTester : MonoBehaviour {

    int updatesBeforeException;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("Create Error");
        updatesBeforeException = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Call the exception-throwing method here so that it's run
        // every frame update
        throwExceptionEvery60Updates();
    }

    // A method that tests your Crashlytics implementation by throwing an
    // exception every 60 frame updates. You should see non-fatal errors in the
    // Firebase console a few minutes after running your app with this method.
    void throwExceptionEvery60Updates()
    {
        if (updatesBeforeException > 0)
        {
            updatesBeforeException--;
        }
        else
        {
            // Set the counter to 60 updates
            updatesBeforeException = 60;
            
            
            /*var message = new AndroidJavaObject("java.lang.String", "This is a test crash, ignore.");
            var exception = new AndroidJavaObject("java.lang.Exception", message);
       
            var looperClass = new AndroidJavaClass("android.os.Looper");
            var mainLooper = looperClass.CallStatic<AndroidJavaObject>("getMainLooper");
            var mainThread = mainLooper.Call<AndroidJavaObject>("getThread");
            var exceptionHandler = mainThread.Call<AndroidJavaObject>("getUncaughtExceptionHandler");
            exceptionHandler.Call("uncaughtException", mainThread, exception);
            */

            // Throw an exception to test your Crashlytics implementation
            throw new System.Exception("test exception please ignore");
            
        }
    }
}