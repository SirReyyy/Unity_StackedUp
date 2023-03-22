using UnityEngine;

public class StackHolder_Script : MonoBehaviour {

    // Stack Color Array
    public Color32[] gameColors = new Color32[5];
    public Material stackMat;

    // Game Constant Values
    private const float BOUNDS_SIZE = 3.5f;
    private const float STACK_MOVE_SPEED = 3.0f;
    private const float ERROR_MARGIN = 0.1f;
    private const float STACK_BOUNDS_GAIN = 0.25f;
    private const float COMBO_START_GAIN = 3.0f;

    private GameObject[] stackElements;
    private Vector2 stackBounds = new Vector2(BOUNDS_SIZE, BOUNDS_SIZE);

    // Scoring Variables
    private int stackIndex;
    private int scoreCount = 12;
    private int comboCount = 0;
    
    private float tileTransition = 0.0f;
    private float tileSpeed = 2.5f;
    private float secondaryPosition;

    // Tile States
    private bool isMovingX = true;
    private bool gameOver = false;

    private Vector3 targetTilePosition;
    private Vector3 lastTilePosition;


    void Start() {
        stackElements = new GameObject[transform.childCount];

        for(int i = 0; i < transform.childCount; i++) {
            stackElements[i] = transform.GetChild(i).gameObject;
            ColorMesh(stackElements[i].GetComponent<MeshFilter>().mesh);
        }

        stackIndex = transform.childCount - 1;
    }


    void Update() {
        
    }

    
    void ColorMesh(Mesh mesh) {
        Vector3[] vertices = mesh.vertices;
        Color32[] colors = new Color32[vertices.Length];

        float f = Mathf.Sin(scoreCount * 0.25f);

        for(int i = 0; i < vertices.Length; i++) {
            colors[i] = Lerp5(gameColors[0], gameColors[1], gameColors[2], gameColors[3], gameColors[4], f);
        }

        mesh.colors32 = colors;
    }

    Color32 Lerp5(Color32 a, Color32 b, Color32 c, Color32 d, Color32 e, float t) {
        if(t < 0.25f) {
            return Color.Lerp(a, b, t / 0.25f);
        } else if(t < 0.50f) {
            return Color.Lerp(b, c, (t - 0.25f) / 0.25f);
        } else if(t < 0.75f) {
            return Color.Lerp(c, d, (t - 0.50f) / 0.50f);
        } else {
            return Color.Lerp(d, e, (t - 0.75f) / 0.75f);
        }
    }
}


/*

Made by : Rey M. Oronos, Jr.
Project : Stacked Up

*/