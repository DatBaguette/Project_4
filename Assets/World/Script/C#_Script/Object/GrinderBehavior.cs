using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The grinder of the 2nd level behaviour
/// </summary>

public class GrinderBehavior : MonoBehaviour
{
    /// <summary>
    /// the object to grind
    /// </summary>
    [SerializeField] GameObject m_objectToGrind;
    /// <summary>
    /// the elevator 
    /// </summary>
    [SerializeField] GameObject m_elevator;
    /// <summary>
    /// the invisible wall 
    /// </summary>
    [SerializeField] GameObject m_invisibleWall;
    /// <summary>
    /// the number of object to grind 
    /// </summary>
    [SerializeField] int m_numberOfGrind = 1;
    /// <summary>
    /// the smoke particle effect
    /// </summary>
    [SerializeField] ParticleSystem m_smoke;
    /// <summary>
    /// the bool that check if an object can be grind
    /// </summary>
    private bool m_canGrind = true;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.tag == "MovableObject")
        {
            Destroy(other.gameObject);
            if ( m_canGrind)
            {
                m_canGrind = false;
                StartCoroutine(GrindObject());

                m_smoke.Play();
            }
        }
    }

    IEnumerator GrindObject()
    {
        yield return new WaitForSeconds(.5f);

        m_canGrind = true;

        m_numberOfGrind -= 1;
        m_objectToGrind.transform.position -= new Vector3(0, 5, 0);

        if ( m_numberOfGrind == 0)
        {
            if (!SoundManager.Instance.m_succeedSound.isPlaying)
            {
                SoundManager.Instance.m_succeedSound.Play();
            }

            m_elevator.GetComponent<Animator>().Play("MoveUp2");

            m_invisibleWall.SetActive(false);
        }
    }
}
