namespace Operations
{
    public interface IWriter<out T>
    {
        public T Write();
    }
}
