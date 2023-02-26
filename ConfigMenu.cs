using System;
using OptionalUI;
using UnityEngine;

namespace ThrowMovement
{
	public class Config : OptionInterface
	{
		public Config() : base(ThrowMovement.instance)
		{
		}

		public override bool Configuable()
		{
			return true;
		}

		public override void ConfigOnChange()
		{
			ThrowMovement.reqFramesOnGround = int.Parse(OptionInterface.config["D1"]);
			ThrowMovement.reqFramesSinceThrow = int.Parse(OptionInterface.config["D2"]);
			ThrowMovement.defRockLifespan = int.Parse(OptionInterface.config["D3"]);
		}

		public override void Initialize()
		{
			base.Initialize();
			this.Tabs = new OpTab[]
			{
				new OpTab("Config")
			};
			OpLabel labelAuthor = new OpLabel(20f, 570f, "ThrowMovement mod by pinecubes", true);
			OpLabel labelPing = new OpLabel(20f, 500f, "laura#2871 for any questions or suggestions!", false);
			OpLabel labelNote = new OpLabel(200f, 530f, "Gain free rocks on the ground that despawn once thrown!\n(When a rock is in slugcat's stomach)", false);

			int top = 200;

			OpLabel labelGround = new OpLabel(20f, (600 - top), "Grounded time until rocks", false);
			OpLabel labelGroundUnit = new OpLabel(356f, (600 - top + 2), " frames", false);
			OpTextBox typerGround = new OpTextBox(new Vector2(300f, (600 - top)), 50, "D1", "0", OpTextBox.Accept.Int)
			{
				description = "How many frames should slugcat be on the ground before they get rocks? (40fps)",
				colorEdge = Color.clear,
				colorText = new Color(122f, 216f, 255f)
			};
			OpLabel labelThrow = new OpLabel(20f, (600 - top - 30), "Throw time until rocks", false);
			OpLabel labelThrowUnit = new OpLabel(356f, (600 - top - 30 + 2), " frames", false);
			OpTextBox typerThrow = new OpTextBox(new Vector2(300f, (600 - top - 30)), 50, "D2", "20", OpTextBox.Accept.Int)
			{
				description = "At what frame interval after throwing should slugcat get rocks? (40fps)",
				colorEdge = Color.clear,
				colorText = new Color(122f, 216f, 255f)
			};
			OpLabel labelDespawn = new OpLabel(20f, (600 - top - 60), "Time until despawn", false);
			OpLabel labelDespawnUnit = new OpLabel(356f, (600 - top - 60 + 2), " frames", false);
			OpTextBox typerDespawn = new OpTextBox(new Vector2(300f, (600 - top - 60)), 50, "D3", "80", OpTextBox.Accept.Int)
			{
				description = "How many frames should rocks be thrown before they despawn? (40fps)",
				colorEdge = Color.clear,
				colorText = new Color(122f, 216f, 255f)
			};
			this.Tabs[0].AddItems(new UIelement[]
			{
				labelAuthor,
				labelPing,
				labelNote,
				labelGround,
				labelGroundUnit,
				typerGround,
				labelThrow,
				labelThrowUnit,
				typerThrow,
				labelDespawn,
				labelDespawnUnit,
				typerDespawn
			});
		}
	}
}
