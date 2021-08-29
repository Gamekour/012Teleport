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
                Log.Debug(roomObjects.Length);

                foreach (Room room in roomObjects)
                {
                    if (room.Zone != ZoneType.Surface && room.Zone != ZoneType.Unspecified)
                    {
                        if (
                            room.Type != RoomType.Pocket &&
                            room.Type != RoomType.HczHid &&
                            room.Type != RoomType.Hcz939 &&
                            room.Type != RoomType.Hcz106 &&
                            room.Type != RoomType.LczCurve &&
                            room.Type != RoomType.HczCurve &&
                            room.Type != RoomType.EzCollapsedTunnel &&
                            room.Type != RoomType.EzVent
                            )
                        {
                            roomsFiltered.Add(room);
                            //Log.Debug(room.Name);
                        }
                    }
                }
                Room randomRoom = roomsFiltered[UnityEngine.Random.Range(0, roomsFiltered.Count - 1)];
                Timing.CallDelayed(0.5f, () =>
                {
                    ev.Player.Position += Vector3.up * -10f;
                    foreach (Item item in ev.Player.Items)
                    {
                        if (Check(item))
                        {
                            ev.Player.DropItem(item);
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