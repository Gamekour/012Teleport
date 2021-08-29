using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs;
using Exiled.API.Features.Components;
using MEC;
using Mirror.RemoteCalls;
using Mirror;

namespace _012Teleport
{
    public class Class1 : Plugin<Config>
    {
        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            base.OnDisabled();
        }
        void OnRoundStarted()
        {
            Room[] rooms = (Room[])UnityEngine.Object.FindObjectsOfType(typeof(Room));
            Room room012 = rooms[0];
            foreach(Room room in rooms)
            {
                if(room.Type == Exiled.API.Enums.RoomType.Lcz012)
                {
                    room012 = room;
                }
            }
            TPKeycard test = new TPKeycard();
            test.TryRegister();
            Timing.CallDelayed(5, () =>
            {
                test.Spawn(room012.Transform.TransformPoint(4.88f, -7, 3.68f));
            });
        }
    }
}
