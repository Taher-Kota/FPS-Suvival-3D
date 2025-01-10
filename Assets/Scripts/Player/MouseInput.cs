using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private Vector2 current_Mouse;
    private Vector3 look_Angle;
    [SerializeField]
    private float Sensivity = 5f;
    [SerializeField]
    private Vector2 default_look_angle = new Vector2(-70f,50f);

    public Transform PlayerRoot, LookRoot;

    [SerializeField]
    private bool invert;

    //[SerializeField]
    //private float roll_Angle ,Curren_RollAngle, roll_Speed = 3f;
    //
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MouseMovement();
    }

    private void MouseMovement()
    {
        current_Mouse = new Vector2(Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"));
        look_Angle.x += current_Mouse.x * Sensivity * (invert ? 1f:-1f) ;
        look_Angle.y += current_Mouse.y * Sensivity ;

        look_Angle.x = Mathf.Clamp(look_Angle.x, default_look_angle.x, default_look_angle.y);
        //To Show Drunken Visualization put this value in ZAxix;
        //Curren_RollAngle = Mathf.Lerp(Curren_RollAngle, Input.GetAxisRaw("Mouse X") * roll_Angle, roll_Speed * Time.deltaTime);
        PlayerRoot.localRotation = Quaternion.Euler(0f,look_Angle.y,0f);
        LookRoot.localRotation = Quaternion.Euler(look_Angle.x,0f,0f);
    }
}
