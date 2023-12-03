using System.Collections;
using UnityEngine;

public class EndSceneScript : MonoBehaviour
{
    public GameObject characters;
    public GameObject title;
    public GameObject all;
    public GameObject panelToShow;
    public GameObject credits;
    public float moveSpeed = 2.0f;
    public float newObjectSpeed = 3.0f; 

    private float targetX = 0f;
    private float creditsStartY;
    private float creditsTargetY = 10f;

    void Start()
    {
        StartCoroutine(MoveToTarget());
        StartCoroutine(MoveNewObject());
    }

    IEnumerator MoveToTarget()
    {
        float startingX = characters.transform.position.x;
        float distanceToMove = Mathf.Abs(startingX - targetX);
        float duration = distanceToMove / moveSpeed;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newX = Mathf.Lerp(startingX, targetX, elapsedTime / duration);
            Vector3 newPosition = new Vector3(newX, characters.transform.position.y, characters.transform.position.z);
            characters.transform.position = newPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        characters.transform.position = new Vector3(targetX, characters.transform.position.y, characters.transform.position.z);
        yield return new WaitForSeconds(3.0f);
        all.SetActive(false);
        panelToShow.SetActive(true);
        StartCoroutine(MoveCredits());
    }
    IEnumerator MoveNewObject()
    {
        float targetY = 3.7f;

        float startingY = title.transform.position.y;

        float elapsedTime = 0f;

        while (elapsedTime < 1.0f)
        {
            float newY = Mathf.Lerp(startingY, targetY, elapsedTime);
            Vector3 newPosition = new Vector3(title.transform.position.x, newY, title.transform.position.z);
            title.transform.position = newPosition;

            elapsedTime += Time.deltaTime * newObjectSpeed;
            yield return null;
        }

        title.transform.position = new Vector3(title.transform.position.x, targetY, title.transform.position.z);
    }
    IEnumerator MoveCredits()
    {
        creditsStartY = credits.transform.position.y;
        float elapsedTime = 0f;

        while (elapsedTime < 5.0f)
        {
            float newY = Mathf.Lerp(creditsStartY, creditsTargetY, elapsedTime / 5.0f);
            Vector3 newPosition = new Vector3(credits.transform.position.x, newY, credits.transform.position.z);
            credits.transform.position = newPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        credits.transform.position = new Vector3(credits.transform.position.x, creditsTargetY, credits.transform.position.z);

        FadeTransition fadeTransition = FindObjectOfType<FadeTransition>();
        fadeTransition.LoadNextLevel(0);
    }
}
