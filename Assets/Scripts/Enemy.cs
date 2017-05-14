using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10.0f;

    private Transform target;
    private int wavePointIndex = 0;


    private void Start()
    {
        target = WayPoints.wayPoints[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (wavePointIndex >= WayPoints.wayPoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavePointIndex++;
        target = WayPoints.wayPoints[wavePointIndex];
    }
}