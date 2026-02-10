using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FloatingJoystick joystick;
    public float speed = 5f;
    public float rotationSpeed = 10f;
    public Transform cameraTransform;

    void FixedUpdate()
    {
        Vector3 input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        if (input.magnitude > 0.1f)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            Vector3 moveDirection = forward * input.z + right * input.x;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            transform.Translate(moveDirection * speed * Time.fixedDeltaTime, Space.World);
        }
    }
}