using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoCaller : MonoBehaviour
{
    public bool load;
    public bool save;
    private Level_Data data;
    // Start is called before the first frame update
    void Start()
    {
        if(load)
            Gamemaster.Instance.GetLevel().LoadLevelFromFile("testLevel", "TestLevels");
        else
            Gamemaster.Instance.GetLevel().CreateNewLevel(20, 20, "testLevel");
        data = Gamemaster.Instance.GetLevel().GetLevelData();
    }

    private void OnApplicationQuit()
    {
        if(save)
            LevelSaveLoad.Save(data, "TestLevels");
    }

}
