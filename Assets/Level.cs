using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] List<SpawnEnemies> spawners;
    [SerializeField] float progressRequired;
    [SerializeField] AudioSource music;
    [SerializeField] GameObject levelExit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player.instance.progress = 0;
        Player.instance.maxProgress = progressRequired;
        Player.instance.won = false;
        Player.instance.PlayMusic(music);
        Player.instance.transform.position = new Vector3(3.65f, -7.45f);
        GameObject levelExitObj = Player.instance.FindObjectByName("LevelExitDoor");
        Player.instance.levelText.text = gameObject.scene.name;
        Player.instance.health = Player.instance.maxHealth;
        levelExitObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
