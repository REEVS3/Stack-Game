using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubespawn : MonoBehaviour
{
    [SerializeField]
    private Move cubePrefab;
    [SerializeField]
    private MoveDirection moveDirection;

    public void SpawnCube()
    {

        var cube = Instantiate(cubePrefab);
        if (Move.Last != null && Move.Last.gameObject != GameObject.Find("Start"))
        {
            float x = moveDirection == MoveDirection.X ? transform.position.x : Move.Last.transform.position.x;
            float z = moveDirection == MoveDirection.Z ? transform.position.z : Move.Last.transform.position.z;


            cube.transform.position = new Vector3(x,
            Move.Last.transform.position.y + cubePrefab.transform.localScale.y,
            z);
        }
        else
        {
            cube.transform.position = transform.position;

        }

        cube.MoveDirection = moveDirection;

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }

}
