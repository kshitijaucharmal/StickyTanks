using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int dimensions;
    [SerializeField] private Vector2Int offset;
    [SerializeField] private Vector2 gap = new Vector2(0.1f, 0.1f);
    [SerializeField] private GameObject gridBlock;

    // Start is called before the first frame update
    void Awake() {
        GenerateGrid();
        CreateDieBlock();
    }

    void GenerateGrid(){
        for(int i = 0; i < dimensions.x; i++){
            for(int j = 0; j < dimensions.y; j++){
                var pos = new Vector3(i + (i * gap.x) + offset.x, transform.position.y, j + (j * gap.y) + offset.y);
                Instantiate(gridBlock, pos, Quaternion.identity, transform);
            }
        }
    }

    void CreateDieBlock(){
        var dieBlock = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        dieBlock.GetComponent<MeshRenderer>().enabled = false;
        var pos = transform.position;
        pos.y -= 10;
        dieBlock.position = pos;
        dieBlock.localScale = new Vector3Int(dimensions.x + 5, 1, dimensions.y + 5);
        dieBlock.tag = "DieBlock";
        var col = dieBlock.GetComponent<BoxCollider>();
        col.isTrigger = true;
        dieBlock.AddComponent<DestroyOnDieBlock>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
