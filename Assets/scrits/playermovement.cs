using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class playermovement : MonoBehaviour
{
    public float playerwalkingspeed = 5f;
    public float playerrunningspeed = 10f;
    public float jumpstre = 4f;
    CharacterController cc;
    float Verticalrotation = 0;
    float VerticalrotationLimit = 80f;
    float forwardmove;
    float sidewaymove;
    float verticalVelocity;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //rozgladanie sie
        float horizontalRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, horizontalRotation,0);
        //ROZGLADANIE SIE GORA DOL
        Verticalrotation -= Input.GetAxis("Mouse Y");
        Verticalrotation = Mathf.Clamp(Verticalrotation, -Verticalrotation, VerticalrotationLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(Verticalrotation, 0, 0);
        //poruszanie sie
        if (cc.isGrounded)
        {

            forwardmove = Input.GetAxis("Vertical") * playerwalkingspeed;
            sidewaymove = Input.GetAxis("Horizontal") * playerwalkingspeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                forwardmove = Input.GetAxis("Vertical") * playerrunningspeed;
                sidewaymove = Input.GetAxis("Horizontal") * playerrunningspeed;
            }
        }
        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        if(Input.GetButton("Jump") && cc.isGrounded)
        {
            verticalVelocity = jumpstre;
        }

        Vector3 playermovement = new Vector3(sidewaymove, verticalVelocity, forwardmove);
        cc.Move(transform.rotation * playermovement * Time.deltaTime);
    }
}
