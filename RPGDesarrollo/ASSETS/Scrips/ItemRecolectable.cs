using UnityEngine;

public class ItemRecolectable : MonoBehaviour
{
    [Header("Identificador del objeto")]
    public string idItem;

    void Start()
    {
        // Si no se asigna manualmente, usa el tag del objeto
        if (string.IsNullOrEmpty(idItem))
            idItem = gameObject.tag;
    }
}
