using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using Exiled.CustomItems.API.Spawn;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using Exiled.API.Enums;
using UnityEngine;
using MEC;

namespace _012Teleport
{
    public class TPKeycard : CustomWeapon
    {
        public override void Init()
        {
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;

            base.Init();
        }
        public override void Destroy()
        {
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;

            base.Destroy();
        }

        public override byte ClipSize { get; set; } = 0;
        public override ItemType Type { get; set; } = ItemType.KeycardO5;
        public override uint Id { get; set; } = 12;
        public override string Description { get; set; } = "";
        public override string Name { get; set; } = "012 Keycard";
        public override Modifiers Modifiers { get; set; }
        public override float Damage { get; set; } = 10;
        public override float Weight { get; set; } = 1f;
        public override SpawnProperties SpawnProperties { get; set; }

        public float dmgpercentage = 25;

        public void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if(Check(ev.Pickup))
            {
                Spawn(ev.Pickup.Position);
                Room[] roomObjects = (Room[])UnityEngine.Object.FindObjectsOfType(typeof(Room));
                List<Room> roomsFiltered = new List<Room>();

                foreach (Room room in roomObjects)
                {
                    if 
                    (
                            room.Type != RoomType.LczCurve &&
                            room.Type != RoomType.Lcz012 &&
                            room.Type != RoomType.LczCafe &&
                            room.Type != RoomType.LczToilets &&
                            room.Type != RoomType.LczAirlock &&
                            room.Type != RoomType.Lcz173 &&
                            room.Type != RoomType.LczClassDSpawn &&
                            room.Type != RoomType.LczChkpB &&
                            room.Type != RoomType.LczGlassBox &&
                            room.Type != RoomType.LczChkpA &&
                            room.Type != RoomType.Hcz079 &&
                            room.Type != RoomType.HczEzCheckpoint &&
                            room.Type != RoomType.Hcz939 &&
                            room.Type != RoomType.HczHid &&
                            room.Type != RoomType.Hcz049 &&
                            room.Type != RoomType.HczChkpA &&
                            room.Type != RoomType.Hcz106 &&
                            room.Type != RoomType.HczNuke &&
                            room.Type != RoomType.HczTesla &&
                            room.Type != RoomType.HczChkpB &&
                            room.Type != RoomType.HczCurve &&
                            room.Type != RoomType.HczServers &&
                            room.Type != RoomType.Hcz096 &&
                            room.Type != RoomType.EzVent &&
                            room.Type != RoomType.EzIntercom &&
                            room.Type != RoomType.EzDownstairsPcs &&
                            room.Type != RoomType.EzCurve &&
                            room.Type != RoomType.EzPcs &&
                            room.Type != RoomType.EzCollapsedTunnel &&
                            room.Type != RoomType.EzConference &&
                            room.Type != RoomType.EzCafeteria &&
                            room.Type != RoomType.EzUpstairsPcs &&
                            room.Type != RoomType.EzShelter &&
                            room.Type != RoomType.Pocket &&
                            room.Type != RoomType.Surface &&
                            room.Type != RoomType.EzCrossing
                    )
                    {
                            roomsFiltered.Add(room);
                            //Log.Debug(room.Name);
                    }
                }
                Room randomRoom = roomsFiltered[UnityEngine.Random.Range(0, roomsFiltered.Count - 1)];
                Timing.CallDelayed(0.5f, () =>
                {
                    foreach (Item item in ev.Player.Items)
                    {
                        if (Check(item))
                        {
                            ev.Player.RemoveItem(item);
                        }
                    }
                });
                Timing.CallDelayed(0.6f, () => ev.Player.Position = randomRoom.Position + Vector3.up * 1.5f);
                ev.Player.Hurt(ev.Player.Health * 0.25f);
                ev.Player.Broadcast(10, "<color=#9e0000><size=30><b>YOU HAVE CONSUMED SCP-012!\nYOU HAVE BEEN TELEPORTED TO A RANDOM LOCATION BY SCP-012!</b></size></color>");
            }
        }
    }
}