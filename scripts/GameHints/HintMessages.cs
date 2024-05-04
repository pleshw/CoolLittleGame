using Generic;

namespace GameHint;

public static class HintMessages
{
  public static class LoadingScreen
  {
    public static readonly RandomList<Hint> Hints = [
      new Hint {Message="Mate monstros para adquirir experiência e aprimorar suas habilidades.", TimeInMs = 3000},
      new Hint {Message="Alguns lugares possuem uma aura liminar. Dizem que esses lugares podem te levar para outras dimensões.", TimeInMs = 5000},
      new Hint {Message="Você pode fazer amizade com outras criaturas. Tente entregar algo que elas gostem.", TimeInMs = 4000},
    ];
  }
}