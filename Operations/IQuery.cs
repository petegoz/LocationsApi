namespace Operations
{
    public interface IQuery<T>
    {
        public Result<T> Run();
    }
}
