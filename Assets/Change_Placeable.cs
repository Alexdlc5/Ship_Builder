using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_Placeable : MonoBehaviour
{
    private Button button;
    public GameObject Placeable;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(createPlaceable);
    }
    void createPlaceable()
    {
        if (Player_Selection.current_placable != Placeable)
        {
            Player_Selection.current_placable = Placeable;
            GameObject placed_object = Instantiate(Placeable);
            placed_object.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
