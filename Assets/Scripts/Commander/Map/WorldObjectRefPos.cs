using UnityEngine;

public class WorldObjectRefPos : MonoBehaviour
{
    [SerializeField]
    private PosUpdateEvent OnPosUpdateEvent;

    [SerializeField]
    private char identifier;

    [SerializeField]
    private MapData mapData;

    // Start is called before the first frame update
    void Awake()
    {
        OnPosUpdateEvent.AddListener(UpdatePos);
    }

    private void UpdatePos(PosUpdateEvent.PosUpdate posUpdate)
    {
        if(posUpdate.Identifier == identifier)
        {
            Vector2 mapPos = mapData.XZWorldPosToMapPos(posUpdate.Pos);
            transform.localPosition = mapPos.ToVector3();
        }
    }
}
