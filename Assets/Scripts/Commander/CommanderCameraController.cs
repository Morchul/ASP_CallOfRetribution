using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FocusObject;

public class CommanderCameraController : MonoBehaviour
{
    private FocusAnimationParam startPos;
    private FocusAnimationParam focusPos;

    [SerializeField]
    [Tooltip("Animation duration is: [1 / this value]")]
    private float animationMultiplier;

    public bool Animate { get; private set; } = false;
    private float timer;
    private bool focusObject = false;

    private System.Action onFinishedAnimation;

    private void Awake()
    {
        startPos = new FocusAnimationParam(transform);
    }

    public void FocusObject(Transform focus)
    {
        if (focusObject || Animate) return; //Already focusing something or in animation
        this.focusObject = true;
        focusPos = new FocusAnimationParam(focus);
        StartAnimation();
    }

    public void StopFocus(System.Action onFinishedAnimation)
    {
        if (Animate) return;
        focusObject = false;
        this.onFinishedAnimation = onFinishedAnimation;

        var tmp = startPos;
        startPos = focusPos;
        focusPos = tmp;

        StartAnimation();
    }

    private void StartAnimation()
    {
        Animate = true;
        timer = 0;
    }

    private void Update()
    {
        if (Animate)
        {
            if (timer <= 1)
            {
                timer += Time.deltaTime * animationMultiplier;
                FocusAnimationParam param = FocusAnimationParam.Lerp(startPos, focusPos, timer);
                transform.position = param.Pos;
                transform.eulerAngles = param.Rot;
            }
            else
            {
                Animate = false;
                if (!focusObject)
                {
                    startPos = focusPos;
                    onFinishedAnimation?.Invoke();
                }
            }
        }
    }
}
