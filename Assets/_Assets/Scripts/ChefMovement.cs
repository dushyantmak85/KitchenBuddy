using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ChefSimpleMovement : MonoBehaviour
{
    public float speed = 3f;
    public float rotationSpeed = 10f;
    private CharacterController controller;
    private Animator animator;

    public Transform animatorRoot; // drag Game_Rig here in Inspector

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = animatorRoot.GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(h, 0, v);
        float inputMagnitude = Mathf.Clamp01(input.magnitude);

        // Move character
        Vector3 move = input.normalized * speed;
        controller.SimpleMove(move);

        // Rotate if moving
        if (input != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(input);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Update animator blend value
        animator.SetFloat("Speed", inputMagnitude);
    }
}
