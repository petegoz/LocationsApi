namespace Operations
{
    public interface IReader<T>
    {
        public Result<T> Read();
    }
}
