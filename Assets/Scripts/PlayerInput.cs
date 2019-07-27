using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

	Player player;
    public PlayerDash playerDash;

	void Start () {
		player = GetComponent<Player> ();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            if (player.currentSpeed < player.maximumSpeed)
            {
                player.currentSpeed += Time.deltaTime * player.acceleration;
            }
            else
            {
                player.currentSpeed = player.maximumSpeed;
            }
        }
        else
        {
            player.currentSpeed = player.minimumSpeed;
        }
        if (Input.GetButtonDown("Fire2") && Input.GetAxisRaw("Horizontal") > 0)
        {
            playerDash.Dash(1);
        }

        if (Input.GetButtonDown("Fire2") && Input.GetAxisRaw("Horizontal") < 0)
        {
            playerDash.Dash(-1);
        }

        if (Input.GetButtonDown("Jump")) {
			player.OnJumpInputDown ();
		}
		if (Input.GetButtonUp("Jump")) {
			player.OnJumpInputUp ();
		}
	}
}