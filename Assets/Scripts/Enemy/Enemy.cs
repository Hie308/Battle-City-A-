using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity {


    public ParticleSystem deathEffect;

	protected override void Start()
    {
        base.Start();
    }

    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)//ep15
    {
        if (damage >= health)
        {
            Destroy(Instantiate(deathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, 1);
        }
        base.TakeHit(damage, hitPoint, hitDirection);
    }
    void FixedUpdate()
    {
        if (dead == false)
            return;
    }
}
