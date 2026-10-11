using System.Collections;
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
    public GameObject playerObj;
    public GameManager manager;

    void Start()
    {
        StartCoroutine(SetEdges());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > EdgeX || transform.position.x < NegEdgeX)
        {
            Debug.Log("X Threshold Reached");
            targetObj.SetActive(true);
            manager.WinRewards();
            Destroy(playerObj);
        }

        if (transform.position.y > EdgeY || transform.position.y < NegEdgeY)
        {
            Debug.Log("Y Threshold Reached");
            targetObj.SetActive(true);
            manager.WinRewards();
            Destroy(playerObj);
        }
    }

    IEnumerator SetEdges() 
    {
        while (manager.width == 0 && manager.height == 0) 
        {
            yield return null;
        }

        EdgeX = (manager.width - 1) / 4f;
        EdgeY = (manager.height - 1) / 4f;

        NegEdgeX = EdgeX - (EdgeX * 2);
        NegEdgeY = EdgeY - (EdgeY * 2);

        yield return null;
    }
}
