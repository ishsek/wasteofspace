using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterScript : MonoBehaviour
{
    public int TargetFrameRate = 60;
    public string SceneName = "World";

    void Start()
    {
        Application.targetFrameRate = TargetFrameRate;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
