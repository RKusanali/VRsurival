using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon") || other.gameObject.CompareTag("Bullet"))
        {
            m_animator.SetTrigger("Death");
        }
    }

    public void ActivateDummy()
    {
        m_animator.SetTrigger("Activate");
    }
}
