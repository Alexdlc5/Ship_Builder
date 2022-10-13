using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Change_Placeable : MonoBehaviour
{
    private Button button;
    public GameObject Placeable;
    private TextMeshProUGUI text;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(createPlaceable);
        image = gameObject.GetComponent<Image>();
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        //if in submode turn invisible and uninteractable
        if (Submarine_Core.submode)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            button.interactable = false;
        }
        //if in build mode show button and make interactable
        else
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            button.interactable = true;
        }
    }
    void createPlaceable()
    {
        Player_Selection.destruction_mode = false;
        Placeable_Object[] placeable_object_in_scene = FindObjectsOfType<Placeable_Object>();
        for (int i = 0; i < placeable_object_in_scene.Length; i++)
        {
            Destroy(placeable_object_in_scene[i].gameObject);
        }
        //set player selection
        Player_Selection.current_placable = Placeable;
        //create first placeable and set position to mouse
        GameObject placed_object = Instantiate(Placeable);
        placed_object.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
