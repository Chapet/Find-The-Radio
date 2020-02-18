using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sleep : MonoBehaviour
{
    //[SerializeField]
    private GameObject sleepText;
    private TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        sleepText = GameObject.Find("/Canvas/SleepText");
        text = sleepText.GetComponent<TextMeshPro>();
        Debug.Log(sleepText);
    }

    // Update is called once per frame
    void Update()
    {
        //sleepText.SetText(GetComponent<Slider>().value.ToString("0.0")) ;
    }

    public void OnValueChanged(float newValue)
    {
        Debug.Log(gameObject.GetComponent<Slider>().value);
        Debug.Log(newValue);
    }
}
