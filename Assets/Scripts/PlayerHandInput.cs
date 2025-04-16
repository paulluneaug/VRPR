using UnityEngine;
using UnityEngine.XR.Hands.Samples.GestureSample;

using UnityUtility.CustomAttributes;

public class PlayerHandInput : MonoBehaviour
{
    public bool MoveInput => m_moveInput > 0;
    public bool SitInput => m_sitInput > 0;


    [SerializeField] private StaticHandGesture m_rightSitPoseGesture;
    [SerializeField] private StaticHandGesture m_rightMovePoseGesture;
    [SerializeField] private StaticHandGesture m_leftSitPoseGesture;
    [SerializeField] private StaticHandGesture m_leftMovePoseGesture;

    [SerializeField, Disable] private int m_moveInput = 0;
    [SerializeField, Disable] private int m_sitInput = 0;

    private void Start()
    {
        m_rightSitPoseGesture.gesturePerformed.AddListener(OnSitPosePerformed);
        m_rightMovePoseGesture.gesturePerformed.AddListener(OnMovePosePerformed);
        m_leftSitPoseGesture.gesturePerformed.AddListener(OnSitPosePerformed);
        m_leftMovePoseGesture.gesturePerformed.AddListener(OnMovePosePerformed);

        m_rightSitPoseGesture.gestureEnded.AddListener(OnSitPoseEnded);
        m_rightMovePoseGesture.gestureEnded.AddListener(OnMovePoseEnded);
        m_leftSitPoseGesture.gestureEnded.AddListener(OnSitPoseEnded);
        m_leftMovePoseGesture.gestureEnded.AddListener(OnMovePoseEnded);
    }

    private void OnDestroy()
    {
        m_rightSitPoseGesture.gesturePerformed.RemoveListener(OnSitPosePerformed);
        m_rightMovePoseGesture.gesturePerformed.RemoveListener(OnMovePosePerformed);
        m_leftSitPoseGesture.gesturePerformed.RemoveListener(OnSitPosePerformed);
        m_leftMovePoseGesture.gesturePerformed.RemoveListener(OnMovePosePerformed);

        m_rightSitPoseGesture.gestureEnded.RemoveListener(OnSitPoseEnded);
        m_rightMovePoseGesture.gestureEnded.RemoveListener(OnMovePoseEnded);
        m_leftSitPoseGesture.gestureEnded.RemoveListener(OnSitPoseEnded);
        m_leftMovePoseGesture.gestureEnded.RemoveListener(OnMovePoseEnded);
    }

    private void OnSitPosePerformed()
    {
        m_sitInput++;
    }

    private void OnMovePosePerformed()
    {
        m_moveInput++;
    }

    private void OnSitPoseEnded()
    {
        m_sitInput--;
    }

    private void OnMovePoseEnded()
    {
        m_moveInput--;
    }
}
