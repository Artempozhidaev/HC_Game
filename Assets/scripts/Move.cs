using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject persona;
    public GameObject Button;

    public float Speed = 6f;
    public static float OffsetY = 0;
    public static float Offsetx = 0;
    public static bool finished = false;

    public string animationFloatname;

    bool _isGrounded;
    bool _isCyl_up;
    bool _isCyl_down;
    bool _isTri_up;
    bool _isTri_down;
    bool _isCube;
    bool _isFinish;

    Rigidbody rb;
    Animator anim;

    Vector3 moveDirection;
    Vector3 raycastFloorPos;
    Vector3 floorMovement;
    Vector3 gravity;
    Vector3 CombinedRaycast;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
        if (anim.GetBool("Move") == true)
        {
            moveDirection = new Vector3(Speed, 0, 0);
            
        }
        else
        {
            moveDirection = Vector3.zero;
            anim.SetFloat(animationFloatname, 0, 0.2f, Time.deltaTime);
        }
            
    }

    void FixedUpdate()
    {
        if (_isFinish == true)
        {
            anim.SetBool("Move", false);
            MovementLogic(0);
            anim.SetLayerWeight(1, 0);
            anim.SetFloat(animationFloatname, 0, 0.2f, Time.deltaTime);
            Button.SetActive(true);
            finished = true;
        }
        else if (_isTri_up == true)
        {
            if (Offsetx > OffsetY)
            {
                MovementLogic(6);
                anim.SetLayerWeight(1, 0);
                anim.SetFloat(animationFloatname, 1, 0.2f, Time.deltaTime);
            }
                
            else
            {
                MovementLogic(1);
                anim.SetLayerWeight(1, .5f);
                anim.SetFloat(animationFloatname, .5f, 0.2f, Time.deltaTime);
            }
                
        }
        else if (_isCube == true)
        {
            if (Offsetx < OffsetY)
            {
                MovementLogic(6 + (OffsetY - Offsetx));
                anim.SetLayerWeight(1, 0);
                anim.SetFloat(animationFloatname, 1, 0.2f, Time.deltaTime);
            }
                
            else
            {
                MovementLogic(6);
                anim.SetLayerWeight(1, 0);
                anim.SetFloat(animationFloatname, 1, 0.2f, Time.deltaTime);
            }
               
        }
        else if (_isTri_down == true)
        {
            if (Offsetx > OffsetY)
            {
                MovementLogic(6);
                anim.SetLayerWeight(1, 0);
                anim.SetFloat(animationFloatname, 1, 0.2f, Time.deltaTime);
            }
                
            else
            {
                MovementLogic(2);
                anim.SetLayerWeight(1, .5f);
                anim.SetFloat(animationFloatname, .5f, 0.2f, Time.deltaTime);
            }
                
        }
        else if (_isCyl_up == true)
        {
            if (Offsetx < OffsetY)
            {
                MovementLogic(6);
                anim.SetLayerWeight(1, 0);
                anim.SetFloat(animationFloatname, 1, 0.2f, Time.deltaTime);
            }
                
            else
            {
                MovementLogic(1);
                anim.SetLayerWeight(1, .5f);
                anim.SetFloat(animationFloatname, .5f, 0.2f, Time.deltaTime);
            }
                
        }
        else if (_isCyl_down == true)
        {
            if (Offsetx < OffsetY)
            {
                MovementLogic(6);
                anim.SetLayerWeight(1, 0);
                anim.SetFloat(animationFloatname, 1, 0.2f, Time.deltaTime);
            }
            else
            {
                MovementLogic(1);
                anim.SetLayerWeight(1, .5f);
                anim.SetFloat(animationFloatname, .5f, 0.2f, Time.deltaTime);
            }
                
        }
        else if ((_isGrounded == true) && (anim.GetBool("Move") == true))
        {
            if (Offsetx < OffsetY)
            {
                MovementLogic(6 + (OffsetY - Offsetx));
                anim.SetLayerWeight(1, 0);
                anim.SetFloat(animationFloatname, 1, 0.2f, Time.deltaTime);
            }
            else
            {
                MovementLogic(6);
                anim.SetLayerWeight(1, 0);
                anim.SetFloat(animationFloatname, 1, 0.2f, Time.deltaTime);
            }
                
        }
        else
        {
            if (anim.GetBool("Move") == true)
            {
                MovementLogic(6);
                anim.SetLayerWeight(1, 0);
                anim.SetFloat(animationFloatname, 1, 0.2f, Time.deltaTime);
            }
                
        }
    }

    private void MovementLogic(float speed)
    {
        // if not grounded , increase down force
        if (FloorRaycasts(0, 0, 2f) == Vector3.zero)
        {
            gravity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime;
        }

        // actual movement of the rigidbody + extra down force
        rb.velocity = (moveDirection * speed * Time.fixedDeltaTime) + gravity;
            
        // find the Y position via raycasts
        floorMovement = new Vector3(rb.position.x, FindFloor().y + OffsetY, rb.position.z);

        // only stick to floor when grounded
        if (FloorRaycasts(0, 0, 2f) != Vector3.zero && floorMovement != rb.position)
        {
            // move the rigidbody to the floor
            rb.MovePosition(floorMovement);
            gravity.y = 0;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        TagUpdate(other, true);
    }
    private void OnTriggerExit(Collider other)
    {
        TagUpdate(other, false);
    }
    


    private void TagUpdate(Collider other, bool value)
    {
        if (other.gameObject.tag == ("Ground"))
        {
            _isGrounded = value;

        }
        else if (other.gameObject.tag == ("tri_up"))
        {
            _isTri_up = value;

        }
        else if (other.gameObject.tag == ("tri_down"))
        {
            _isTri_down = value;

        }
        else if (other.gameObject.tag == ("cube"))
        {
            _isCube = value;

        }
        else if (other.gameObject.tag == ("cyl_up"))
        {
            _isCyl_up = value;

        }
        else if (other.gameObject.tag == ("cyl_down"))
        {
            _isCyl_down = value;

        }
        else if (other.gameObject.tag == ("finish"))
        {
            _isFinish = value;

        }

        
    }
    Vector3 FindFloor()
    {
        // width of raycasts around the centre of your character
        float raycastWidth = 0.25f;
        // check floor on 5 raycasts   , get the average when not Vector3.zero  
        int floorAverage = 1;

        CombinedRaycast = FloorRaycasts(0, 0, 1.8f);
        floorAverage += (getFloorAverage(raycastWidth, 0) + getFloorAverage(-raycastWidth, 0) + getFloorAverage(0, raycastWidth) + getFloorAverage(0, -raycastWidth));

        return CombinedRaycast / floorAverage;
    }
    int getFloorAverage(float offsetx, float offsetz)
    {

        if (FloorRaycasts(offsetx, offsetz, 1.8f) != Vector3.zero)
        {
            CombinedRaycast += FloorRaycasts(offsetx, offsetz, 1.8f);
            return 1;
        }
        else { return 0; }
    }


    Vector3 FloorRaycasts(float offsetx, float offsetz, float raycastLength)
    {
        RaycastHit hit;
        // move raycast
        raycastFloorPos = transform.TransformPoint(0 + offsetx, 0 + 0.5f, 0 + offsetz);

        Debug.DrawRay(raycastFloorPos, Vector3.down, Color.magenta);
        if (Physics.Raycast(raycastFloorPos, -Vector3.up, out hit, raycastLength))
        {
            return hit.point;
        }
        else return Vector3.zero;
    }
}
