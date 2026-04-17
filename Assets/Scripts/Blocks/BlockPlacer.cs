using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    public float reachDistance = 5f;
    [SerializeField] GameObject blockPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PlaceBlock();
        }
        
    }


    private void PlaceBlock()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            Vector3 placePos = hit.point + hit.normal * 0.5f;

            placePos = new Vector3(
                Mathf.Round(placePos.x),
                Mathf.Round(placePos.y),
                Mathf.Round(placePos.z)
            );
            Instantiate(blockPrefab, placePos, Quaternion.identity);
        }
    }


}
