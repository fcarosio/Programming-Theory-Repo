using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreenUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winner;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetWinner(GameManager.PlayerData playerData)
    {
        gameObject.SetActive(true);
        winner.text = playerData.GetName() + " wins!";
    }
}
