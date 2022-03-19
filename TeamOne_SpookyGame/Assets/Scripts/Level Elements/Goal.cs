using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] string sceneName;
    AudioManager AM;

    private void Awake()
    {
        AM = FindObjectOfType<AudioManager>();
    }

    //If the trigger collides with the player, load the next level
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            NextLevel();
        }
    }

    //Loads the next level
    void NextLevel()
    {
        AM.PlaySFX("Win");

        //Switches to the next level in the list
        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().IncrementLevel();
        //Loads the level
        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadNextLevel();
    }
}
