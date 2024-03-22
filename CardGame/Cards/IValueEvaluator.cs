namespace SamMRoberts.CardGame.Cards
{
    public interface IValueEvaluator<T>
    {
        int GetValue(T obj);
    }
}