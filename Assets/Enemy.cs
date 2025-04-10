using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float health = 20f;

    public WaveSpawner waveSpawner;
    public int tier;

    private Transform target;
    private int movepointIndex;

    [SerializeField]
    private Material tier1;

    [SerializeField]
    private Material tier2;

    // Start is called before the first frame update
    void Start()
    {
        target = MovePoints.points[0];
        switch(tier)
        {
            case 0:
                break;
            case 1:
                health *= 1.5f;
                this.GetComponentInChildren<MeshRenderer>().material = tier1;
                break;
            case 2:
                health *= tier;
                this.GetComponentInChildren<MeshRenderer>().material = tier2;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.02f)
        {
            GetNextMovepoint();
        }
    }

    void GetNextMovepoint()
    {
        if (movepointIndex >= MovePoints.points.Length - 1)
        {
            target.GetComponent<Defender>().TakeDamage(health / 2);
            Destroy(this.gameObject);
        }
        else
        {
            movepointIndex++;
            target = MovePoints.points[movepointIndex];
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        waveSpawner.incrementScore((int)damage);

        if(health <= 0)
        {
            waveSpawner.incrementScore(100);
            waveSpawner.incrementKillCount();
            Destroy(gameObject);
        }
    }
}
