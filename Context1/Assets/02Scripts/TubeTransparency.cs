using UnityEngine;

public class TubeTransparency : MonoBehaviour
{
    [SerializeField] private TubeColor color;
    private enum TubeColor
    {
        Black, Blue, Cyan, Green, Grey, Magenta, Orange, Purple, Red, White, Yellow
    }

    private Material backMaterial;
    private Material frontMaterial;

    [SerializeField] private MeshRenderer meshRenderer;

    private void Awake()
    {
        switch (color)
        {
            case TubeColor.Black:
                backMaterial = Resources.Load("Tubes/Back/TubeBack_Black", typeof(Material)) as Material;
                frontMaterial = Resources.Load("Tubes/Front/TubeFront_Black", typeof(Material)) as Material;
                break;
            case TubeColor.Blue:
                backMaterial = Resources.Load("Tubes/Back/TubeBack_Blue", typeof(Material)) as Material;
                frontMaterial = Resources.Load("Tubes/Front/TubeFront_Blue", typeof(Material)) as Material;
                break;
            case TubeColor.Cyan:
                backMaterial = Resources.Load("Tubes/Back/TubeBack_Cyan", typeof(Material)) as Material;
                frontMaterial = Resources.Load("Tubes/Front/TubeFront_Cyan", typeof(Material)) as Material;
                break;
            case TubeColor.Green:
                backMaterial = Resources.Load("Tubes/Back/TubeBack_Green", typeof(Material)) as Material;
                frontMaterial = Resources.Load("Tubes/Front/TubeFront_Green", typeof(Material)) as Material;
                break;
            case TubeColor.Grey:
                backMaterial = Resources.Load("Tubes/Back/TubeBack_Grey", typeof(Material)) as Material;
                frontMaterial = Resources.Load("Tubes/Front/TubeFront_Grey", typeof(Material)) as Material;
                break;
            case TubeColor.Magenta:
                backMaterial = Resources.Load("Tubes/Back/TubeBack_Magenta", typeof(Material)) as Material;
                frontMaterial = Resources.Load("Tubes/Front/TubeFront_Magenta", typeof(Material)) as Material;
                break;
            case TubeColor.Orange:
                backMaterial = Resources.Load("Tubes/Back/TubeBack_Orange", typeof(Material)) as Material;
                frontMaterial = Resources.Load("Tubes/Front/TubeFront_Orange", typeof(Material)) as Material;
                break;
            case TubeColor.Purple:
                backMaterial = Resources.Load("Tubes/Back/TubeBack_Purple", typeof(Material)) as Material;
                frontMaterial = Resources.Load("Tubes/Front/TubeFront_Purple", typeof(Material)) as Material;
                break;
            case TubeColor.Red:
                backMaterial = Resources.Load("Tubes/Back/TubeBack_Red", typeof(Material)) as Material;
                frontMaterial = Resources.Load("Tubes/Front/TubeFront_Red", typeof(Material)) as Material;
                break;
            case TubeColor.White:
                backMaterial = Resources.Load("Tubes/Back/TubeBack_White", typeof(Material)) as Material;
                frontMaterial = Resources.Load("Tubes/Front/TubeFront_White", typeof(Material)) as Material;
                break;
            case TubeColor.Yellow:
                backMaterial = Resources.Load("Tubes/Back/TubeBack_Yellow", typeof(Material)) as Material;
                frontMaterial = Resources.Load("Tubes/Front/TubeFront_Yellow", typeof(Material)) as Material;
                break;
        }

        Material[] materials = new Material[2];
        materials[0] = backMaterial;
        materials[1] = backMaterial;
        meshRenderer.materials = materials;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player entered");
            Material[] materials = new Material[2];
            materials[0] = backMaterial;
            materials[1] = frontMaterial; // dit is de material die transparant moet worden
            meshRenderer.materials = materials;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Material[] materials = new Material[2];
            materials[0] = backMaterial;
            materials[1] = backMaterial;
            meshRenderer.materials = materials;
        }
    }
}
