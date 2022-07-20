using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandling : MonoBehaviour
{
    Animator anim;

    public float ikweight = 1;

    public Transform leftIKTarget;
    public Transform rightIKTarget;
    
    public Transform hintLeft;
    public Transform hintRight;

    Vector3 leftFootPos;
    Vector3 rightFootPos;

    Quaternion leftFootRot;
    Quaternion rightFootRot;

    float lfWeight;
    float rfWeight;
    float offset = 0.15f;

    Transform leftFoot;
    Transform rightFoot;

    void Start()
    {
        anim = GetComponent<Animator>();

        leftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);

        leftFootPos = leftFoot.transform.position;
        rightFootPos = rightFoot.transform.position;

        leftFootRot = leftFoot.rotation;
        rightFootRot = rightFoot.rotation;
    }

    void Update()
    {
        RaycastHit lefthit;
        RaycastHit righthit;

        Vector3 leftpos = leftFoot.TransformPoint(Vector3.zero);
        Vector3 rightpos = rightFoot.TransformPoint(Vector3.zero);

        if (Physics.Raycast(leftpos, -Vector3.up,out lefthit, 4))
        {
            
            if ((lefthit.transform.tag == "tri_down"))
            {
                leftFootPos = lefthit.point + new Vector3(0, Move.OffsetY, 0);
                leftFootRot = Quaternion.FromToRotation(transform.up, lefthit.normal) * transform.rotation;
            }
            else if ((lefthit.transform.tag == "tri_up"))
            {
                leftFootPos = lefthit.point + new Vector3(0, Move.OffsetY, 0);
                leftFootRot = Quaternion.FromToRotation(transform.up, lefthit.normal) * transform.rotation;
            }
            else
            {
                leftFootPos = lefthit.point;
                leftFootRot = Quaternion.FromToRotation(transform.up, lefthit.normal) * transform.rotation;
            }
            
        }

        if (Physics.Raycast(rightpos, -Vector3.up,out righthit, 4))
        {
            if ((lefthit.transform.tag == "tri_down"))
            {
                rightFootPos = righthit.point + new Vector3(0, Move.OffsetY, 0);
                rightFootRot = Quaternion.FromToRotation(transform.up, righthit.normal) * transform.rotation;
            }
            else if ((lefthit.transform.tag == "tri_up"))
            {
                rightFootPos = righthit.point + new Vector3(0, Move.OffsetY, 0);
                rightFootRot = Quaternion.FromToRotation(transform.up, righthit.normal) * transform.rotation;
            }
            else
            {
                rightFootPos = righthit.point;
                rightFootRot = Quaternion.FromToRotation(transform.up, righthit.normal) * transform.rotation;
            }

        }
    }
    private void OnAnimatorIK(int layerIndex)
    {
        lfWeight = anim.GetFloat("RightFoot");
        rfWeight = anim.GetFloat("LeftFoot");

        


        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, lfWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, rfWeight);

        anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos + new Vector3(0, offset, 0)) ;
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos + new Vector3(0, offset, 0));

        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, lfWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, rfWeight);

        anim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRot);
        anim.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRot);
    }
}
//anim.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, ikweight);
//anim.SetIKHintPositionWeight(AvatarIKHint.RightKnee, ikweight);

//anim.SetIKHintPosition(AvatarIKHint.LeftKnee, hintLeft.position);
//anim.SetIKHintPosition(AvatarIKHint.RightKnee, hintRight.position);
