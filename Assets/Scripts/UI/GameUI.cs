using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] PlayerScreenUI playerScreen;
    [SerializeField] GameScreenUI gameScreen;
    [SerializeField] GameObject pieceSelector;
    [SerializeField] GameObject cellIndicator;
    [SerializeField] GameObject eatIndicator;

    private Piece selectedPiece = null;
    private int totalTime = 0;
    private int partialTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameManager gameMng = GameManager.Instance;
        gameMng.StartGame();
        playerScreen.ShowPlayer(gameMng.CurrentPlayer);
        StartCoroutine(GameTimer());
    }

    bool IsPiece(GameObject obj)
    {
        return obj.CompareTag("Piece");
    }

    bool IsCellIndicator(GameObject obj)
    {
        return obj.CompareTag("Cell Indicator");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager gameMng = GameManager.Instance;
            GameObject obj = GetSelected();

            if (obj != null)
            {
                if (IsPiece(obj))
                {
                    Piece piece = obj.GetComponent<Piece>();

                    if (piece.GetColor() == gameMng.CurrentPlayer.Color)
                    {
                        SelectPiece(obj);
                    }
                    else if (selectedPiece != null && IsEatable(piece))
                    {
                        EatPiece(piece);
                    }
                }
                else if (IsCellIndicator(obj))
                {
                    if (selectedPiece != null)
                    {
                        MoveSelectedPiece(obj);
                    }
                }
            }
        }
    }

    bool IsEatable(Piece piece)
    {
        Board.Position position = piece.GetPosition();
        List<Board.Position> eatables = selectedPiece.GetEatable();

        return eatables.Find(p => p.Column == position.Column && p.Row == position.Row) != null;
    }

    void SelectPiece(GameObject pieceObj)
    {
        Piece piece = pieceObj.GetComponent<Piece>();
        if (piece.GetColor() == GameManager.Instance.CurrentPlayer.Color)
        {
            selectedPiece = piece;
            playerScreen.ShowPiece(selectedPiece);

            pieceSelector.SetActive(true);
            Vector3 piecePosition = Board.ToCoords(piece.GetPosition());
            pieceSelector.transform.position = piecePosition;

            HideMovements();
            List<Board.Position> positions = selectedPiece.GetAllowedMovements();
            ShowMovements(positions);

            HideEatables();
            List<Board.Position> eatables = selectedPiece.GetEatable();
            ShowEatables(eatables);
        }
    }

    void NextTurn()
    {
        GameManager gameMng = GameManager.Instance;
        GameManager.PlayerData playerData = gameMng.CurrentPlayer;

        selectedPiece = null;
        pieceSelector.SetActive(false);

        if (Win(playerData))
        {
            gameMng.GameOver();
        }
        else
        {
            playerData.TimeCount = partialTime;
            gameMng.NextTurn();
            playerData = gameMng.CurrentPlayer;
            partialTime = playerData.TimeCount;

            playerScreen.ShowPlayer(gameMng.CurrentPlayer);
            playerScreen.ShowPiece(null);
        }
    }

    bool Win(GameManager.PlayerData player)
    {
        int count = 0;
        GameObject[] pieceObjs = GameObject.FindGameObjectsWithTag("Piece");
        foreach (GameObject pieceObj in pieceObjs)
        {
            Piece piece = pieceObj.GetComponent<Piece>();
            if (piece.GetColor() == PlayerSettings.GetOther(player.Color))
            {
                ++count;
            }
        }

        return count == 0;
    }

    void MoveSelectedPiece(GameObject targetPosition)
    {
        selectedPiece.MoveTo(Board.ToPosition(targetPosition.transform.position));
        HideMovements();

        NextTurn();
    }

    void EatPiece(Piece piece)
    {
        selectedPiece.Eat(piece);
        HideEatables();
        HideMovements();

        NextTurn();
    }

    void ShowMovements(List<Board.Position> positions)
    {
        Show(cellIndicator, positions);
    }

    void ShowEatables(List<Board.Position> positions)
    {
        Show(eatIndicator, positions);
    }

    void Show(GameObject prefab, List<Board.Position> positions)
    {
        foreach (Board.Position position in positions)
        {
            Instantiate(prefab, Board.ToCoords(position), Quaternion.identity);
        }
    }

    void HideMovements()
    {
        Hide("Cell Indicator");
    }

    void HideEatables()
    {
        Hide("Eat Indicator");
    }

    void Hide(string tag)
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
        {
            Destroy(obj);
        }
    }

    GameObject GetSelected()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform != null)
            {
                return hit.transform.gameObject;
            }
        }

        return null;
    }

    IEnumerator GameTimer()
    {
        GameManager gameMng = GameManager.Instance;

        while (gameMng.GameActive)
        {
            yield return new WaitForSeconds(1.0f);

            ++partialTime;
            ++totalTime;

            gameScreen.SetTime(partialTime, totalTime);
        }
    }
}
