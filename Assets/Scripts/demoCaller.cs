using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoCaller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Gamemaster.Instance.GetLevel().CreateNewLevel(8, 4, "testLevel");
        //Gamemaster.Instance.GetLevel().LoadLevelFromFile("testLevel", "TestLevels");
    }
    
}
