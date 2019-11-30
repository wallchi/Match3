using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBoard : MonoBehaviour
{
    public Board board;
    void Start()
    {
        Vector3 temp = new Vector3();
        temp.z = transform.position.z;
        temp.x = ((float)board.mWidth / 2) - 0.5f;
        temp.y = 4;
        transform.position = temp;
    }
}
