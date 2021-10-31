namespace Operations
{
    public interface IQuery<out T>
    {
        public T Run();
    }
}
