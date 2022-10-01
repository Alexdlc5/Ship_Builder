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
        if (Submarine_Core.submode)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            button.interactable = false;
        }
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
        Placeable_Object placeable_object_in_scene = FindObjectOfType<Placeable_Object>();
        if (placeable_object_in_scene == null)
        {
            Player_Selection.current_placable = Placeable;
            GameObject placed_object = Instantiate(Placeable);
            placed_object.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
