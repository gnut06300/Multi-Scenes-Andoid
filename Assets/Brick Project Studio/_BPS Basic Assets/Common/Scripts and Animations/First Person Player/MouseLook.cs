using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
    public class MouseLook : MonoBehaviour
    {

        public float mouseXSensitivity = 100f;

        public Transform playerBody;

        float xRotation = 0f;
        [SerializeField] private FixedJoystick fixedJoystick;

        // Start is called before the first frame update
        void Start()
        {
            //Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
            /*float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseXSensitivity * Time.deltaTime;*/

            float mouseX = fixedJoystick.Horizontal * mouseXSensitivity * Time.deltaTime;
            float mouseY = fixedJoystick.Vertical * mouseXSensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}