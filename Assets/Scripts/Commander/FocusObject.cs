using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusObject : MonoBehaviour, IFocusable
{
    public bool InFocus { get; private set; }
    private FocusAnimationParam startPos;
    private FocusAnimationParam targetPos;

    [SerializeField]
    [Tooltip("Faster than division for a second duration of 2sec use 0.5f")]
    private float animationMultiplier = 0.6f;

    public FocusHandler FocusHandler { get; set; }

    private float timer;
    protected bool animate;

    private void Awake()
    {
        InFocus = false;
    }

    public void DisableFocus()
    {
        InFocus = false;

        SetAnimation(startPos);

        LerpBetweenTransforms();
    }

    public void EnableFocus(FocusAnimationParam focusPos)
    {
        InFocus = true;

        SetAnimation(focusPos);

        LerpBetweenTransforms();
    }

    private void SetAnimation(FocusAnimationParam focusPos)
    {
        if (animate)
        {
            FocusAnimationParam tmp = targetPos;
            targetPos = startPos;
            startPos = tmp;
        }
        else
        {
            targetPos = focusPos;
            startPos = new FocusAnimationParam(this.transform);
        }
    }

    public void SetOriginPosition(Vector3 rot, float yPos)
    {
        if (InFocus)
        {
            startPos.Rot = rot;
            startPos.Pos = new Vector3(startPos.Pos.x, yPos, startPos.Pos.z);
        }
        else
        {
            transform.eulerAngles = rot;
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        }
    }

    public void LerpBetweenTransforms()
    {
        if (animate)
        {
            timer = 1 - timer;
        }
        else
            timer = 0;
            

        animate = true;
    }

    protected virtual void Update()
    {
        if (animate)
        {
            if(timer <= 1)
            {
                timer += Time.deltaTime * animationMultiplier;
                FocusAnimationParam param = FocusAnimationParam.Lerp(startPos, targetPos, timer);
                transform.position = param.Pos;
                transform.eulerAngles = param.Rot;
            }
            else
            {
                animate = false;
            }
        }
    }

    public virtual void OnMouseUp()
    {
        if (!InFocus)
        {
            FocusHandler.SetFocusObject(this);
        }
    }

    public struct FocusAnimationParam
    {
        public Vector3 Pos;
        public Vector3 Rot;

        public FocusAnimationParam(Transform transform)
        {
            Pos = transform.position;
            Rot = GetSignedEulerAngles(transform.eulerAngles);
        }

        public FocusAnimationParam(Vector3 pos, Vector3 rot)
        {
            Pos = pos;
            Rot = GetSignedEulerAngles(rot);
        }

        private static Vector3 GetSignedEulerAngles(Vector3 eulerAngles)
        {
            return new Vector3
                (
                eulerAngles.x > 180 ? eulerAngles.x - 360 : eulerAngles.x,
                eulerAngles.y > 180 ? eulerAngles.y - 360 : eulerAngles.y,
                eulerAngles.z > 180 ? eulerAngles.z - 360 : eulerAngles.z
                );
        }

        public static FocusAnimationParam Lerp(FocusAnimationParam start, FocusAnimationParam end, float t)
        {
            return new FocusAnimationParam(Vector3.Lerp(start.Pos, end.Pos, t), Vector3.Lerp(start.Rot, end.Rot, t));
        }
    }
}
