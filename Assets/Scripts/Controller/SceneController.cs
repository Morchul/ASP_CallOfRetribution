using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneController", menuName = "Controller/SceneController")]
public class SceneController : ScriptableObject
{
    [Header("Scenes")]
    [SerializeField]
    [Scene]
    private string mainLevel;

    [SerializeField]
    [Scene]
    private string commanderScene;

    [SerializeField]
    [Scene]
    private string loadingScreen;

    [SerializeField]
    [Scene]
    private string mainMenu;

    [SerializeField]
    [Scene]
    private string thiefTutorial;

    [Header("Events")]
    public GameEvent OnMissionLoaded;

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenu, LoadSceneMode.Single);
    }

    public void LoadThiefTutorial()
    {
        SceneManager.LoadScene(thiefTutorial, LoadSceneMode.Single);
    }

    public void LoadMissionForThief(Mission mission)
    {
        LoadScene(loadingScreen, LoadingScreenLoaded, LoadSceneMode.Single);

        void LoadingScreenLoaded()
        {
            LoadScene(mainLevel, MainLevelLoaded);
        }

        void MainLevelLoaded()
        {
            LoadScene(mission.MissionScene, MissionLoaded);
        }

        void MissionLoaded()
        {
            UnloadScene(loadingScreen, LoadingScreenUnloaded);
        }

        void LoadingScreenUnloaded()
        {
            OnMissionLoaded.RaiseEvent();
        }
    }

    public void LoadCommanderView(bool onlyCommander = true)
    {
        LoadScene(loadingScreen, LoadingScreenLoaded, (onlyCommander) ? LoadSceneMode.Single : LoadSceneMode.Additive);

        void LoadingScreenLoaded()
        {
            LoadScene(commanderScene, CommanderSceneLoaded);
        }

        void CommanderSceneLoaded()
        {
            UnloadScene(loadingScreen, LoadingScreenUnloaded);
        }

        void LoadingScreenUnloaded()
        {
            if(onlyCommander)
                OnMissionLoaded.RaiseEvent();
        }
    }

    public void UnloadCommanderView()
    {
        SceneManager.UnloadSceneAsync(commanderScene);
    }

    private void LoadScene(string scene, System.Action afterLoad, LoadSceneMode mode = LoadSceneMode.Additive)
    {
        SceneManager.LoadSceneAsync(scene, mode).completed += ((_) => afterLoad());
    }

    private void UnloadScene(string scene, System.Action afterUnload)
    {
        SceneManager.UnloadSceneAsync(scene).completed += ((_) => afterUnload());
    }
}
