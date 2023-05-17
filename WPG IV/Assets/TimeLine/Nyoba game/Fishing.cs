using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public Transform objectToFollow;
    public float followTime = 3f;
    public bool fishingevent = false;

    private Vector3 cursorPosition;
    public float timer;
    private bool isFollowing = false;

    void Event1()
    {
        if (isFollowing)
        {

            // Get the current position of the cursor
            Vector3 cursorPosition = Input.mousePosition;
            cursorPosition.z = Camera.main.transform.position.y;

            // Increment the timer
            timer += Time.deltaTime;

            // Check if the follow time has expired
            if (timer >= followTime)
            {
                // Stop following the object
                timer = 0f;
                fishingevent = false;

                // Do something else, like catch the object
                Debug.Log("Object caught!");
            }
        }
        timer = Mathf.Max(timer, 0);
        timer -= Time.deltaTime/2;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            fishingevent = true;
            Event1();
        }
        if (fishingevent) Event1();
    }

    void OnMouseEnter()
    {
        // Start following the object
        isFollowing = true;
    }

    void OnMouseExit()
    {
        // Check if the player exits the collider before the follow time has expired
        if (timer < followTime)
        {
            // Stop following the object and reset the timer
            isFollowing = false;
        }
    }
}