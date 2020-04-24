using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 
/// Behavior of the joystick gameObject
/// It save his direction to send it to the robot movement script
/// 
/// </summary>

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image m_bgImg;
    private Image m_joystickImg;

    private Vector3 m_joystickPosition;

    [SerializeField] public Vector3 m_InputDirection { set; get; }


    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (m_bgImg.rectTransform,
                ped.position,
                 ped.pressEventCamera,
                out pos))
        {
            pos.x = (pos.x / m_bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / m_bgImg.rectTransform.sizeDelta.y);

            float x = (m_bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
            float y = (m_bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

            m_InputDirection = new Vector3(x, 0, y);
            m_InputDirection = (m_InputDirection.magnitude > 1) ? m_InputDirection.normalized : m_InputDirection;

            m_joystickImg.rectTransform.anchoredPosition =
                new Vector3(m_InputDirection.x * (m_bgImg.rectTransform.sizeDelta.x / 3),
                    m_InputDirection.z * (m_bgImg.rectTransform.sizeDelta.y / 3));
            
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        m_InputDirection = Vector3.zero;
        m_joystickImg.rectTransform.anchoredPosition = Vector3.zero;

    }

    void Start()
    {
        m_bgImg = GetComponent<Image>();
        m_joystickImg = transform.GetChild(0).GetComponent<Image>();
        m_InputDirection = Vector3.zero;
    }
}
