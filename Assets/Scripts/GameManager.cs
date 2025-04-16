using System;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

using UnityUtility.CustomAttributes;
using UnityUtility.Extensions;
using UnityUtility.MathU;
using UnityUtility.Pools;
using UnityUtility.Singletons;
using UnityUtility.Timer;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    private enum GameState
    {
        Running,
        Reaction,
    }

    [Title("Chairs")]
    [SerializeField] private int m_startChairCount = 8;
    [SerializeField] private float m_chairCircleRadius = 8;
    [Separator]
    [SerializeField] private Transform m_chairParent;
    [SerializeField] private ChairPool m_chairPool;

    [Title("Rounds")]
    [SerializeField] private Vector2 m_startRoundDurationRange;
    [SerializeField] private float m_startReactionTime;
    [SerializeField, Range(0.0f, 1.0f)] private float m_reactionTimeMultiplier;

    [Title("Player")]
    [SerializeField] private PlayerHandInput m_playerInput;
    [SerializeField] private float m_startPlayerSpeed;
    [SerializeField, Range(0.0f, 3.0f)] private float m_playerSpeedMultiplier;
    [SerializeField] private float m_playerRailDistance = 1.0f;
    [SerializeField] private Transform m_playerContainer;
    [SerializeField] private float m_motionlessTime;

    [Title("Audio")]
    [SerializeField] private AudioSource m_musicSource;
    
    [Title("UI")]
    [SerializeField] private TextMeshProUGUI m_textUI;
     
    // Cache
    [NonSerialized] private GameState m_currentGameState;

    [NonSerialized] private List<PooledObject<Transform>> m_chairs;
    [NonSerialized] private int m_currentRound;

    [NonSerialized] private Timer m_roundTimer;
    [NonSerialized] private Timer m_reactionTimer;
    [NonSerialized] private Timer m_motionlessTimer;

    [NonSerialized] private float m_currentPlayerSpeed;
    [NonSerialized] private float m_currentPlayerAngle;

    protected override void Start()
    {
        base.Start();
        m_chairs = new List<PooledObject<Transform>>();

        m_roundTimer = new Timer(0.0f, false);
        m_reactionTimer = new Timer(0.0f, false);
        m_motionlessTimer = new Timer(m_motionlessTime, false);

        m_currentRound = -1;

        StartNewRound();
    }

    private void Update()
    {
        Action<float> updateStateAction = m_currentGameState switch
        {
            GameState.Running => UpdateRunningState,
            GameState.Reaction => UpdateReactionState,
            _ => throw new IndexOutOfRangeException(),
        };

        updateStateAction(Time.deltaTime);
    }

    private void UpdateRunningState(float deltaTime)
    {
        if (m_roundTimer.Update(deltaTime))
        {
            StartReactionState();
            return;
        }

        if (!m_playerInput.MoveInput)
        {
            if (!m_motionlessTimer.IsRunning)
            {
                m_motionlessTimer.Start();
            }
            if (m_motionlessTimer.Update(deltaTime))
            {
                LoseGame();
            }
            return;
        }
        m_motionlessTimer.Stop();

        m_currentPlayerAngle += MathUf.TAU * deltaTime * m_currentPlayerSpeed;
        float radius = m_playerRailDistance + m_chairCircleRadius;

        m_playerContainer.transform.localPosition = new Vector3(
            MathUf.Cos(m_currentPlayerAngle) * radius,
            0.0f,
            MathUf.Sin(m_currentPlayerAngle) * radius);

        m_playerContainer.LookAt(m_playerContainer.parent.transform.position, Vector3.up);
    }

    private void StartReactionState()
    {
        m_musicSource.Stop();
        m_currentGameState = GameState.Reaction;
        m_textUI.text = "SIT";
        m_reactionTimer.Start();
    }

    private void UpdateReactionState(float deltaTime)
    {
        if (m_reactionTimer.Update(deltaTime))
        {
            LoseGame();
            return;
        }

        if (m_playerInput.SitInput)
        {
            m_textUI.text = "WELL SITTED";
            StartNewRound();
        }
    }

    private void StartNewRound()
    {
        ++m_currentRound;

        float roundDuration = UnityEngine.Random.value.RemapFrom01(m_startRoundDurationRange);
        m_roundTimer.Duration = roundDuration;
        m_roundTimer.Start();

        float reactionTime = m_startReactionTime * MathUf.Pow(m_reactionTimeMultiplier, m_currentRound);
        m_reactionTimer.Duration = reactionTime;

        int chairCount = m_startChairCount - m_currentRound;
        m_chairs.ForEach(chair => chair.Release());
        m_chairs.Clear();

        for (int iChair = 0; iChair < chairCount; ++iChair)
        {
            PooledObject<Transform> pooledChair = m_chairPool.Request();

            Transform chairTransform = pooledChair.Object;
            chairTransform.SetParent(m_chairParent);
            chairTransform.gameObject.SetActive(true);

            float angle = MathUf.TAU / chairCount * iChair;

            chairTransform.localPosition = new Vector3(MathUf.Cos(angle) * m_chairCircleRadius, 0.0f, MathUf.Sin(angle) * m_chairCircleRadius);

            m_chairs.Add(pooledChair);
        }

        m_currentPlayerSpeed = m_startPlayerSpeed * MathUf.Pow(m_playerSpeedMultiplier, m_currentRound);
        m_currentPlayerAngle = 0.0f;

        m_currentGameState = GameState.Running;
        m_textUI.text = "RUN";

        m_musicSource.Play();
    }

    private void LoseGame()
    {
        m_textUI.text = "GAME OVER";
        Debug.LogError($"Game Lost");
    }
}
