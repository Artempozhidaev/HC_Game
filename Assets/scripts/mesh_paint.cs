using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class mesh_paint : MonoBehaviour
{
    public LineRenderer Line;
    public LineRenderer Line_left;
    public LineRenderer Line_right;

    public GameObject leg_left;
    public GameObject leg_right;

    public CapsuleCollider capsuleCollider;

    public Animator animator;

    void Start()
    {
        Line.startWidth = 0.02f;
        Line.endWidth = 0.02f;
        Line.positionCount = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        try
        {
            if (Input.GetMouseButton(0))
            {
                var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Move.finished == false)
                    animator.SetBool("Move", true); ;

                RaycastHit hit;

                if (Physics.Raycast(Ray, out hit) && (hit.collider.tag == "draw"))
                {
                    Time.timeScale = 0.01f;
                    Time.fixedDeltaTime = Time.timeScale * 0.02f;

                    Line.positionCount++;
                    Line.SetPosition(Line.positionCount - 1, hit.point);
                }
            }
            else

            {
                GeneradeMesh();

                Line.positionCount = 0;

                Time.timeScale = 1f;
                Time.fixedDeltaTime = .02f;
            }
        }
        catch
        {

        }
    }
    void GeneradeMesh()
    {

        Vector3 point_max_Y;
        Vector3 point_min_Y;

        Vector3 point_max_x;
        Vector3 point_min_x;

        // получение массива точек рисунка
        Vector3[] array_points = new Vector3[Line.positionCount];

        Vector3[] array_points_zero = new Vector3[Line.positionCount];
        Vector3[] array_points_zero_ = new Vector3[Line.positionCount];

        for (int i = 0; i < array_points.Length; i++)
            array_points[i] = Line.GetPosition(i);

        // нахождение самой высокой и самой низкой точки
        point_max_Y = array_points[0];
        point_min_Y = array_points[0];
        point_max_x = array_points[0];
        point_min_x = array_points[0];

        for (int i = 1; i < array_points.Length; i++)
        {
            if (array_points[i].y > point_max_Y.y)
            {

                point_max_Y = array_points[i];
            }
            if (array_points[i].y < point_min_Y.y)
            {

                point_min_Y = array_points[i];
            }
            if (array_points[i].x > point_max_Y.x)
            {

                point_max_x = array_points[i];
            }
            if (array_points[i].x < point_min_Y.x)
            {

                point_min_x = array_points[i];
            }
        }

        //Массив где 0 это высшая точка кривой
        for (int i = 0; i < array_points.Length; i++)
        {
            array_points_zero[i] = new Vector3(point_max_Y.x - array_points[i].x, array_points[i].y - point_max_Y.y, 0);
            array_points_zero_[i] = new Vector3(array_points[i].x - point_max_Y.x, array_points[i].y - point_max_Y.y, 0);
        }

        capsuleCollider.height = 1.33f + (point_max_Y.y - point_min_Y.y) * 2;
        capsuleCollider.transform.position += Vector3.up * ((point_max_Y.y - point_min_Y.y));

        Move.OffsetY = point_max_Y.y - point_min_Y.y;
        Move.Offsetx = point_max_x.x - point_min_x.x;

        line_create(array_points_zero);
    }

    void line_create(Vector3[] positions)
    {

        Line_left.positionCount = positions.Length - 1;
        Line_left.SetPositions(positions);
        Line_left.startWidth = 0.05f;
        Line_left.endWidth = 0.05f;

        Line_right.positionCount = positions.Length - 1;
        Line_right.SetPositions(positions);
        Line_right.startWidth = 0.05f;
        Line_right.endWidth = 0.05f;
    }

}