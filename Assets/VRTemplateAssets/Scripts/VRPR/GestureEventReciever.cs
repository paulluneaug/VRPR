using UnityEngine;
using UnityEngine.XR.Hands.Samples.GestureSample;

public class GestureEventReciever : MonoBehaviour
{

    [SerializeField] private StaticHandGesture m_handGesture;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_handGesture.gesturePerformed.AddListener(OnGesturePerformed);
        m_handGesture.gestureEnded.AddListener(OnGestureEnded);
    }

    private void OnGestureEnded()
    {
        Debug.Log($"[{name}] Gesture Ended");
    }

    private void OnGesturePerformed()
    {
        Debug.Log($"[{name}]Gesture Performed");
    }

    private void OnDestroy()
    {
        m_handGesture.gesturePerformed.RemoveListener(OnGesturePerformed);
        m_handGesture.gestureEnded.RemoveListener(OnGestureEnded);
    }
}
