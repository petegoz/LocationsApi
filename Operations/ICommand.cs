namespace Operations
{
    public interface ICommand<out T>
    {
        public T Run();
    }
}
