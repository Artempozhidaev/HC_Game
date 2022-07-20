using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generator : MonoBehaviour
{
    public GameObject cube, cylinder, triangle, cube_finish;
    public Transform parent;
    public int Levelnum;
    private float LevelDistance;
    private int LevelPoints;
    private bool run = true;
    // Start is called before the first frame update
    void Start()
    {
        if (run == true)
        {
            SpawnMap();
        }
        else
        {
            return;
        }
        
    }
    void SpawnMap()
    {
        _spawnLevelPoints(Levelnum);

    }
    void _spawnLevelPoints(int _levelnum)
    {
        if (_levelnum == 0)
        {
            LevelDistance = 40f;
            LevelPoints = 4;
        }
        else
        {
            if (_levelnum == 1)
            {
                LevelDistance = 50f;
                LevelPoints = 4;
            }
            else
            {
                if (_levelnum == 2)
                {
                    LevelDistance = 60f;
                    LevelPoints = 4;
                }
                else
                {
                    if (_levelnum == 3)
                    {
                        LevelDistance = 70f;
                        LevelPoints = 5;
                    }
                    else
                    {
                        LevelDistance = 80f;
                        LevelPoints = 6;
                    }
                }
            }
        }
        Vector3[] masLevelPoints = new Vector3[LevelPoints];
        for (int i = 0; i < LevelPoints;i++)
        {
            masLevelPoints[i] = new Vector3((i *10f)-3,2f,0);

        }

        for (int i = 0; i < LevelPoints- 1; i++)
            generade(Random.Range(0, 3), masLevelPoints[i]);
        generade(3, masLevelPoints[masLevelPoints.Length - 1]);

    }


    void generade(int ground, Vector3 position)
    {
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
        Quaternion rotation_90 = new Quaternion(0, 1, 0, 0);
        Quaternion rotation_cylinder_up = new Quaternion(0, 0.707106829f, 0.707106829f, 0);
        Quaternion rotation_cylinder_down = new Quaternion(90, 0, 0, -90);
        

        if (ground == 0) //tri + cube + tri
        {
            Vector3 pos_for_cube = new Vector3(position.x + 2f, position.y, position.z);
            Vector3 pos_for_last_triangle = new Vector3(pos_for_cube.x +2f, pos_for_cube.y, pos_for_cube.z);

            GameObject triangle_up = Instantiate(triangle, position, rotation, parent);
            GameObject cube_new = Instantiate(cube, pos_for_cube, rotation, parent);
            GameObject triangle_down = Instantiate(triangle, pos_for_last_triangle, rotation_90, parent);

            triangle_up.tag = "tri_up";
            cube_new.tag = "cube";
            triangle_down.tag = "tri_down";

            
        }
        else if (ground == 1) //tri + tri
        {
            Vector3 pos_for_last_triangle = new Vector3(position.x + 2f, position.y, position.z);

            GameObject triangle_up = Instantiate(triangle, position, rotation, parent);
            GameObject triangle_down = Instantiate(triangle, pos_for_last_triangle, rotation_90, parent);

            triangle_up.tag = "tri_up";
            triangle_down.tag = "tri_down";

            transform.SetPositionAndRotation(pos_for_last_triangle, rotation_90);
        }
        else if(ground == 2) //cylinder
        {
            Vector3 position_for_cyl = new Vector3(position.x, 1.19f, position.z);
            
            GameObject cylinder_up = Instantiate(cylinder, position_for_cyl, rotation_cylinder_up, parent);
            GameObject cylinder_down = Instantiate(cylinder, position_for_cyl, rotation_cylinder_down, parent);
            cylinder_up.tag = "cyl_up";
            cylinder_down.tag = "cyl_down";
        }
        else if (ground == 3)
        {
            Vector3 position_for_finish = new Vector3(position.x, position.y + 1f, position.z);

            GameObject finish = Instantiate(cube_finish, position_for_finish, rotation, parent);
            finish.tag = "finish";
        }
        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
