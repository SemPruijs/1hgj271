using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public State state;

    public int coins;
    private static GameManager _instance;
         public static GameManager Instance {
         get {
             return _instance;
         }
    }
    private void Awake()
    {
         if (_instance != null && _instance != this)
         {
            Destroy(this.gameObject);
         } else {
            _instance = this;
         } 
    }

    public enum State
    {
        InGame,
        Menu,
        PlayAgain,
        Credit
    };

    void Start()
    {
        Menu();
        // CameraShaker.Instance.ShakeOnce(3f, 1f, 3f, 3f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentScene();
        }
    }

    public void Menu()
    {
        state = State.Menu;
    }

    public void InGame()
    {
        state = State.InGame;
    }

    public void Credit()
    {
        state = State.Credit;
    }
    
    public void ReloadCurrentScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
