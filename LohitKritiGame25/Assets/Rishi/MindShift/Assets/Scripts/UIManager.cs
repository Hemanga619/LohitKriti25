using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text movesText;

    void Update()
    {
        movesText.text = "Moves Left: " + GameManager.instance.movesLeft;
    }
}