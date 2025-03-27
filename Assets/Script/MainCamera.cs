using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform mainCharacter_transform;
    public float speed;
    public Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Move();
    }

    private void Move()
    {
        var nextPosition = Vector3.Lerp(transform.position, mainCharacter_transform.position + _offset, Time.deltaTime * speed);
        transform.position = nextPosition;
    }
}
