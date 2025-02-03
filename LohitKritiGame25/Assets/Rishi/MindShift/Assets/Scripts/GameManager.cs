using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int movesLeft = 10;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void MakeMove()
    {
        movesLeft--;
        if (movesLeft <= 0)
        {
            Debug.Log("Out of Moves! Game Over.");
            SceneManager.LoadScene("MainMenu");
        }
    }
}