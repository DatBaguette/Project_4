using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creat Mouvable object in the boss scene
/// </summary>

public class ButtonThatCreateMovableObjects : MonoBehaviour
{
    /// <summary>
    /// the spawnner for the object
    /// </summary>
    [SerializeField] GameObject m_spawnerPosition;

    /// <summary>
    /// mouvable prefab to creat
    /// </summary>
    [SerializeField] GameObject m_movableObjectsPrefab;

    /// <summary>
    /// activate on not
    /// </summary>
    public bool m_activate = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if ((other.GetComponent<RobotMovement>() || other.GetComponent<ClickToMoveEntity>()) && !m_activate)
        {
            m_activate = true;
            gameObject.transform.position += transform.forward * .3f;

            GameObject movableObject = Instantiate(m_movableObjectsPrefab, m_spawnerPosition.transform);

            movableObject.transform.SetParent(gameObject.transform.parent);

            movableObject.transform.localScale /= 2;
            movableObject.transform.position = m_spawnerPosition.transform.position;
            

            StartCoroutine(ResetButton());
        }
    }

    IEnumerator ResetButton()
    {
        yield return new WaitForSeconds(3);

        m_activate = false;
        gameObject.transform.position -= transform.forward * .3f;
    }
}
