using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonThatCreateMovableObjects : MonoBehaviour
{
    [SerializeField] GameObject m_spawnerPosition;

    [SerializeField] GameObject m_movableObjectsPrefab;

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
