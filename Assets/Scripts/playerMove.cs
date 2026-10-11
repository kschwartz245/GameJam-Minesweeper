using System.Collections;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    [SerializeField] private bool isRepeatedMovement = false;
    [SerializeField] private float moveDuration = 0.1f;
    [SerializeField] private float gridSize = 1f;
    private bool isMoving = false;
    public bool isJumping = false;
    void Update()
    {
        if (!isMoving)
        {
            System.Func<KeyCode, bool> inputFunction;
            if (isRepeatedMovement)
            {
                inputFunction = Input.GetKey;
            }
            else
            {
                inputFunction = Input.GetKeyDown;
            }

            if (!isJumping)
            {
                if (inputFunction(KeyCode.UpArrow))
                {
                    StartCoroutine(Move(Vector2.up));
                }
                else if (inputFunction(KeyCode.DownArrow))
                {
                    StartCoroutine(Move(Vector2.down));
                }
                else if (inputFunction(KeyCode.LeftArrow))
                {
                    StartCoroutine(Move(Vector2.left));
                }
                else if (inputFunction(KeyCode.RightArrow))
                {
                    StartCoroutine(Move(Vector2.right));
                }
            } 
            else
            {
                if (inputFunction(KeyCode.UpArrow))
                {
                    StartCoroutine(Jump(Vector2.up));
                }
                else if (inputFunction(KeyCode.DownArrow))
                {
                    StartCoroutine(Jump(Vector2.down));
                }
                else if (inputFunction(KeyCode.LeftArrow))
                {
                    StartCoroutine(Jump(Vector2.left));
                }
                else if (inputFunction(KeyCode.RightArrow))
                {
                    StartCoroutine(Jump(Vector2.right));
                }
            }
        }
    }

    private IEnumerator Move(Vector2 direction)
    {
        isMoving = true;

        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (direction * gridSize);

        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveDuration;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent);
            yield return null;
        }
        transform.position = endPosition;
        isMoving = false;
    }

    private IEnumerator Jump(Vector2 direction)
    {
        isMoving = true;
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (direction * gridSize)*2;
        transform.position = endPosition;
        isMoving = false;
        isJumping = false;
        yield return null;
    }
}
