using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllor : MonoBehaviour
{
    /*playerControler[] players;
    playerControler nearestPlayer;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<playerControler>();
    }

    // Update is called once per frame
    void Update()
    {
        //the float has max value ~3.402
        float minidistance = float.MaxValue;
        //loop through the  array elements
        foreach (playerControler player in players)
        {
            //calculate the distance between the enemy and player for each player
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < minidistance)
            {
                minidistance = distance;
                nearestPlayer = player;
            }
        }
        if (nearestPlayer != null)
        {
            //now we have the nerest player, we need to move the enemny towards the player
            transform.position = Vector3.MoveTowards(transform.position,nearestPlayer.transform.position,speed*Time.deltaTime);
        }
    }*/
}