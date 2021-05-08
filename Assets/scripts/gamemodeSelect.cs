using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemodeSelect : MonoBehaviour
{
    public float moveDistance;
    private int current_position;
    public GameObject sceneManager;
    public string right_button;
    public string right_button2;
    public string left_button;
    public string left_button2;
    public GameObject level_preview;

    // Start is called before the first frame update
    void Start()
    {
        if (!sceneManager) { sceneManager = GameObject.FindGameObjectWithTag("sceneManager"); }
        current_position = 2;
    }

    // Update is called once per frame
    void Update()
    {
        moveSelector();
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.L))
        {
            selectLevel();
        }
    }

    public void moveSelector()
    {
        if (Input.GetKeyDown(right_button) || Input.GetKeyDown(right_button2))
        {
            if (current_position < 3)
            {
                transform.Translate(moveDistance, 0, 0);
                current_position += 1;
            }
        }
        else if (Input.GetKeyDown(left_button) || Input.GetKeyDown(left_button2))
        {
            if (current_position > 1)
            {
                transform.Translate(-moveDistance, 0, 0);
                current_position -= 1;
            }
        }
        level_preview.GetComponent<LevelPreview>().changeSprite(current_position);
    }

    public void selectLevel()
    {
        if (current_position == 1) { sceneManager.GetComponent<SceneChange>().change_CastleArena(); }
        else if (current_position == 2) { sceneManager.GetComponent<SceneChange>().change_DungeonArena(); }
        else if (current_position == 3) { sceneManager.GetComponent<SceneChange>().change_SwampArena(); }
    }
}
