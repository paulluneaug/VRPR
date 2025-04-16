using System;

using UnityEngine;

using UnityUtility.MathU;

public class Agent : MonoBehaviour
{
    public float AngleOffset { get => m_angleOffset; set => m_angleOffset = value; }


    [SerializeField] private Texture[] m_agentsTexture;

    [NonSerialized] private float m_angleOffset;

    private void Start()
    {
        Texture agentTexture = m_agentsTexture[UnityEngine.Random.Range(0, m_agentsTexture.Length)];
        GetComponent<Renderer>().material.mainTexture = agentTexture;
    }


    private void UpdateAgentPosition(float playerAngle, float distance)
    {
        float angle = playerAngle + m_angleOffset;

        transform.localPosition = new Vector3(
            MathUf.Cos(angle) * distance,
            0.0f,
            MathUf.Sin(angle) * distance);

        transform.LookAt(transform.parent.position, Vector3.up);
    }
}
