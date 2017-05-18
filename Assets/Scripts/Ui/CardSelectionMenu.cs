using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionMenu : MonoBehaviour
{
    [Header("Available Cards Panel")]

    [SerializeField]
    private GameObject _availableCardsPanel;

    //[SerializeField]
    //private int _rowLengthAC = 4;

    [SerializeField]
    private Color _selectionColorA;

    [SerializeField]
    private Color _selectionColorB;

    private List<UiCardButton> _cardButtonsAC = new List<UiCardButton>();
    //private UiCardButton _currentSelectionA;
    //private UiCardButton _currentSelectionB;

    // Use this for initialization
    void Start()
    {
        LoadAvailableCards();

        //_currentSelectionA = _cardButtonsAC[0];
        //_cardButtonsAC[0].SetSelectionColor(_selectionColorA);

        //_currentSelectionB = _cardButtonsAC[_rowLengthAC - 1];
        //_cardButtonsAC[_rowLengthAC - 1].SetSelectionColor(_selectionColorB);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadAvailableCards()
    {
        List<Card> cards = CardManager.Instance.GetAllCards();

        foreach (Card card in cards)
        {
            GameObject cardButton = Instantiate(Resources.Load<GameObject>("Prefabs/Ui/CardButton"), _availableCardsPanel.transform);
            cardButton.GetComponent<UiCardButton>().Initialize(card);
            _cardButtonsAC.Add(cardButton.GetComponent<UiCardButton>());
        }
    }
}
