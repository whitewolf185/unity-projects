using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] int _damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Condition>(out var hitted))
        {
            hitted.TakeDamage(_damage);
        }
    }
}
