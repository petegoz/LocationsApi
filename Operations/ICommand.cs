namespace Operations
{
    public interface ICommand<T>
    {
        public Result<T> Run();
    }
}
