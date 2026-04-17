using UnityEngine;

public class BlockBreaker : MonoBehaviour
{
    private float breakDistance = 5f;
  

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) // Vasen hiiren painallus
        {
            BreakBlock();
        }
    }

    private void BreakBlock()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, breakDistance)) {
            if (hit.collider.CompareTag("Block"))
            {
                Destroy(hit.collider.gameObject);
            }

        }
    }
}
