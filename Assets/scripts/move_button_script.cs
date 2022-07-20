using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_button_script : MonoBehaviour
{
    public GameObject gObject;
    public Animator anim;
    public void onClick()
    {
        if (anim.GetBool("Move") == false)
            anim.SetBool("Move", true);
        else
            anim.SetBool("Move", false);
        
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
