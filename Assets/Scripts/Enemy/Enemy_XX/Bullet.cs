using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

     Transform target;
    public int damage = 1; // damage of bullet enemy

    public float speed  = 20f; // speed of enemy ;
    public GameObject EffectPlayer; // effect when player dead 
    public float explosionRadius = 0f;

    public void Seek(Transform _target)
    {
        target = _target; // fuct will move to player 
    }

    // Update is called once per frame
    void Update() {
        if (target == null)
        {

        Destroy(gameObject);
        return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

	}
    void HitTarget()
    {
      GameObject effectIns = (GameObject)  Instantiate(EffectPlayer, transform.position, transform.rotation);
        //    Destroy(target.gameObject);
            Destroy(effectIns, 5f);
        if(explosionRadius > 0f) // bán kính nổ E10
        {
            Explode(); // nổ tung 
        }
        else
        {
            Damage(target);
        }
        
        Destroy(gameObject);
    }
    void Explode() // nổ tung 
    {
      Collider[] colliders =   Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Player")
                Damage(collider.transform);
        }
    }

    void Damage(Transform targetA) // destroy player 
    {
       Player player =  targetA.GetComponent<Player>();

        if(player != null ) // if player lives ! then kills him
            player.TakeDamage(damage);


       
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionRadius); // vẽ phere bán kính 
    }
}
