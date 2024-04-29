namespace Game;

public record struct EntityAttributes()
{
  public EntityAttribute Agility = new Attributes.Agility();
  public EntityAttribute Dexterity = new Attributes.Dexterity();
  public EntityAttribute Intelligence = new Attributes.Intelligence();
  public EntityAttribute Luck = new Attributes.Luck();
  public EntityAttribute Strength = new Attributes.Strength();
  public EntityAttribute Vitality = new Attributes.Vitality();
}