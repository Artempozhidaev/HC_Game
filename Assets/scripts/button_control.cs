using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_control : MonoBehaviour
{
    //private Movment m;
    public GameObject gObject;
    public Animator anim;
    public void onClick()
    {
        anim.SetBool("Move", true);
        gObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
