using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mediator_Player_Rope : Mediator
{
    public Player player;
    public RopeController rope;

    public override void Notify(object sender, string ev, params object[] objects){
        if(sender is Player sender_player){}
        else if (sender is RopeController sender_rope){ // Rope Event : isThrown, isTight, isActive
            switch(ev){
                case "isThrown":
                    if(objects[0] is bool)
                        player.state.hasMuffler = (bool)(objects[0] as bool?);
                    break;
                case "isTight":
                    if(objects[0] is bool)
                        player.state.isTight = (bool)(objects[0] as bool?);
                    break;
                case "isActive":
                    if(objects[0] is bool)
                        player.state.isMufflerActive = (bool)(objects[0] as bool?);
                    break;
                default:
                    break; 
            }
        }
    }
}