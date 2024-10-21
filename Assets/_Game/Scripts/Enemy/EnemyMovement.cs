using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MovementState
{
    Idle,
    Wandering,
    RuningAway,
    Chasing,
    Returning
}

public class EnemyMovement : MonoBehaviour
{
    public System.Action OnStartMoving;
    public System.Action OnStopMoving;
    public System.Action OnStopChasing;

    [SerializeField]
    private EnemyState m_enemyState = null;

    [SerializeField]
    private Rigidbody m_body = null;

    [SerializeField]
    private EnemyDetector m_enemyDetector = null;

    [SerializeField]
    private EnemyBump m_enemyBump = null;

    [SerializeField]
    private EnemyRespawn m_enemyRespawn = null;

    [SerializeField, Tooltip("Collider with the Enemy Layer")]
    private Collider m_identityCollider = null;

    [SerializeField]
    private float m_teleportTimerIfStuck = 20f;

    [Header("Chase Parameters")]
    [SerializeField]
    private float m_chaseMovementSpeed = 2f;

    [SerializeField]
    private float m_moveBackMovementSpeed = 2f;

    [SerializeField]
    private float m_chaseTimeOut = 2f;


    [Header("Run away Parameters")]
    [SerializeField]
    private float m_runAwayMovementSpeed = 2f;

    [SerializeField]
    private float m_runAwayTimeout = 3f;


    [Header("Wandering Parameters")]
    [SerializeField]
    private bool m_canWander = false;

    [SerializeField]
    private float m_wanderingMovementSpeed = 2f;

    [SerializeField]
    private float m_minWanderingTimeBetweenMovement = 2f;

    [SerializeField]
    private float m_maxWanderingTimeBetweenMovement = 3f;

    [SerializeField]
    private float m_maxWanderingDistanceToTravelFromOriginalPosition = 5f;


    [Header("Returning Parameters")]
    [SerializeField]
    private float m_returningToOriginalPositionMovementSpeed = 2f;



    // General movement variables
    private RoomController m_roomController;
    private Vector3 m_originalPosition;
    private Vector3 m_roomCenterPosition;
    private Bounds m_roomBounds;
    private MovementState m_movementState;
    private float m_teleportTimer;
    private bool m_canMove;
    private bool m_isMoving;

    public bool IsMoving { get => m_isMoving; }


    // Wandering movement variables
    private Vector3 m_wanderingDestination;
    private float m_wanderingTimer;
    private float m_wanderingRandomTimeBetweenMovements;

    // Chasing movement variables
    private GameObject m_targetToChase;
    private float m_chaseTimeOutTimer;
    private bool m_isTargetInRange;
    private bool m_canChase;

    // Run away variables
    private float m_runAwayTimeoutTimer;


    private void OnEnable()
    {
        m_enemyState.OnInitialize += OnInitialize;
        m_enemyState.OnDisableEnemy += OnDisableEnemy;

        m_enemyDetector.OnEnemyInRange += OnEnemyInRange;
        m_enemyDetector.OnEnemyOutOfRange += OnEnemyOutOfRange;

        PlayerHealth.OnPlayerInvincible += OnPlayerInvincible;
        PlayerHealth.OnPlayerNotInvincible += OnPlayerNotInvincible;

        PlayerStateController.OnPlayerDeath += OnPlayerDeath;
        PlayerStateController.OnPlayerAlive += OnPlayerAlive;

        m_enemyRespawn.OnStartRespawnAnimation += OnStartRespawnAnimation;

        RoomController.OnSendRoomInfoToEnemy += OnSendRoomInfoToEnemy;
    }

    private void OnDisable()
    {
        m_enemyState.OnInitialize -= OnInitialize;
        m_enemyState.OnDisableEnemy -= OnDisableEnemy;

        m_enemyDetector.OnEnemyInRange -= OnEnemyInRange;
        m_enemyDetector.OnEnemyOutOfRange -= OnEnemyOutOfRange;

        PlayerHealth.OnPlayerInvincible -= OnPlayerInvincible;
        PlayerHealth.OnPlayerNotInvincible -= OnPlayerNotInvincible;

        PlayerStateController.OnPlayerDeath -= OnPlayerDeath;
        PlayerStateController.OnPlayerAlive -= OnPlayerAlive;

        m_enemyRespawn.OnStartRespawnAnimation -= OnStartRespawnAnimation;

        RoomController.OnSendRoomInfoToEnemy -= OnSendRoomInfoToEnemy;
    }

    private void Awake()
    {
        m_originalPosition = m_body.position;
    }

    private void Update()
    {
        if (m_canMove == false)
            return;

        if (m_enemyBump.IsBumped == true)
            return;

        switch (m_movementState)
        {
            case MovementState.Idle:

                break;
            case MovementState.Wandering:
                MoveWandering();
                break;
            case MovementState.RuningAway:
                MoveRunAway();
                break;
            case MovementState.Chasing:
                MoveChasing();
                break;
            case MovementState.Returning:
                MoveReturning();
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        DisableVelocity();
        UpdateNearestObject();
    }

    private void UpdateNearestObject()
    {
        if (m_isTargetInRange)
        {
            if (m_enemyDetector.NearestObject == null)
                return;

            m_targetToChase = m_enemyDetector.NearestObject;

            if (m_movementState != MovementState.Chasing || m_movementState != MovementState.RuningAway)
            {
                if (m_enemyState.IsAgressive && m_canChase)
                {
                    if (m_roomBounds.Contains(m_targetToChase.transform.position - m_roomCenterPosition))
                        EnterChaseState();
                }
                else if (m_enemyState.IsAgressive == false)
                    EnterRunAwayState();
            }
        }
    }

    private void DisableVelocity()
    {
        m_body.angularVelocity = Vector3.zero;
        m_body.velocity = Vector3.zero;
    }

    private void TeleportToDestination(Vector3 destination)
    {
        m_body.position = destination;
    }


    #region LISTENERS
    private void OnInitialize()
    {
        m_canMove = true;
        m_canChase = true;
        m_body.position = m_originalPosition;

        EnterIdleState();
        StartCoroutine(EnterWanteringStateWithDelay(1f));
    }

    private void OnDisableEnemy()
    {
        m_canMove = false;
        OnStopMoving?.Invoke();
        m_isMoving = false;
        m_isTargetInRange = false;
        m_targetToChase = null;
    }

    private void OnStartRespawnAnimation()
    {
        m_body.position = m_originalPosition;
    }

    private void OnPlayerDeath()
    {
        m_targetToChase = null;
        EnterWanderingState();
    }

    private void OnPlayerAlive()
    {
        m_body.position = m_originalPosition;
    }

    private void OnSendRoomInfoToEnemy(Collider enemyCollider, RoomController roomController, Vector3 roomCenterPosition, Bounds roomBounds)
    {
        if (m_identityCollider == enemyCollider)
        {
            m_roomCenterPosition = roomCenterPosition;
            m_roomBounds = roomBounds;
            m_roomController = roomController;
        }
    }

    private void OnEnemyInRange()
    {
        m_isTargetInRange = true;
    }

    private void OnEnemyOutOfRange()
    {
        m_isTargetInRange = false;

        if (m_movementState == MovementState.RuningAway)
            m_body.transform.forward = -m_body.transform.forward;
    }

    private void OnPlayerInvincible()
    {
        if (m_targetToChase == null)
            return;

        m_canChase = false;
    }

    private void OnPlayerNotInvincible()
    {
        if (m_targetToChase == null)
            return;

        m_canChase = true;
        EnterChaseState();
    }

    #endregion

    private void EnterIdleState()
    {
        m_movementState = MovementState.Idle;
        OnStopMoving?.Invoke();
        m_isMoving = false;
    }

    #region REGION : WANDERING STATE ===============

    private void EnterWanderingState()
    {
        if (m_canWander == false)
        {
            OnStopMoving?.Invoke();
            return;
        }

        m_movementState = MovementState.Wandering;
        m_wanderingTimer = 0f;
        m_wanderingRandomTimeBetweenMovements = Random.Range(m_minWanderingTimeBetweenMovement, m_maxWanderingTimeBetweenMovement);

        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0;
        randomDirection.Normalize();
        randomDirection *= Random.Range(0f, m_maxWanderingDistanceToTravelFromOriginalPosition);

        m_wanderingDestination = m_originalPosition + randomDirection;
        m_wanderingDestination = m_roomBounds.ClosestPoint(m_wanderingDestination - m_roomCenterPosition) + m_roomCenterPosition;
        m_body.transform.forward = m_wanderingDestination - m_body.position;

        OnStartMoving?.Invoke();
        m_isMoving = true;
    }

    private IEnumerator EnterWanteringStateWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EnterWanderingState();
    }

    private void MoveWandering()
    {
        if (m_canWander == false)
            return;

        m_body.position = Vector3.MoveTowards(m_body.position, m_wanderingDestination, m_wanderingMovementSpeed * Time.deltaTime);


        if (Vector3.Distance(m_body.position, m_wanderingDestination) < 0.1f)
        {
            OnStopMoving?.Invoke();
            m_isMoving = false;
            m_wanderingTimer += Time.deltaTime;

            if (m_wanderingTimer > m_wanderingRandomTimeBetweenMovements)
                EnterWanderingState();
        }

    }

    #endregion // WANDERING STATE


    #region REGION : RUNING AWAY STATE

    private void EnterRunAwayState()
    {
        m_movementState = MovementState.RuningAway;
        m_runAwayTimeoutTimer = 0f;
        OnStartMoving?.Invoke();
        m_isMoving = true;
    }

    private void MoveRunAway()
    {
        if (m_targetToChase == null)
        {
            EnterWanderingState();
            return;
        }

        // If run away too out of the room, stops and look at player
        if (m_roomBounds.Contains(m_body.position - m_roomCenterPosition))
        {
            Vector3 runAwayDirection = m_body.position - m_targetToChase.transform.position;
            m_body.position += runAwayDirection.normalized * m_runAwayMovementSpeed * Time.deltaTime;
            m_body.transform.forward = runAwayDirection;
        }
        else
        {
            m_body.transform.forward = m_targetToChase.transform.position - m_body.position;

            if (m_isMoving)
            {
                m_isMoving = false;
                OnStopMoving?.Invoke();
            }
        }

        /*
        // If run away too from from original position, stops and look at player
        if (Vector3.Distance(m_body.position, m_originalPosition) > m_maxRunAwayDistanceFromOriginalPosition)
        {
            m_body.transform.forward = m_targetToChase.transform.position - m_body.position;

            if (m_isMoving)
            {
                m_isMoving = false;
                OnStopMoving?.Invoke();
            }
        }
        // else continue to run away in the opposite direction of the player
        else
        {
            Vector3 runAwayDirection = m_body.position - m_targetToChase.transform.position;
            m_body.position += runAwayDirection.normalized * m_runAwayMovementSpeed * Time.deltaTime;
            m_body.transform.forward = runAwayDirection;
        }
        */

        ManageOutOfDanger();
    }

    private void ManageOutOfDanger()
    {
        if (m_isTargetInRange == false)
        {
            m_runAwayTimeoutTimer += Time.deltaTime;

            if (m_runAwayTimeoutTimer > m_runAwayTimeout)
            {
                m_runAwayTimeoutTimer = 0f;
                EnterReturningState();
            }
        }
    }

    #endregion


    #region REGION : CHASING STATE ===============

    private void EnterChaseState()
    {
        m_movementState = MovementState.Chasing;
        m_canChase = true;
        m_chaseTimeOutTimer = 0f;
        m_isMoving = true;
        OnStartMoving?.Invoke();
    }

    private void MoveChasing()
    {
        if (m_targetToChase == null)
            return;

        if (m_canChase == true)
        {
            m_body.position = Vector3.MoveTowards(m_body.position, m_targetToChase.transform.position, m_chaseMovementSpeed * Time.deltaTime);
            m_body.transform.forward = m_targetToChase.transform.position - m_body.position;

            //ManageChaseTargetTimeOut();

            ManageChaseTooFar();
        }
        else
        {
            m_body.position = Vector3.MoveTowards(m_body.position, m_body.position + (m_body.position - m_targetToChase.transform.position), m_moveBackMovementSpeed * Time.deltaTime);
            m_body.transform.forward = m_targetToChase.transform.position - m_body.position;
        }
    }

    private void ManageChaseTargetTimeOut()
    {
        if (m_isTargetInRange == false)
        {
            m_chaseTimeOutTimer += Time.deltaTime;

            if (m_chaseTimeOutTimer > m_chaseTimeOut)
            {
                StopChaseTarget();
                OnStopMoving?.Invoke();
                EnterReturningState();
            }
        }
    }

    private void ManageChaseTooFar()
    {
        //if (Vector3.Distance(m_body.position, m_originalPosition) > m_maxChaseDistanceFromOriginalPosition)

        if (m_roomBounds.Contains(m_body.position - m_roomCenterPosition) == false)
        {
            StopChaseTarget();
            OnStopMoving?.Invoke();
            m_isMoving = false;
            EnterReturningState();
        }
    }

    private void StopChaseTarget()
    {
        m_targetToChase = null;
        m_chaseTimeOutTimer = 0f;
        OnStopChasing?.Invoke();
    }

    #endregion


    #region REGION : RETURNING STATE

    private void EnterReturningState()
    {
        m_movementState = MovementState.Returning;
        OnStartMoving?.Invoke();
        m_isMoving = true;
        m_body.transform.forward = m_originalPosition - m_body.position;

        m_teleportTimerIfStuck = 0f;
    }

    private void MoveReturning()
    {
        m_body.position = Vector3.MoveTowards(m_body.position, m_originalPosition, m_returningToOriginalPositionMovementSpeed * Time.deltaTime);

        m_teleportTimerIfStuck += Time.deltaTime;

        if (m_teleportTimer > m_teleportTimerIfStuck)
        {
            TeleportToDestination(m_originalPosition);
        }

        if (Vector3.Distance(m_body.position, m_originalPosition) < 0.1f)
        {
            OnStopMoving?.Invoke();
            m_isMoving = false;
            EnterWanderingState();
        }
    }

    #endregion



}
