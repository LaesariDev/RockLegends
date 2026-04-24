using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    public float reachDistance = 5f;
    [SerializeField] GameObject blockPrefab;
    [SerializeField] Transform blockParent;

    private void LateUpdate()
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
            
            if (Mathf.Round(transform.position.y) && Mathf.Round(transform.position.x) && Mathf.Round(transform.position.z)  != placePos)
            {
                GameObject newBlock = null;
                newBlock = Instantiate(blockPrefab, placePos, Quaternion.identity);
                newBlock.transform.SetParent(blockParent);
            }
        }
    }


}
