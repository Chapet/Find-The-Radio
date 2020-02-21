using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "MyAsset/Player")]

public class Player : ScriptableObject
{
    
    public  int currentHealth;
    public int currentHunger;
    public int currentThirst;
    public int currentEnergy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
