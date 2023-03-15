using UnityEngine;

public class WorldObjectRefPos : MonoBehaviour
{
    [SerializeField]
    private PosUpdateEvent OnPosUpdateEvent;

    [SerializeField]
    private char identifier;

    [SerializeField]
    private MapData mapData;

    private void Awake()
    {
        OnPosUpdateEvent.AddListener(UpdatePos);
    }

    private void UpdatePos(PosUpdateEvent.PosUpdate posUpdate)
    {
        if(posUpdate.Identifier == identifier)
        {
            Vector2 mapPos = mapData.WorldPosToMapPos(posUpdate.Pos);
            transform.localPosition = mapPos.ToVector3();
        }
    }
}
