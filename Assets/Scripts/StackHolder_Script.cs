using UnityEngine;

public class StackHolder_Script : MonoBehaviour {

    // Stack Color Array
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
    private int scoreCount = 0;
    private int comboCount = 0;
    
    private float tileTransition = 0.0f;
    private float tileSpeed = 2.5f;
    private float secondaryPosition;

    // Tile States
    private bool isMovingX = false;
    private bool gameOver = false;

    private Vector3 targetPosition;
    private Vector3 lastTilePosition;


    void Start() {
        stackElements = new GameObject[transform.childCount];

        for(int i = 0; i < transform.childCount; i++) {
            stackElements[i] = transform.GetChild(i).gameObject;
            ColorMesh(stackElements[i].GetComponent<MeshFilter>().mesh);
        }

        stackIndex = transform.childCount - 1;
    } // -- Start function


    void Update() {
        if(gameOver)
            return;
        
        if(Input.GetMouseButtonDown(0)) {
            if(PlaceTile()) {
                SpawnNewTile();
                scoreCount++;
            } else {
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            }
        }

        MoveTile();
        transform.position = Vector3.Lerp(transform.position, targetPosition, STACK_MOVE_SPEED * Time.deltaTime);
    } // -- Update function

    
    void ColorMesh(Mesh mesh) {
        Color randColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        Vector3[] vertices = mesh.vertices;
        Color32[] colors = new Color32[vertices.Length];

        float f = Mathf.Sin(scoreCount * 0.25f);

        for(int i = 1; i < vertices.Length; i++) {
            colors[i] = randColor;
        }

        mesh.colors32 = colors;
    } // -- ColorMesh function


    void MoveTile() {
        tileTransition += Time.deltaTime * tileSpeed;

        if(isMovingX) {
            stackElements[stackIndex].transform.localPosition = new Vector3(Mathf.Sin(tileTransition) * BOUNDS_SIZE, scoreCount, secondaryPosition);
        } else {
            stackElements[stackIndex].transform.localPosition = new Vector3(secondaryPosition, scoreCount, Mathf.Sin(tileTransition) * BOUNDS_SIZE);
        }

    } // -- MoveTile function

    bool PlaceTile() {
        Transform t = stackElements[stackIndex].transform;

        if(isMovingX) {
            float deltaX = lastTilePosition.x - t.position.x;

            if(Mathf.Abs(deltaX) > ERROR_MARGIN) {
                comboCount = 0;
                stackBounds.x -= Mathf.Abs(deltaX);

                if(stackBounds.x <= 0) {
                    return false;
                }

                float middle = lastTilePosition.x + t.localPosition.x / 2.0f;
                t.localScale = new Vector3(stackBounds.x, 1.0f, stackBounds.y);

                CreateSlicedTile(
                    new Vector3((t.position.x > 0) ? t.position.x + (t.localScale.x / 2.0f) : t.position.x - (t.localScale.x / 2.0f), t.position.y, t.position.z),
                    new Vector3(Mathf.Abs(deltaX), 1.0f, t.localScale.z)
                );

                t.localPosition = new Vector3(middle - (lastTilePosition.x / 2.0f), scoreCount, lastTilePosition.z);
            } else {
                if(comboCount > COMBO_START_GAIN) {
                    stackBounds.x += COMBO_START_GAIN;

                    if(stackBounds.x > BOUNDS_SIZE)
                        stackBounds.x = BOUNDS_SIZE;
                    
                    float middle = lastTilePosition.x + t.localPosition.x / 2.0f;
                    t.localScale = new Vector3(stackBounds.x, 1.0f, stackBounds.y);
                    t.localPosition = new Vector3(middle - (lastTilePosition.x / 2.0f), scoreCount, lastTilePosition.z);
                }

                comboCount++;
                t.localPosition = new Vector3(lastTilePosition.x, scoreCount, lastTilePosition.z);
            }

        } else {
            float deltaZ = lastTilePosition.z - t.position.z;

            if(Mathf.Abs(deltaZ) > ERROR_MARGIN) {
                comboCount = 0;
                stackBounds.y -= Mathf.Abs(deltaZ);

                if(stackBounds.y <= 0) {
                    return false;
                }

                float middle = lastTilePosition.z + t.localPosition.z / 2.0f;
                t.localScale = new Vector3(stackBounds.x, 1.0f, stackBounds.y);

                CreateSlicedTile(
                    new Vector3(t.position.x, t.position.y, (t.position.z > 0) ? t.position.z + (t.localScale.z / 2.0f) : t.position.z - (t.localScale.z / 2.0f)),
                    new Vector3(t.localScale.x, 1.0f, Mathf.Abs(deltaZ))
                );

                t.localPosition = new Vector3(lastTilePosition.x / 2.0f, scoreCount, middle - (lastTilePosition.z / 2.0f));
            } else {
                if(comboCount > COMBO_START_GAIN) {
                    stackBounds.y += COMBO_START_GAIN;

                    if(stackBounds.y > BOUNDS_SIZE)
                        stackBounds.y = BOUNDS_SIZE;
                    
                    float middle = lastTilePosition.z + t.localPosition.z / 2.0f;
                    t.localScale = new Vector3(stackBounds.x, 1.0f, stackBounds.y);
                    t.localPosition = new Vector3(lastTilePosition.x, scoreCount, middle - (lastTilePosition.z / 2.0f));
                }

                comboCount++;
                t.localPosition = new Vector3(lastTilePosition.x, scoreCount, lastTilePosition.z);
            }
        }

        secondaryPosition = (isMovingX) ? t.localPosition.x : t.localPosition.z;
        isMovingX = !isMovingX;

        return true;

    } // -- PlaceTile function

    void CreateSlicedTile(Vector3 pos, Vector3 scale) {
        GameObject slicedTile = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        slicedTile.transform.localPosition = pos;
        slicedTile.transform.localScale = scale;
        slicedTile.AddComponent<Rigidbody>();

        slicedTile.GetComponent<MeshRenderer>().material = stackMat;
        ColorMesh(slicedTile.GetComponent<MeshFilter>().mesh);
    } // -- CreateSlicedTile

    void SpawnNewTile() {
        lastTilePosition = stackElements[stackIndex].transform.localPosition;
        stackIndex--;

        if(stackIndex < 0) {
            stackIndex = transform.childCount - 1;
        }

        targetPosition = Vector3.down * scoreCount;
        stackElements[stackIndex].transform.localPosition = new Vector3(0.0f, scoreCount, 0.0f);
        stackElements[stackIndex].transform.localScale = new Vector3(stackBounds.x, 1.0f, stackBounds.y);

        ColorMesh(stackElements[stackIndex].GetComponent<MeshFilter>().mesh);

    } // -- SpawnNewTile function
}


/*

Made by : Rey M. Oronos, Jr.
Project : Stacked Up

*/