namespace Game;

public abstract partial class NPC(NPCInteraction InteractionState) : Entity
{
  public abstract bool PlayerCanInteractWith { get; set; }

  public NPCInteraction Interaction = InteractionState;

  public override void _Ready()
  {
    base._Ready();
    AddToGroup("NPC");
  }
}