using UnityEngine;

// Clase que contiene funciones utilies accesibles a todas
// las clases del juego.
public static class Utilities
{

    // Funcion que le cambia el color de emision al objeto con el transform
    // especificado en los argumentos
    public static void ChangeHighlight(Transform parent, Color color)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            // Se obvian el canvas, sus hijos
            if (!parent.GetChild(i).CompareTag("Canvas"))
            {
                Renderer rend = parent.GetChild(i).GetComponent<Renderer>();
                if (rend != null)
                {
                    rend.material.SetColor("_EmissionColor", color);
                }
                if (parent.GetChild(i).childCount != 0)
                {
                    ChangeHighlight(parent.GetChild(i), color);
                }
            }
        }
    }
}
