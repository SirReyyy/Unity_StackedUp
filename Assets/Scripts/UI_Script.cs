using UnityEngine;
using UnityEngine.UIElements;

public class UI_Script : MonoBehaviour {

    public StackHolder_Script _stackHolderScript;
    public VisualElement veScoring, veMainMenu, veGameOver;
    public Label lblScore, lblCombo, lblEndScore;
    public Button btnStart, btnRestart, btnHome;

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

        // Game Over Container
        veGameOver = root.Q<VisualElement>("VE_GameOver");
        btnRestart = root.Q<Button>("Btn_Restart");
        btnHome = root.Q<Button>("Btn_Home");

        veMainMenu.style.display = DisplayStyle.None;
        veScoring.style.display = DisplayStyle.Flex;
        veGameOver.style.display = DisplayStyle.None;
    } // -- OnEnable function

    public void UpdateLblScore(int score) {
        lblScore.text = score.ToString();
    } // -- UpdateLblScore function

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
} // -- End


/*

Made by : Rey M. Oronos, Jr.
Project : Stacked Up

*/