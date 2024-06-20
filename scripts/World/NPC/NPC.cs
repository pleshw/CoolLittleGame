namespace Game;

public abstract partial class NPC(SerializableInteraction InteractionState) : Entity
{
  public abstract bool PlayerCanInteractWith { get; set; }

  public SerializableInteraction Interaction = InteractionState;

  public override void _Ready()
  {
    base._Ready();
    AddToGroup("NPC");
  }
}