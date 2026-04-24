using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    public float reachDistance = 5f;
    [SerializeField] GameObject blockPrefab;
    [SerializeField] Transform blockParent;
    [SerializeField] Collider playerCol;

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
            Vector3Int hitBlockPos = Vector3Int.RoundToInt(hit.point - hit.normal * 0.5f);
            Vector3Int placePos = hitBlockPos + Vector3Int.RoundToInt(hit.normal);

            Bounds player = playerCol.bounds;

            Bounds blockBounds = new Bounds(placePos, Vector3.one);

            // Estä placeaminen vain jos blokki on pelaajan tasolla tai ylempänä
            if (placePos.y >= player.min.y)
            {
                if (blockBounds.Intersects(player))
                    return;
            }

            GameObject newBlock = null;
                newBlock = Instantiate(blockPrefab, placePos, Quaternion.identity);
                newBlock.transform.SetParent(blockParent);
            
        }
    }


}
