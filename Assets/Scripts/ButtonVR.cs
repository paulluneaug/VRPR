using UnityEngine;
using UnityEngine.UI;

using UnityUtility.CustomAttributes;

public class ButtonVR : MonoBehaviour
{
    [SerializeField, Layer] private int m_playerLayer;
    private Button m_button;


    private void Awake()
    {
        m_button = GetComponent<Button>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == m_playerLayer)
        {
            m_button.onClick.Invoke();
        }
    }
}
