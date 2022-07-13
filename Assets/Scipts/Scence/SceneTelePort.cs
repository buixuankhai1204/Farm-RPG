using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class SceneTelePort : MonoBehaviour
{
    [SerializeField] private SceneName sceneName;
    [SerializeField] private Vector3 scenePositionGoto = new Vector3();
    private SceneControllerManager sceneControllerManager;

    private void Start()
    {
        sceneControllerManager =
            GameObject.FindWithTag(Tags.SceneControllerManager).GetComponent<SceneControllerManager>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        float xPosition = Mathf.Approximately(scenePositionGoto.x, 0f)
            ? player.transform.position.x
            : scenePositionGoto.x;
        
        float yPosition = Mathf.Approximately(scenePositionGoto.y, 0f)
            ? player.transform.position.y
            : scenePositionGoto.y;

        float zPosition = 0f;
        
        Debug.LogError(sceneName.ToString());
        sceneControllerManager.FadeAndLoadScene(sceneName.ToString(), new Vector3(xPosition, yPosition, zPosition));
    }
}
