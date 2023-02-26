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

    [SerializeField]
    [Tooltip("With a scale of 1 how big is the map")]
    protected float mapScaleSizeRatio = 10;
    public float MapScaleSizeRatio => mapScaleSizeRatio;

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
    private readonly Vector2 normalizedStartPos = new Vector2(0.5f, 0.5f);
    private Vector2 mapSize;


    //public const float MAP_SCALE_POS_RATIO = 10; //Scale 1 = distance 10

    protected override void Awake()
    {
        base.Awake();
        FocusHandler = focusHandler;
        MapWidth = Mathf.Abs(transform.localScale.x) * MapScaleSizeRatio;
        MapHeight = Mathf.Abs(transform.localScale.z) * MapScaleSizeRatio;
        mapSize = new Vector2(MapWidth, MapHeight);
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
            verticalScale.gameObject.SetActive(true);
            horizontalScale.UpdateZoom();
            verticalScale.UpdateZoom();
            horizontalScale.NormalizedScalePos = currentPos.x;
            verticalScale.NormalizedScalePos = currentPos.y;
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
        zoom = horizontalScale.SetZoomBoundaries(zoom);
        zoom = verticalScale.SetZoomBoundaries(zoom);

        if(zoom != 0)
        {
            base.Zoom(zoom);
            horizontalScale.UpdateZoom();
            verticalScale.UpdateZoom();

            Move(Vector2.zero);
        }
    }

    protected override void Move(Vector2 delta)
    {
        //base.Move(delta);
        delta *= Time.deltaTime * moveSpeed;
        //transform.Translate(new Vector3(delta.x, 0, delta.y), Space.Self);

        float boundX = horizontalScale.GetPosBoundaries();
        float boundY = verticalScale.GetPosBoundaries();

        float x = Mathf.Clamp(transform.position.x + delta.x, startPos.x - boundX, startPos.x + boundX);
        float y = Mathf.Clamp(transform.position.y + delta.y, startPos.y - boundY, startPos.y + boundY);

        transform.position = new Vector3(x, y, transform.position.z);

        Vector2 mapDelta = transform.position - startPos;
        currentPos = normalizedStartPos - (mapDelta / mapSize);
        horizontalScale.NormalizedScalePos = currentPos.x;
        verticalScale.NormalizedScalePos = currentPos.y;
    }

    public Vector3 MapPosToWorldPos(Vector2 mapPos)
    {
        return new Vector3(mapPos.x / MapScaleSizeRatio * worldWidth, 0, mapPos.y / MapScaleSizeRatio * worldHeight);
    }

    public Vector3 MapCoordinateToWorldPos(int x, int y)
    {
        return new Vector3(x / horizontalScale.Range * worldWidth, 0, y / verticalScale.Range * worldHeight);
    }

    public Vector2 WorldPosToMapCoordinate(Vector2 worldPos)
    {
        return new Vector2(worldPos.x / worldWidth * horizontalScale.Range, worldPos.y / worldHeight * verticalScale.Range);
    }

    public Vector2 WorldPosToMapPos(Vector2 worldPos)
    {
        return new Vector2(worldPos.x / worldWidth, worldPos.y / worldHeight) * MapScaleSizeRatio;
    }
}
