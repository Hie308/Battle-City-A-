using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public Transform target;
    public float range = 15f;
    public string targetTag = "Player"; 

    public float fireRate = 1f;
    float fireCountdown = 0f;
    public GameObject Bullet;
    public Transform firePoint;
    public Transform RotateHead; 

	// Use this for initialization
	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);// unity

	}
	
    void UpdateTarget()
    {
        GameObject[] targettt = GameObject.FindGameObjectsWithTag(targetTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;
        foreach (GameObject targetX in targettt)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetX.transform.position);
            if(distanceToTarget < shortestDistance)
            {
                shortestDistance = distanceToTarget;
                nearestTarget = targetX;
            }

        }
        if (nearestTarget != null && shortestDistance <= range)
        {
            target = nearestTarget.transform;
        }
        else target = null;

    }
	// Update is called once per frame
	void Update () {
        if (target == null)
            return;
        // ROtation moment head of turret -> player 
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        RotateHead.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
	}
    void Shoot()
    {
       GameObject bulletGO =(GameObject) Instantiate(Bullet, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seek(target);

    }
     void OnDrawGizmosSelected()// unity
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, range);
    }
}
