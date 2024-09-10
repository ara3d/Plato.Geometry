using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {
    float MouseRotateSpeed = 80f;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        var y = Input.GetAxis("Mouse X") * MouseRotateSpeed * Time.deltaTime;
        var x = Input.GetAxis("Mouse Y") * MouseRotateSpeed * Time.deltaTime;
        transform.Rotate(-x, y, 0);

        var Angles = Camera.main.transform.eulerAngles;
        transform.eulerAngles = new Vector3(Angles.x, Angles.y, 0);
    }
}
