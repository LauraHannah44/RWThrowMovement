using UnityEngine;
using Menu.Remix.MixedUI;

namespace ThrowMovement;

using static ThrowMovement;
partial class Config : OptionInterface
{
	public override void Initialize()
	{
		base.Initialize();
        Tabs = new OpTab[] { new OpTab(this) };

        OpLabel labelAuthor = new(20f, 570f, "ThrowMovement mod by pinecubes", true);
		OpLabel labelPing = new(20f, 500f, "laura#2871 for any questions or suggestions!", false);
		OpLabel labelNote = new(200f, 530f, "Gain free rocks on the ground that despawn once thrown!\n(When a rock is in slugcat's stomach)", false);

		int top = 200;

		OpLabel labelGround = new(20f, 600 - top, "Grounded time until rocks", false);
		OpLabel labelGroundUnit = new(356f, 600 - top + 2, " frames", false);
		OpTextBox typerGround = new(reqFramesOnGround, new Vector2(300f, 600 - top), 50)
		{
			description = "How many frames should slugcat be on the ground before they get rocks? (40fps)",
			colorEdge = Color.clear,
			colorText = new Color(122f, 216f, 255f)
		};
		OpLabel labelThrow = new(20f, 600 - top - 30, "Throw time until rocks", false);
		OpLabel labelThrowUnit = new(356f, 600 - top - 30 + 2, " frames", false);
		OpTextBox typerThrow = new(reqFramesSinceThrow, new Vector2(300f, 600 - top - 30), 50)
		{
			description = "At what frame interval after throwing should slugcat get rocks? (40fps)",
			colorEdge = Color.clear,
			colorText = new Color(122f, 216f, 255f)
		};
		OpLabel labelDespawn = new(20f, 600 - top - 60, "Time until despawn", false);
		OpLabel labelDespawnUnit = new(356f, 600 - top - 60 + 2, " frames", false);
		OpTextBox typerDespawn = new(defRockLifespan, new Vector2(300f, 600 - top - 60), 50)
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
	public Config()
	{
		reqFramesOnGround = config.Bind("grounded_frames", 0);
        reqFramesSinceThrow = config.Bind("rate_frames", 20);
        defRockLifespan = config.Bind("despawn_frames", 80);

    }
}