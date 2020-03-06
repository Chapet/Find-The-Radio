using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI message = null;
    
    // Start is called before the first frame update
    public void show(string msg)
    {
        this.message.SetText(msg);
        gameObject.SetActive(true);
    }

    public void OnClickOK()
    {
        gameObject.SetActive(false);
    }
}
