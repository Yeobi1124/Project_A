using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mediator
{
    public class Mediator_Player_Rope : AbstractMediator
    {
        public Player player;
        public Muffler rope;

        public override void Notify(object sender, string ev, params object[] objects)
        {
            if (sender is Player senderPlayer)
            {
            }
            else if (sender is Muffler senderRope)
            {
                // Rope Event : isPlayerOwned, isTight, isActive
                switch (ev)
                {
                    case "isPlayerOwned":
                        if (objects[0] != null && objects[0] is bool isPlayerOwned)
                            player.state.hasMuffler = isPlayerOwned;
                        break;
                    case "isTight":
                        if (objects[0] != null && objects[0] is bool isTight)
                            player.state.isTight = isTight;
                        break;
                    case "isActive":
                        if (objects[0] != null && objects[0] is bool isActive)
                            player.state.isMufflerActive = isActive;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}