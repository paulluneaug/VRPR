using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonVR : MonoBehaviour
{
    private Button m_button;

    private void Awake()
    {
        m_button = GetComponent<Button>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_button.onClick.Invoke();
        }
    }
}
