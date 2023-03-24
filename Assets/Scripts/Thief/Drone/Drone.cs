using System.Collections;
using UnityEngine;

public class Drone : PositionSensor
{
    [SerializeField]
    private MapData mapData;

    [Header("Events")]
    [SerializeField]
    private Vector2Event OnDroneMoveMessage;
    [SerializeField]
    private GameEvent OnDrownScanMessage;
    [SerializeField]
    private Vector2Event OnGuardScanned;
    [SerializeField]
    private GameEvent OnDrownFlareMessage;
    [SerializeField]
    private GameEvent OnGameReady;
    [SerializeField]
    private FloatEvent OnScanOnCooldown;
    [SerializeField]
    private BoolEvent OnDroneConnectionStateChanged;
    [SerializeField]
    private Vector2Event OnTargetPointFound;

    private bool moving;
    private Vector3 targetPos;

    private Vector3 moveDir;

    [Header("Move params")]
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float minDistToTarget = 2;

    [Header("Scan params")]
    [SerializeField]
    private float scanInterval;
    [SerializeField]
    private float scanAmount;
    [SerializeField]
    private float scanCooldown;
    private float cooldownTimer;
    [SerializeField]
    private float scanRadius;
    [SerializeField]
    private LayerMask scanLayerMask;

    [Header("Network params")]
    [SerializeField]
    private float posUpdateInterval;
    private float posUpdateTimer;

    [Header("Flare params")]
    [SerializeField]
    private Flare flairePrefab;

    public const char IDENTIFIER = 'D';

    private void Awake()
    {
        OnDroneMoveMessage.AddListener(MoveToMapCoordinateCommand);
        OnDrownScanMessage.AddListener(ScanCommand);
        OnDrownFlareMessage.AddListener(FlareCommand);
        OnGameReady.AddListener(() => SendPosUpdate());

        identifier = IDENTIFIER;
    }

    protected override void AfterDisturbedChange()
    {
        OnDroneConnectionStateChanged.RaiseEvent(Disturbed);
        if (Disturbed)
            moving = false;
    }

    void Update()
    {
        if (moving)
        {
            float disSqrt = (targetPos - transform.position).sqrMagnitude;
            transform.Translate(moveDir * moveSpeed * Time.deltaTime);

            if (disSqrt < minDistToTarget * minDistToTarget)
            {
                moving = false;
                SendPosUpdate();
            }

            if(posUpdateTimer > 0)
            {
                posUpdateTimer -= Time.deltaTime;
            }
            else
            {
                posUpdateTimer = posUpdateInterval;
                SendPosUpdate();
            }
        }
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }

    public void FlareCommand()
    {
        if (Disturbed) return;
        Instantiate(flairePrefab, transform.position, Quaternion.identity);
    }

    public void MoveToMapCoordinateCommand(Vector2 mapCoordinateTargetPos)
    {
        if (Disturbed) return;

        MoveToWorldPosCommand(mapData.MapCoordinateToWorldPos(mapCoordinateTargetPos));
    }

    public void MoveToWorldPosCommand(Vector2 worldTargetPos)
    {
        if (Disturbed) return;

        MoveToWorldPosCommand(worldTargetPos.ToVector3(transform.position.y));
    }

    public void MoveToWorldPosCommand(Vector3 worldTargetPos)
    {
        if (Disturbed) return;

        moving = true;
        this.targetPos = new Vector3(worldTargetPos.x, transform.position.y, worldTargetPos.z);
        moveDir = (this.targetPos - transform.position).normalized;
    }

    public void ScanCommand()
    {
        if (Disturbed) return;

        if(cooldownTimer > 0)
        {
            Debug.Log("Scan in cooldown");
            OnScanOnCooldown.RaiseEvent(cooldownTimer);
        }
        else
        {
            StartCoroutine(ScanAction());
        }
        
    }

    private IEnumerator ScanAction()
    {
        cooldownTimer = scanCooldown;
        for (int i = 0; i < scanAmount; ++i)
        {
            Collider[] overlaps = Physics.OverlapSphere(transform.position.Ground(), scanRadius, scanLayerMask, QueryTriggerInteraction.Ignore);
            foreach(Collider collider in overlaps)
            {
                if (collider.CompareTag("Interactable"))
                {
                    OnTargetPointFound.RaiseEvent(collider.transform.position.ToVector2());
                }
                else
                {
                    OnGuardScanned.RaiseEvent(collider.transform.position.ToVector2());
                }
            }
            yield return new WaitForSeconds(scanInterval);
        }
    }
}
