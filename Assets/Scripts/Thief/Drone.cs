using System.Collections;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [Header("Events")]
    [SerializeField]
    private Vector2Event OnDroneMoveMessage;
    [SerializeField]
    private GameEvent OnDrownScanMessage;
    [SerializeField]
    private Vector2Event OnGuardScanned;

    private bool moving;
    private Vector2 targetPos;

    private Vector2 moveDir;

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

    private bool connected;
    public bool Connected
    {
        get => connected;
        set
        {
            connected = value;
            //OnDrownConnectionStateChanged.RaiseEvent(connected);
            NetworkManager.Instance.Transmitter.WriteToClient(MessageUtility.CreateDroneStateChangedMessage(connected));
            if (!connected) //Disconnect
            {
                moving = false;
                SendPosUpdate();
            }
        }
    }

    private void Awake()
    {
        OnDroneMoveMessage.AddListener(MoveCommand);
        OnDrownScanMessage.AddListener(ScanCommand);
    }

    private void SendPosUpdate()
    {
        NetworkManager.Instance.Transmitter.WriteToClient(MessageUtility.CreateDronePosMessage(transform.position));
    }

    private void Start()
    {
        SendPosUpdate();
    }

    void Update()
    {
        if (moving)
        {
            float disSqrt = (targetPos - (Vector2)transform.position).sqrMagnitude;
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

    public void MoveCommand(Vector2 targetPos)
    {
        moving = true;
        this.targetPos = targetPos;
        moveDir = new Vector3(targetPos.x - transform.position.x, 0, targetPos.y - transform.position.z).normalized;
    }

    public void ScanCommand()
    {
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
            }
            yield return new WaitForSeconds(scanInterval);
        }
    }
}
