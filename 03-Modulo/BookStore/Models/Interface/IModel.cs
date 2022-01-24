namespace BookStore.Models.Interface
{
    public interface IModel
    {
        string Error { get; }
        bool EstaValida();
    }
}