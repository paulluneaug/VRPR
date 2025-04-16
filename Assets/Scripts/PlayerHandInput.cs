using System;

using UnityEngine;
using UnityEngine.XR.Hands.Samples.GestureSample;

public class PlayerHandInput : MonoBehaviour
{
    public bool MoveInput => m_moveInput;
    public bool SitInput => m_sitInput;


    [SerializeField] private StaticHandGesture m_rightSitPoseGesture;
    [SerializeField] private StaticHandGesture m_rightMovePoseGesture;
    [SerializeField] private StaticHandGesture m_leftSitPoseGesture;
    [SerializeField] private StaticHandGesture m_leftMovePoseGesture;

    [NonSerialized] private bool m_moveInput = true;
    [NonSerialized] private bool m_sitInput = true;

    private void Start()
    {
        m_rightSitPoseGesture.gesturePerformed.AddListener(OnSitPosePerformed);
        m_rightMovePoseGesture.gesturePerformed.AddListener(OnMovePosePerformed);
        m_leftSitPoseGesture.gesturePerformed.AddListener(OnSitPosePerformed);
        m_leftMovePoseGesture.gesturePerformed.AddListener(OnMovePosePerformed);

        m_rightSitPoseGesture.gesturePerformed.AddListener(OnSitPosePerformed);
        m_rightMovePoseGesture.gesturePerformed.AddListener(OnMovePosePerformed);
        m_leftSitPoseGesture.gesturePerformed.AddListener(OnSitPosePerformed);
        m_leftMovePoseGesture.gesturePerformed.AddListener(OnMovePosePerformed);
    }

    private void OnDestroy()
    {
        m_rightSitPoseGesture.gesturePerformed.RemoveListener(OnSitPosePerformed);
        m_rightMovePoseGesture.gesturePerformed.AddListener(OnMovePosePerformed);
        m_leftSitPoseGesture.gesturePerformed.AddListener(OnSitPosePerformed);
        m_leftMovePoseGesture.gesturePerformed.AddListener(OnMovePosePerformed);
    }

    private void OnSitPosePerformed()
    {
    }

    private void OnMovePosePerformed()
    {
    }

    private void OnSitPoseEnded()
    {
    }

    private void OnMovePoseEnded()
    {
    }
} 
