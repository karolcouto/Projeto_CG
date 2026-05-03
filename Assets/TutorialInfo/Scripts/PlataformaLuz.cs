using UnityEngine;

public class PlataformaLuz : MonoBehaviour
{
    private MeshRenderer mr;
    private Collider col;

    public PlayerMovement player; // referência ao player

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();

        mr.enabled = false;
        col.enabled = false;
    }

    void Update()
    {
        if (player != null && player.temLanterna)
        {
            mr.enabled = true;
            col.enabled = true;
        }
        else
        {
            mr.enabled = false;
            col.enabled = false;
        }
    }
}
