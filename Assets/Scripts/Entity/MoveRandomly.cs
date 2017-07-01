using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandomly : MonoBehaviour 
{
    public float radius;
    [SerializeField] private float moveSpeed;

    private Vector3 center;
    private Vector3 currentWaypoint;

    private bool HasReachedCurrentWaypoint()
    {
        const float CLOSE_ENOUGH = 0.1f;
        return Vector3.Distance(transform.position, currentWaypoint) < CLOSE_ENOUGH;
    }

    private void Start()
    {
        currentWaypoint = center = transform.position;
    }

	// Update is called once per frame
	private void Update () 
    {
		if(HasReachedCurrentWaypoint())
        {
            Vector2 randomInsideCircle = Random.insideUnitCircle;
            currentWaypoint = center + new Vector3(randomInsideCircle.x, 0f, randomInsideCircle.y);
        }

        var dir = (currentWaypoint - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
