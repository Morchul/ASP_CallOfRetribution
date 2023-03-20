using UnityEngine;

public class TargetItem : MonoBehaviour
{
    [SerializeField]
    private GameEvent ItemStolenEvent;

    public void Steal()
    {
        ItemStolenEvent.RaiseEvent();
    }
}
