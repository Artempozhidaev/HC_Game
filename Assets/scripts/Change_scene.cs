using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_scene : MonoBehaviour
{
    public Scene scene;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (SceneManager.GetActiveScene().name == "Start_scene")
            SceneManager.LoadScene("game_scene", LoadSceneMode.Single);
        else
        {
            Move.finished = false;
            SceneManager.LoadScene("Start_scene", LoadSceneMode.Single);
        }
            
    }
}