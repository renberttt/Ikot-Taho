using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public CustomerMovement customerMovement;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Customer"))
        {
            // Get the CustomerMovement component
            CustomerMovement customerMovement = other.GetComponent<CustomerMovement>();

            // Check if the CustomerMovement component exists and is not moving or returning
            //if (customerMovement != null && !customerMovement.isMoving && !customerMovement.isReturning)
            //{
            //    // Get the ScoreText object with the "Score" tag
            //    GameObject scoreTextObject = GameObject.FindGameObjectWithTag("Score");

            //    // Check if the ScoreText object exists
            //    if (scoreTextObject != null)
            //    {
            //        // Get the ScoreText component
            //        ScoreText scoreText = scoreTextObject.GetComponent<ScoreText>();

            //        // Check if the ScoreText component exists
            //        if (scoreText != null)
            //        {
            //            // Add 15 to the score
            //            scoreText.AddScore(15);
            //        }
            //    }

            //    // Destroy the FinalTaho object
            //    Destroy(gameObject);
            //}
        }
    }
}
