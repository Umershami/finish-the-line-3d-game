using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Transform> waypoints; // List of waypoints
    public float speed = 10f;
    public float waypointRadius = 1f;
    private int currentWaypointIndex = 0;
    private bool isStopped = false;
    public Transform waypoint1;
    public Transform waypoint2;
    public GameObject eventToTrigger; // Event to trigger when passing between waypoints

    private bool passedWaypoint1 = false;

    void Update()
    {
        if (waypoints.Count == 0) return;

        if (isStopped) return; // Skip movement if the car is stopped

        // Get the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        float distance = direction.magnitude;

        if (distance > waypointRadius)
        {
            // Move towards the waypoint
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            // Rotate towards the waypoint
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }
        else
        {
            // Arrived at the waypoint, move to the next one
            currentWaypointIndex++;

            // Check if we've reached the end of the waypoints list
            if (currentWaypointIndex >= waypoints.Count)
            {
                // Stop the car
                isStopped = true;
                speed = 0f; // Optional: Set speed to 0 to ensure the car stops
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Adjust tag if needed
        {
            if (transform == waypoint1)
            {
                passedWaypoint1 = true;
            }
            else if (transform == waypoint2 && passedWaypoint1)
            {
                TriggerEvent();
                passedWaypoint1 = false; // Reset for future triggers if needed
            }
        }
    }

    void TriggerEvent()
    {
        if (eventToTrigger != null)
        {
            // Example: Trigger an event on the specified GameObject
            eventToTrigger.SetActive(true); // Activate the event GameObject
            // Add additional event logic here
        }
    }
}
