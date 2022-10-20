using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public float delay = 2f;
    public float radius = 10f;
    public float force = 1000f;
    public GameObject explosionEffect;
    private float countDown;
    private bool exploded = false;
    // Start is called before the first frame update
    void Start()
    {
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0 && !exploded)
        {
            Explode();
        }
    }

    public void Explode()
    {
        exploded = true;

        if (explosionEffect != null) Instantiate(explosionEffect, transform.position, transform.rotation);

        // Move objects logic
        var nearObjects = Physics.OverlapSphere(transform.position, radius);

        foreach (var nearObject in nearObjects)
        {
            var rb = nearObject.GetComponent<Rigidbody>();
            if (nearObject.tag == "MainCamera" || nearObject.tag == "Gracie") continue;
            if (rb != null)
            {
                var ghost = nearObject.GetComponent<GhostMovement>();
                if (ghost != null)
                {
                    ghost.resetForce = false;
                    rb.constraints = RigidbodyConstraints.None;
                    rb.AddExplosionForce(force, transform.position, radius);
                }
            }
        }

        Destroy(gameObject);
    }
}
