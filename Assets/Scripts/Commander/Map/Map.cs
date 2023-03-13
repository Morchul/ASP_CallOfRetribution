using UnityEngine;

public class Map : MovableFocusObject
{
    [SerializeField]
    private FocusHandler focusHandler;

    [SerializeField]
    private Transform mapFocusPos;

    [Header("Map")]
    [SerializeField]
    private MapData mapData;

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
        mapSize = new Vector2(mapData.MapSizeX, mapData.MapSizeY);
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
}
