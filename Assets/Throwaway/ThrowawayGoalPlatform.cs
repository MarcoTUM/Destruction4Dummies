using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThrowawayGoalPlatform : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && nextLevelName != "")
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
