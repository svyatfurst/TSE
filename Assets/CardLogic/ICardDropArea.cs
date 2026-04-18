using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardDropArea
{
    // Return a boolean indicating whether the card was successfully dropped in this area
    bool OnCardDropped(Card card);
}
