using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string p1_tag;
    public string p2_tag;
    public bool change_on_button;
    public string scene_name;
    public string change_scene_button;

    private void Update()
    {
        if (change_on_button)
        {
            handleSceneOnButton();
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void handleSceneOnButton()
    {
        if (Input.GetKey(change_scene_button))
        {
            ChangeScene(scene_name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(p1_tag) || collision.gameObject.CompareTag(p2_tag))
        {
            ChangeScene(scene_name);
        }
    }
}

