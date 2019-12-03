using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoCaller : MonoBehaviour
{
    private Level_Data data;
    // Start is called before the first frame update
    void Start()
    {
        Gamemaster.Instance.GetLevel().CreateNewLevel(20, 20, "testLevel");
        data = Gamemaster.Instance.GetLevel().GetLevelData();
        //Gamemaster.Instance.GetLevel().LoadLevelFromFile("testLevel", "TestLevels");
    }

    private void OnApplicationQuit()
    {
        LevelSaveLoad.Save(data, FilePaths.CustomLevelFolder);
    }

}
