using UnityEngine;

public class Map : MovableFocusObject
{
    [SerializeField]
    private FocusHandler focusHandler;

    [SerializeField]
    private Transform mapFocusPos;

    [Header("Map")]
    [SerializeField]
    private float worldWidth;
    [SerializeField]
    private float worldHeight;

    public float MapWidth { get; private set; }
    public float MapHeight { get; private set; }

    [Header("UI")]
    [SerializeField]
    private GameObject mapOverlay;
    [SerializeField]
    private MapScale horizontalScale;
    [SerializeField]
    private MapScale verticalScale;

    private Vector3 startPos;
    private Vector2 currentPos;
    private Vector2 normalizedStartPos = new Vector2(0.5f, 0.5f);
    private Vector2 localScale;

    public const float MAP_SCALE_POS_RATIO = 10; //Scale 1 = distance 10

    protected override void Awake()
    {
        base.Awake();
        FocusHandler = focusHandler;
        MapWidth = transform.localScale.x;
        MapHeight = transform.localScale.z;
        localScale = new Vector2(MapWidth, MapHeight);
    }

    public override void EnableFocus(FocusAnimationParam focusPos)
    {
        base.EnableFocus(new FocusAnimationParam(mapFocusPos));
    }

    protected override void AnimationFinished()
    {
        base.AnimationFinished();
        if (InFocus)
        {
            startPos = transform.position;
            Debug.Log("Map start pos: " + startPos);
            currentPos = normalizedStartPos;
            //mapOverlay.SetActive(true);

            horizontalScale.gameObject.SetActive(true);
            horizontalScale.ScalePos = currentPos.x;
            horizontalScale.UpdateScale();
            horizontalScale.UpdatePos();
            verticalScale.gameObject.SetActive(true);
            verticalScale.ScalePos = currentPos.y;
            verticalScale.UpdateScale();
            verticalScale.UpdatePos();
        }
    }

    public override void DisableFocus()
    {
        base.DisableFocus();

        mapOverlay.SetActive(false);
        horizontalScale.gameObject.SetActive(false);
        verticalScale.gameObject.SetActive(false);
    }

    protected override void Zoom(float zoom)
    {
        base.Zoom(zoom);
        horizontalScale.UpdateScale();
        verticalScale.UpdateScale();
    }

    protected override void Move(Vector2 delta)
    {
        base.Move(delta);
        Vector2 mapDelta = transform.position - startPos;
        currentPos = normalizedStartPos - (mapDelta / MAP_SCALE_POS_RATIO / localScale);
        horizontalScale.ScalePos = currentPos.x;
        verticalScale.ScalePos = currentPos.y;
    }

    //MapPos is normalized value between 0 and 1
    public Vector2 MapPosToWorldPos(Vector2 mapPos)
    {
        return new Vector2(mapPos.x / MapWidth * worldWidth, mapPos.y / MapHeight * worldHeight);
    }

    public Vector2 MapCoordinateToWorldPos(string mapCoordinate)
    {
        return new Vector2();
    }
}
