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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cam = Camera.main;
        hover_position = new Vector3(0f, hover_level, 0f);
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
            Instantiate(GetRandomDrop(), hover_position, Quaternion.identity);
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
   
}
