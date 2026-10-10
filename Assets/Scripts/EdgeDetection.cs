using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EdgeDetection : MonoBehaviour
{
    public float EdgeX;
    private float NegEdgeX;
    public float EdgeY;
    private float NegEdgeY;
    public GameObject targetObj;

    void Start()
    {
        NegEdgeX = EdgeX - (EdgeX * 2);
        NegEdgeY = EdgeY - (EdgeY * 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > EdgeX || transform.position.x < NegEdgeX)
        {
            Debug.Log("X Threshold Reached");
            targetObj.SetActive(true);
            Destroy(this);
        }

        if (transform.position.y > EdgeY || transform.position.y < NegEdgeY)
        {
            Debug.Log("Y Threshold Reached");
            targetObj.SetActive(true);
            Destroy(this);
        }
    }


}
