using Godot;

namespace UI;

public partial class EditPlayerMenu : Control
{
	[Export]
	public Button BackButton { get; set; }

	[Export]
	public Button ConfirmButton { get; set; }
}
