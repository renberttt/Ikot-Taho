    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SoyaScoop : MonoBehaviour
    {
        private bool isDragging;
        private Vector3 offset;
        

        public GameObject replacementObject; // The object to be instantiated when collision occurs

        public GameObject SoyaSoya;
        public GameObject SoyaSoyaSoya;
         public GameObject toAddSoyaonPearl;

         public GameObject toaddSoyaonSyrup;
        public GameObject toAddSoyaonPearlwithSyrup;
        void Start()
        {
            offset = Vector3.zero; // Initialize the offset to zero
        }

        void Update()
        {
            if (isDragging)
            {
                Vector2 newPosition = offset + Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = newPosition;
            }

            if (!isDragging && !CheckCollision()) // Check if not dragging and no collision
            {
                Destroy(gameObject); // Destroy the object
            }
        }

        public void OnMouseDown()
        {
            isDragging = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition); // Update the offset when clicked
        }

        public void OnMouseUp()
        {
            isDragging = false;
        }

        private bool CheckCollision()
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
            foreach (Collider2D collider in colliders)
         {
                if (collider != GetComponent<Collider2D>() && collider.CompareTag("Target"))
                {
                    // Instantiate the replacement object at the collision position and rotation
                    Instantiate(replacementObject, collider.transform.position, collider.transform.rotation);

                    // Destroy both the dragged object and the cup object
                    Destroy(gameObject);
                    Destroy(collider.gameObject);
                   

                    return true; // Collision detected with a Cup object
                }
                else if (collider.CompareTag("Pearls"))
            {
                // Instantiate the replacement object for the "AnotherTag" at the collision position and rotation
                // Replace "AnotherReplacementObject" with the appropriate replacement object prefab for the "AnotherTag"
                Instantiate(toAddSoyaonPearl, collider.transform.position, collider.transform.rotation);

                // Destroy both the dragged object and the object with "AnotherTag"
                Destroy(gameObject);
                Destroy(collider.gameObject);

                return true; // Collision detected with an object with "AnotherTag"
            }

                else if (collider.CompareTag("Soya"))
            {
                // Instantiate the replacement object for the "AnotherTag" at the collision position and rotation
                // Replace "AnotherReplacementObject" with the appropriate replacement object prefab for the "AnotherTag"
                Instantiate(SoyaSoya, collider.transform.position, collider.transform.rotation);

                // Destroy both the dragged object and the object with "AnotherTag"
                Destroy(gameObject);
                Destroy(collider.gameObject);

                return true; // Collision detected with an object with "AnotherTag"
            }
            

            else if (collider.CompareTag("SoyaAndSoya"))
            {
                // Instantiate the replacement object for the "AnotherTag" at the collision position and rotation
                // Replace "AnotherReplacementObject" with the appropriate replacement object prefab for the "AnotherTag"
                Instantiate(SoyaSoyaSoya, collider.transform.position, collider.transform.rotation);

                // Destroy both the dragged object and the object with "AnotherTag"
                Destroy(gameObject);
                Destroy(collider.gameObject);

                return true; // Collision detected with an object with "AnotherTag"
            }


        

                else if (collider.CompareTag("Syrup"))
            {
                // Instantiate the replacement object for the "AnotherTag" at the collision position and rotation
                // Replace "AnotherReplacementObject" with the appropriate replacement object prefab for the "AnotherTag"
                Instantiate(toaddSoyaonSyrup, collider.transform.position, collider.transform.rotation);

                // Destroy both the dragged object and the object with "AnotherTag"
                Destroy(gameObject);
                Destroy(collider.gameObject);

                return true; // Collision detected with an object with "AnotherTag"
            }
              else if (collider.CompareTag("PearlAndSyrup"))
            {
                // Instantiate the replacement object for the "AnotherTag" at the collision position and rotation
                // Replace "AnotherReplacementObject" with the appropriate replacement object prefab for the "AnotherTag"
                Instantiate(toAddSoyaonPearlwithSyrup, collider.transform.position, collider.transform.rotation);

                // Destroy both the dragged object and the object with "AnotherTag"
                Destroy(gameObject);
                Destroy(collider.gameObject);

                return true; // Collision detected with an object with "AnotherTag"
            }
             
         }
            return false; // No collision detected or collided with objects other than Cup
        }
    }
