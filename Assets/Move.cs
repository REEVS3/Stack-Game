using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    public static Move Current { get; private set; }
    public static Move Last { get; private set; }
    
    public MoveDirection MoveDirection { get; internal set; }

    private void OnEnable()
    {
        if (Last == null)
            Last = GameObject.Find("Start").GetComponent<Move>();
        Current = this;
        GetComponent<Renderer>().material.color = GetRandColor();

        transform.localScale = new Vector3(Last.transform.localScale.x,transform.localScale.y,Last.transform.localScale.z);

    }

    private Color GetRandColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }





    internal void Stop()
    {
        speed = 0;
        float hang = GetHang();
        float max = MoveDirection == MoveDirection.Z ? Last.transform.localScale.z : Last.transform.localScale.x;
        if (Mathf.Abs(hang) >= max)
        {
            Last = null;
            Current = null;
            SceneManager.LoadScene("Final");
        }



        Debug.Log(speed);
        float direction = hang > 0 ? 1f : -1f;

        if (MoveDirection == MoveDirection.Z)
            CutcubeZ(hang, direction);
        else
            CutcubeX(hang, direction);

        Last = this;
    }

    private float GetHang()
    {
        if (MoveDirection == MoveDirection.Z)
        return transform.position.z - Last.transform.position.z;
        else
        return transform.position.x - Last.transform.position.x;
    }

    private void CutcubeX(float hang, float direction)
    {
        float newxsize = Last.transform.localScale.x - Mathf.Abs(hang);
        float fallingcube = transform.localScale.x - newxsize;

        float newxPos = Last.transform.position.x + (hang / 2);
        transform.localScale = new Vector3(newxsize,transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newxPos, transform.position.y,transform.position.z );

        float cubeEdge = transform.position.x + (newxsize / 2f * direction);
        float fallingcubexpos = cubeEdge + fallingcube / 2f * direction;


        SpawnCube(fallingcubexpos, fallingcube);
    }



    private void CutcubeZ(float hang,float direction)
    {
        float newZsize = Last.transform.localScale.z - Mathf.Abs(hang);
        float fallingcube = transform.localScale.z - newZsize;

        float newZPos = Last.transform.position.z + (hang / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZsize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPos);

        float cubeEdge = transform.position.z + (newZsize / 2f * direction);
        float fallingcubezpos = cubeEdge + fallingcube / 2f * direction ;


        SpawnCube(fallingcubezpos, fallingcube);
    }

    private void SpawnCube(float fallingcubezpos, float fallingcube )
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (MoveDirection == MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingcube);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingcubezpos);
        }
        else
        {
            cube.transform.localScale = new Vector3(fallingcube, transform.localScale.y,transform.localScale.z);
            cube.transform.position = new Vector3(fallingcubezpos, transform.position.y,transform.position.z);

        }
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        
        //Destroy(cube.gameObject, 1f);

    }

    // Update is called once per frame
    private void Update()
    {
        if (MoveDirection == MoveDirection.Z)
            transform.position += transform.forward * Time.deltaTime * speed;
        else
            transform.position += transform.right * Time.deltaTime * speed;
    }
}
