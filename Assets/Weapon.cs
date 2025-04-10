using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Initialization")]
    [SerializeField]
    [Tooltip("The point that the project is created")]
    Transform m_StartPoint = null;

    [Header("Weapon Stats")]
    [SerializeField]
    [Tooltip("The damage the weapon deals")]
    float damage = 10f;

    [SerializeField]
    float range = 100f;

    public ParticleSystem bulletSim;
    public GameObject impactEffect;
    public LineRenderer aimIndicator;

    public void Fire()
    {
        aimIndicator.enabled = false;
        bulletSim.Play();
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        RaycastHit hit;
        aimIndicator.SetPosition(0, m_StartPoint.position);
        if (Physics.Raycast(m_StartPoint.transform.position, m_StartPoint.transform.forward, out hit, range))
        {
            aimIndicator.SetPosition(1, hit.point);
            StartCoroutine(DisplayIndicator());

            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    IEnumerator DisplayIndicator()
    {
        aimIndicator.enabled = true;
        yield return new WaitForSeconds(.2f);
        aimIndicator.enabled = false;
    }
}
