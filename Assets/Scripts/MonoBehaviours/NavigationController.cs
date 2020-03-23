using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    public float movementSpeed;
    public float stopDistance;
    public float rotationSpeed;
    public Transform destination;

    private Vector3 animVelocity;
    private Vector3 lastPosition;
    private bool reachedDestination;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != destination.position)
        {
            Vector3 destinationDirection = destination.position - transform.position;
            destinationDirection.y = 0;
            float destinationDistance = destinationDirection.magnitude;

            if(destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else
            {
                reachedDestination = true;
            }

            animVelocity = (transform.position - lastPosition) / Time.deltaTime;
            animVelocity.y = 0;
            var velocityMagnitude = animVelocity.magnitude;
            //animVelocity = animVelocity.normalized;

            var fwdDotProduct = Vector3.Dot(transform.forward, animVelocity);
            var rightDotProduct = Vector3.Dot(transform.right, animVelocity);

            animator.SetFloat("Speed", fwdDotProduct);
            animator.SetFloat("Direction", rightDotProduct);

            lastPosition = transform.position;
        }
    }


    public void setDestination(Transform dest)
    {
        destination = dest;
        reachedDestination = false;
    }


    
}
