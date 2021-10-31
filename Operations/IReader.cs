namespace Operations
{
    public interface IReader<out T>
    {
        public T Read();
    }
}
