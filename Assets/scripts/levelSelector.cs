using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSelector : MonoBehaviour
{
    public float moveDistance;
    private int current_position;
    public GameObject sceneManager;
    public string right_button;
    public string right_button2;
    public string left_button;
    public string left_button2;
    public GameObject level_preview;
    public bool dojoMode;

    // Start is called before the first frame update
    void Start()
    {
        if (!sceneManager) { sceneManager = GameObject.FindGameObjectWithTag("sceneManager"); }
        current_position = 3;
    }

    // Update is called once per frame
    void Update()
    {
        moveSelector();
        if (dojoMode)
        {
            if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.L))
            {
                selectDojoLevel();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.L))
            {
                selectBattleLevel();
            }
        }
    }

    public void moveSelector()
    {
        if (Input.GetKeyDown(right_button) || Input.GetKeyDown(right_button2))
        {
            if(current_position < 5)
            {
                transform.Translate(moveDistance, 0, 0);
                current_position += 1;
            }
        }
        else if (Input.GetKeyDown(left_button) || Input.GetKeyDown(left_button2))
        {
            if(current_position > 1)
            {
                transform.Translate(-moveDistance, 0, 0);
                current_position -= 1;
            }
        }
        level_preview.GetComponent<LevelPreview>().changeSprite(current_position);
    }

    public void selectBattleLevel()
    {
        if (current_position == 1) { sceneManager.GetComponent<SceneChange>().ChangeScene("CastleBattleArena"); }
        else if (current_position == 2) { sceneManager.GetComponent<SceneChange>().ChangeScene("ForestBattleArena"); }
        else if (current_position == 3) { sceneManager.GetComponent<SceneChange>().ChangeScene("DungeonBattleArena"); }
        else if (current_position == 4) { sceneManager.GetComponent<SceneChange>().ChangeScene("DojoBattleArena"); }
        else if (current_position == 5) { sceneManager.GetComponent<SceneChange>().ChangeScene("SwampBattleArena"); }
    }
    
    public void selectDojoLevel()
    {
        if (current_position == 1) { sceneManager.GetComponent<SceneChange>().ChangeScene("CastleTrainingArena"); }
        else if (current_position == 2) { sceneManager.GetComponent<SceneChange>().ChangeScene("ForestTrainingArena"); }
        else if (current_position == 3) { sceneManager.GetComponent<SceneChange>().ChangeScene("DungeonTrainingArena"); }
        else if (current_position == 4) { sceneManager.GetComponent<SceneChange>().ChangeScene("DojoTrainingArena"); }
        else if (current_position == 5) { sceneManager.GetComponent<SceneChange>().ChangeScene("SwampTrainingArena"); }
    }
}
