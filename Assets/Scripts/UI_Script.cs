using UnityEngine;
using UnityEngine.UIElements;

public class UI_Script : MonoBehaviour {

    public StackHolder_Script _stackHolderScript;
    public VisualElement veScoring, veMainMenu, veGameOver;
    public Label lblScore, lblCombo, lblEndScore, lblHighScore;
    public Button btnStart, btnReturn;

    void OnEnable() {
        int currentScore = _stackHolderScript._scoreCount;
        int currentCombo = _stackHolderScript._comboCount;

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // Main Menu Container
        veMainMenu = root.Q<VisualElement>("VE_MainMenu");
        btnStart = root.Q<Button>("Btn_Start");

        // Scoring Container
        veScoring = root.Q<VisualElement>("VE_Scoring");
        lblScore = root.Q<Label>("Lbl_Score");
        lblCombo = root.Q<Label>("Lbl_Combo");
        lblHighScore = root.Q<Label>("Lbl_HighScore");

        // Game Over Container
        veGameOver = root.Q<VisualElement>("VE_GameOver");
        lblEndScore = root.Q<Label>("Lbl_EndScore");
        btnReturn = root.Q<Button>("Btn_Return");

        BtnFunction(0);
        btnStart.clicked += () => BtnFunction(1);
        btnReturn.clicked += () => BtnFunction(3);


    } // -- OnEnable function

    public void UpdateLblScore(int score) {
        lblScore.text = score.ToString();
    } // -- UpdateLblScore function
    
    public void UpdateEndScore(int score) {
        lblEndScore.text = score.ToString();
    } // -- UpdateEndScore function

    public void UpdateLblCombo(int combo) {
        switch(combo) {
            case 0:
                lblCombo.text = "";
                break;
            case 1:
                lblCombo.text = "- COMBO -";
                break;
            case 2:
                lblCombo.text = "-- COMBO --";
                break;
            case 3:
                lblCombo.text = "--- COMBO ---";
                break;
            default:
                lblCombo.text = "*- COLOR COMBO -*";
                break;
        }
        
    } // -- UpdateLblCombo function

    public void BtnFunction(int btnFunc) {
        switch(btnFunc) {
            case 1: // First Play State
                veMainMenu.style.display = DisplayStyle.None;
                veScoring.style.display = DisplayStyle.Flex;
                veGameOver.style.display = DisplayStyle.None;

                _stackHolderScript.ChangeState(1);
                break;
            case 2: // Game Over State 
                veMainMenu.style.display = DisplayStyle.None;
                veScoring.style.display = DisplayStyle.None;
                veGameOver.style.display = DisplayStyle.Flex;

                _stackHolderScript.ChangeState(2);
                break;
            case 3: // Home State
                veMainMenu.style.display = DisplayStyle.Flex;
                veScoring.style.display = DisplayStyle.None;
                veGameOver.style.display = DisplayStyle.None;

                _stackHolderScript.ChangeState(3);
                break;
            default: // Initial State
                veMainMenu.style.display = DisplayStyle.Flex;
                veScoring.style.display = DisplayStyle.None;
                veGameOver.style.display = DisplayStyle.None;

                _stackHolderScript.ChangeState(0);
                break;
        }
    }
} // -- End


/*

Made by : Rey M. Oronos, Jr.
Project : Stacked Up

*/