using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private SlingShot slingshot;
    [SerializeField] private Bird[] birds;
    private int currentBirdIndex;
    public static GameState currentGameState = GameState.Start;
    private Pig[] pigs;
    private UIController uiController;

    public GameState CurrentGameState => currentGameState;

    void Start()
    {
        uiController = FindObjectOfType<UIController>();
        birds = FindObjectsOfType<Bird>();
        pigs = FindObjectsOfType<Pig>();
        currentGameState = GameState.Start;
        slingshot.enabled = false;
        slingshot.BirdThrown -= Slingshot_BirdThrown; slingshot.BirdThrown += Slingshot_BirdThrown;
    }

    void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.Start:
                if (Input.GetMouseButtonUp(0))
                    AnimateBirdToSlingshot();
                break;
            case GameState.BirdMovingToSlingshot:
                break;
            case GameState.Playing:
                if (slingshot.slingshotState == SlingshotState.BirdFlying && (Time.time - slingshot.TimeSinceThrown > 7f))
                {
                    slingshot.enabled = false;
                    AnimateCamera_ToStartPosition();
                    currentGameState = GameState.BirdMovingToSlingshot;
                }
                break;
            case GameState.Won:
                if (pigs.Length == pigs.Length - 1)
                {
                    uiController.ToggleUI(true);
                    uiController.ToggleWinScreen(true);
                }
                break;
            case GameState.Lost:
                if (Input.GetMouseButtonUp(0))
                {
                    uiController.ToggleUI(true);
                    uiController.ToggleLoseScreen(true);
                }
                break;
            default:
                break;
        }
    }
    
    private bool VerifyEndGame()
    {
        return pigs.All(x => x == null);
    }

    private void AnimateCamera_ToStartPosition()
    {
        float duration = Vector2.Distance(Camera.main.transform.position, cameraFollow.StartingPosition) / 10f;
        if (duration == 0.0f) duration = 0.1f;
        //animate the camera to start
        Camera.main.transform.positionTo
            (duration,
            cameraFollow.StartingPosition). //end position
            setOnCompleteHandler((x) =>
            {
                cameraFollow.IsFollowing = false;
                if (VerifyEndGame())
                    currentGameState = GameState.Won;
                else if (currentBirdIndex == birds.Length - 1)
                    currentGameState = GameState.Lost;
                else
                {
                    slingshot.slingshotState = SlingshotState.Idle;
                    currentBirdIndex++;
                    AnimateBirdToSlingshot();
                }
            });
    }

    void AnimateBirdToSlingshot()
    {
        currentGameState = GameState.BirdMovingToSlingshot;
        birds[currentBirdIndex].transform.positionTo
            (Vector2.Distance(birds[currentBirdIndex].transform.position / 10,
            slingshot.BirdWaitPosition.transform.position) / 10, //duration
            slingshot.BirdWaitPosition.transform.position). //final position
                setOnCompleteHandler((x) =>
                        {
                            x.complete();
                            x.destroy();
                            currentGameState = GameState.Playing;
                            slingshot.enabled = true;
                            slingshot.BirdToThrow = birds[currentBirdIndex].gameObject;
                        });
    }

    private void Slingshot_BirdThrown(object sender, System.EventArgs e)
    {
        cameraFollow.BirdToFollow = birds[currentBirdIndex].transform;
        cameraFollow.IsFollowing = true;
    }
}
