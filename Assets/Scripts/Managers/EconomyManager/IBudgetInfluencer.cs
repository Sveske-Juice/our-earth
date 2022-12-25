/**
<summary>
Every class that implements this interface will be an Influencer
to the player's budget. It will either Influence in a positive
(gives a profit) or negative way (costs more money sustain the class)
</summary>
**/
public interface IBudgetInfluencer
{
    /// <summary>
    /// Interface method for how much the implementing class
    /// Influence to the player's budget. Returns an amount
    /// which will be summed from all other classes implementing
    /// this interface and give the player's total yearly budget/income.
    /// </summary>
    double GetYearlyInfluence();
}