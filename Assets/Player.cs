using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    ResourceAbsorbingZone zone;
    [SerializeField]
    GameObject leftStick;
    [SerializeField]
    float speedModifier = 3;
    [SerializeField]
    BackpackStack backPack;

    Joystick joystick;
    Animator animator;
    CharacterController characterController;


    void Start()
    {
        Application.targetFrameRate = 60;

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        joystick = leftStick.GetComponent<Joystick>();
        zone.OnResourceCollision += ResourceCollisionHandler;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 NextDir = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        if (NextDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(NextDir);
        }
        characterController.Move(NextDir * Time.fixedDeltaTime * speedModifier);
        animator.SetFloat("Speed", characterController.velocity.magnitude);
    }

    public bool Staying { get => characterController.velocity.magnitude <= .01; }

    void ResourceCollisionHandler (object sender, Resource resource)
    {
        if (!backPack.Full) backPack.receive(resource);
    }
}
