using UnityEngine;

[CreateAssetMenu(fileName = "ProgressController", menuName = "Controller/ProgressController")]
public class ProgressController : ScriptableObject
{
    public const string MISSION_PROGRESS = "Mission";

    public int GetCurrentProgress()
    {
        return PlayerPrefs.GetInt(MISSION_PROGRESS, 0);
    }

    public void SetCurrentProgress(int progress)
    {
        PlayerPrefs.SetInt(MISSION_PROGRESS, progress);
        PlayerPrefs.Save();
    }
}
