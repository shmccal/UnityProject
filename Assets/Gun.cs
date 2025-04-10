using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
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

    public void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_StartPoint.transform.position, m_StartPoint.transform.forward, out hit, range))
        {
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
