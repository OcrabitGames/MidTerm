using UnityEngine;

public class Dropper : MonoBehaviour
{
    private Camera _cam;
    public GameObject[] droppables;
    public GameObject leftMarker;
    public GameObject rightMarker;

    public float hover_level = 3f;
    public float hover_buffer = 0.5f;
    private Vector3 hover_position;

    private Scoreboard scoreScript;

    public float cooldown = 1f;
    private float _cooldown;
    private bool _onCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cam = Camera.main;
        scoreScript = GetComponent<Scoreboard>();
        hover_position = new Vector3(0f, hover_level, 0f);
        _cooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        // Follow
        UpdateHoverPos();
        UpdateDropper();

        var mouseClicked = Input.GetMouseButtonDown(0);
        if (mouseClicked)
        {
            if (!_onCooldown) {
                SpawnCandy();
            }
        }
        if (_onCooldown) {
            if (_cooldown <= 0) {
                _cooldown = cooldown;
                _onCooldown = false;
            } else {
                _cooldown -= Time.deltaTime;
            }
        }
    
    }

    void UpdateHoverPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0f;

        Vector3 worldPoint = _cam.ScreenToWorldPoint(mousePos);

        //Bounds
        float leftBound = leftMarker.transform.position.x + hover_buffer;
        float rightBound = rightMarker.transform.position.x - hover_buffer;

        hover_position.x = Mathf.Clamp(worldPoint.x, leftBound, rightBound);
    }

    void UpdateDropper() {
        this.transform.position = hover_position;
    }

    GameObject GetRandomDrop() {
        int i = Random.Range(0, droppables.Length);
        return droppables[i];
    }
   
    void SpawnCandy() {
        Instantiate(GetRandomDrop(), hover_position, Quaternion.identity);
        scoreScript.IncreamentScore();
        _onCooldown = true;
    }
}
