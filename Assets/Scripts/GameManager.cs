using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager manager = null;
    private enum GameState { Game, End };
    

    public GUIStyle style1;
    public GUIStyle style2;
    private static int score = 0;
    private float timer = 30.0f;

    private string score_txt = "";
    private string time_text = "";

    GameState gameState = GameState.Game;

    public static GameManager Manager
    {
        get
        {
            if (manager == null) manager = new GameObject("GameManager").AddComponent<GameManager>();
            return manager;
        }
    }

    void Awake()
    {
        if((manager) && (manager.GetInstanceID() != GetInstanceID()))
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }

        manager = this;
    }
    
    void Start()
    {
        score = 0;
        gameState = GameState.Game;
    }
    void Update()
    {
        score_txt = score.ToString();
        time_text = ((int)(timer)).ToString();
        if (gameState == GameState.Game)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EndGame();
                gameState = GameState.End;
            }
        }
    }
    void OnGUI()
    {
        
        if (gameState == GameState.Game)
        {
            DisplayInGameHUD();
        }
        if (gameState == GameState.End)
        {
            DisplayGameOverScreen();
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
    }
    public void EndGame()
    {
        Application.LoadLevel("scene2");
    }
    void DisplayGameOverScreen()
    {
        GUI.Label(new Rect(Screen.width/2 - 50, Screen.height/2, 100, 30), "Game Over!", style1);
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 30, 100, 30), "Score: " + score, style1);
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 60, 60, 20), "restart", style2))
        {
            Application.LoadLevel("scene1");
            Destroy(gameObject);
        }
        if (GUI.Button(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 60, 60, 20), "quit", style2)) Application.Quit();
    }
    void DisplayInGameHUD()
    {
        GUI.Label(new Rect(40, 40, 80, 20), "your score: " + score_txt, style1);
        GUI.Label(new Rect(40, 70, 80, 20), "your time:  " + time_text, style1);
        GUI.Label(new Rect(40, 100, 120, 20), "Drag ball to shoot", style1);
        GUI.Label(new Rect(40, 130, 120, 20), "Mouse wheel to rotate", style1);

    }
}
