using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public Texture newTexture; // La nouvelle texture � appliquer

    public void ChangeTexture()
    {
        Renderer renderer = GetComponent<Renderer>(); // Obtenez le Renderer de l'objet
        if (renderer != null)
        {
            Material mat = renderer.material; // Obtenez le mat�riau de l'objet
            mat.SetTexture("_MainTex", newTexture); // Changez la texture d'Albedo
        }
        else
        {
            Debug.LogError("Renderer non trouv� sur l'objet.");
        }
    }

    public void Awake()
    {
        ChangeTexture();
    }
}
