using System.Collections.Generic;
using System.Linq;
using System.Text;
using OptionalUI;
using Partiality.Modloader;
using UnityEngine;

namespace ThrowMovement
{
    public class ThrowMovement : PartialityMod
    {
        public static OptionInterface LoadOI()
        {
            return new Config();
        }

        public ThrowMovement()
        {
            instance = this;
            this.ModID = "ThrowMovement";
            this.Version = "1";
            this.author = "laura#2871";
        }
        public static ThrowMovement instance;

        public override void OnEnable()
        {
            base.OnEnable();
            On.Player.ReleaseObject += Player_ReleaseObject;
            On.Player.ThrowObject += Player_ThrowObject;
            On.Player.GrabUpdate += Player_GrabUpdate;
            On.Rock.Update += Rock_Update;
            On.Rock.PickedUp += Rock_PickedUp;
        }

        private void Player_ReleaseObject(On.Player.orig_ReleaseObject orig, Player self, int grasp, bool eu)
        {
            orig(self, grasp, eu);
            rockReplenishInterval = reqFramesSinceThrow;
        }

        private void Player_ThrowObject(On.Player.orig_ThrowObject orig, Player self, int grasp, bool eu)
        {
            orig(self, grasp, eu);
            rockReplenishInterval = reqFramesSinceThrow;
        }

        private void Player_GrabUpdate(On.Player.orig_GrabUpdate orig, Player self, bool eu)
        {
            orig(self, eu);
            if (self.objectInStomach?.type == AbstractPhysicalObject.AbstractObjectType.Rock)
            {
                if (self.lowerBodyFramesOnGround > reqFramesOnGround && self.FreeHand() > -1 && rockReplenishInterval <= 0)
                {
                    AbstractPhysicalObject newRock = new AbstractPhysicalObject(self.room.world, AbstractPhysicalObject.AbstractObjectType.Rock, null,
                                                                                self.room.GetWorldCoordinate(self.firstChunk.pos), self.room.game.GetNewID());
                    newRock.RealizeInRoom();
                    self.SlugcatGrab(newRock.realizedObject, self.FreeHand());
                    rockLifespans.Add(newRock.realizedObject as Rock, defRockLifespan);
                    rockReplenishInterval = reqFramesSinceThrow;
                }
                rockReplenishInterval -= 1;
            }
        }

        private void Rock_Update(On.Rock.orig_Update orig, Rock self, bool eu)
        {
            orig(self, eu);
            if (rockLifespans.ContainsKey(self) && !(self.mode is Weapon.Mode.Carried))
            {
                if (rockLifespans[self] <= 0) self.Destroy();
                rockLifespans[self] -= 1;
            }
        }

        private void Rock_PickedUp(On.Rock.orig_PickedUp orig, Rock self, Creature upPicker)
        {
            if (rockLifespans.ContainsKey(self) && !(self.mode is Weapon.Mode.Carried))
            {
                rockLifespans[self] = defRockLifespan;
            }
        }

        public static int reqFramesOnGround;

        public static int reqFramesSinceThrow;
        public static int rockReplenishInterval = 0;

        public static int defRockLifespan;
        public Dictionary<Rock, int> rockLifespans = new Dictionary<Rock, int>();
    }
}