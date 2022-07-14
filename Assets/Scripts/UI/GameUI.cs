using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] PlayerScreenUI playerScreen;
    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject pieceSelector;
    [SerializeField] GameObject cellIndicator;

    private Piece selectedPiece = null;

    // Start is called before the first frame update
    void Start()
    {
        GameManager gameMng = GameManager.Instance;
        gameMng.StartGame();
        playerScreen.ShowPlayer(gameMng.GetCurrentPlayer());
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
            GameObject obj = GetSelected();
            if (obj != null && IsPiece(obj))
            {
                SelectPiece(obj);
            }
            if (obj != null && IsCellIndicator(obj) && selectedPiece != null)
            {
                MoveSelectedPiece(obj);
            }
        }
    }

    void SelectPiece(GameObject pieceObj)
    {
        Piece piece = pieceObj.GetComponent<Piece>();
        if (piece.GetColor() == GameManager.Instance.GetCurrentPlayer().GetColor())
        {
            selectedPiece = piece;
            playerScreen.ShowPiece(selectedPiece);

            pieceSelector.SetActive(true);
            Vector3 piecePosition = Board.ToCoords(piece.GetPosition());
            pieceSelector.transform.position = piecePosition;

            LightOff();
            List<Board.Position> positions = selectedPiece.GetAllowedMovements();
            Highlight(positions);
        }
    }

    void MoveSelectedPiece(GameObject targetPosition)
    {
        selectedPiece.MoveTo(Board.ToPosition(targetPosition.transform.position));
        LightOff();

        pieceSelector.SetActive(false);
        GameManager gameMng = GameManager.Instance;
        gameMng.NextTurn();

        playerScreen.ShowPlayer(gameMng.GetCurrentPlayer());
    }

    void Highlight(List<Board.Position> positions)
    {
        foreach (Board.Position position in positions)
        {
            Instantiate(cellIndicator, Board.ToCoords(position), Quaternion.identity);
        }
    }

    void LightOff()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Cell Indicator"))
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
}
