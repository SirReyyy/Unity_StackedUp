using UnityEngine;
using UnityEngine.UIElements;

public class UIGameplay_Script : MonoBehaviour {

    public StackHolder_Script _stackHolderScript;
    public Label lblScore, lblCombo;

    void OnEnable() {
        int currentScore = _stackHolderScript._scoreCount;
        int currentCombo = _stackHolderScript._comboCount;

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        lblScore = root.Q<Label>("Lbl_Score");
        lblCombo = root.Q<Label>("Lbl_Combo");
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
                lblCombo.text = "*** GREAT ***";
                break;
        }
        
    } // -- UpdateLblCombo function
} // -- End


/*

Made by : Rey M. Oronos, Jr.
Project : Stacked Up

*/