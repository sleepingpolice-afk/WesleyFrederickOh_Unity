using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Title : MonoBehaviour
{
    public UIDocument UIDocument;
    private Label title;

    LevelManager levelManager;
    void OnEnable()
    {
        var root = UIDocument.rootVisualElement;
        title = root.Q<Label>("Title");
        var name = title.Q<Label>("Name");
    }

    public Label getTitle()
    {
        return title;
    }

}