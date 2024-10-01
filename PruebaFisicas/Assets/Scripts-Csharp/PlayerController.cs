//  Code by Yeahlowflicker
//  for the Unity First Person Controller Series
//  of the YouTube channel Yeahlowflicker.
//
//  Feel free to visit and support my channel and website. Many thanks.
//  https://www.youtube.com/channel/UCATag7uzDpitt7lZ_9hdp4Q
//  https://yeahlowflicker.com
//
//  This code is distributed under the MIT License.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("References")] //este atributo [Header("Texto")] solo sirve para ordenar en listas las variables publicas que vamos a ver en el inspector del editor de Unity 
    public Rigidbody rb;
    public Transform head; //esta variable solo tomara en cuenta la posicion rotacion y escala del gameobject "Head"
    public Camera camara;


    [Header("Configurations")]
    public float walkSpeed;
    public float runSpeed;


    [Header("Runtime")]
    Vector3 newVelocity;



    // Start is called before the first frame update
    void Start() {
        //  Oculta la flecha del mouse usando el método Cursor de la clase UnityEngine.
        // https://docs.unity3d.com/ScriptReference/Cursor.html
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }




    // Update is called once per frame
    void Update() {
        // Horizontal rotation
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);   // Adjust the multiplier for different rotation speed

        newVelocity = Vector3.up * rb.velocity.y;
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;   
        newVelocity.x = Input.GetAxis("Horizontal") * speed;
        newVelocity.z = Input.GetAxis("Vertical") * speed;

        rb.velocity = transform.TransformDirection(newVelocity);
    }


    void LateUpdate() {
        // Vertical rotation
        Vector3 e = head.eulerAngles;
        e.x -= Input.GetAxis("Mouse Y") * 2f;   //  Edit the multiplier to adjust the rotate speed
        e.x = RestrictAngle(e.x, -85f, 85f);    //  This is clamped to 85 degrees. You may edit this.
        head.eulerAngles = e;
    }



    //  A helper function
    //  Clamp the vertical head rotation (prevent bending backwards)
    public static float RestrictAngle(float angle, float angleMin, float angleMax) {
        if (angle > 180)
            angle -= 360;
        else if (angle < -180)
            angle += 360;

        if (angle > angleMax)
            angle = angleMax;
        if (angle < angleMin)
            angle = angleMin;

        return angle;
    }
}