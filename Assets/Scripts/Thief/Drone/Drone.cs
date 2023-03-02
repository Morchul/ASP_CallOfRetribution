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

    private void Awake()
    {
        OnDroneMoveMessage.AddListener(MoveCommand);
        OnDrownScanMessage.AddListener(ScanCommand);
        OnDrownFlareMessage.AddListener(FlareCommand);
        OnGameReady.AddListener(() => SendPosUpdate());

        UpdateCreateFunc = MessageUtility.CreateDronePosMessage;
    }

    protected override void AfterDisturbedChange()
    {
        NetworkManager.Instance.Transmitter.WriteToClient(MessageUtility.CreateDroneStateChangedMessage(Disturbed));
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

    public void MoveCommand(Vector2 targetPos)
    {
        Debug.Log("Move command, target pos: " + targetPos);
        if (Disturbed) return;

        Vector3 worldTargetPos = mapData.MapCoordinateToWorldPos(targetPos);

        moving = true;
        Debug.Log("this.position: " + this.transform.position);
        this.targetPos = new Vector3(worldTargetPos.x, transform.position.y, worldTargetPos.z);
        Debug.Log("this.targetPos: " + this.targetPos);
        moveDir = (this.targetPos - transform.position).normalized;
        Debug.Log("moveDir: " + moveDir);
    }

    public void ScanCommand()
    {
        if (Disturbed) return;

        if(cooldownTimer > 0)
        {
            Debug.Log("Scan in cooldown");
            NetworkManager.Instance.Transmitter.WriteToClient(MessageUtility.CreateScanCooldownMessage(cooldownTimer));
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
            Collider[] overlaps = Physics.OverlapSphere(new Vector3(this.transform.position.x, 0, this.transform.position.z), scanRadius, scanLayerMask);
            foreach(Collider collider in overlaps)
            {
                NetworkManager.Instance.Transmitter.WriteToClient(MessageUtility.CreateScanResultMessage(collider.transform.position));
                OnGuardScanned.RaiseEvent(collider.transform.position);
                Debug.Log("OnGuardscanned: " + collider.transform.position);
            }
            yield return new WaitForSeconds(scanInterval);
        }
    }
}
