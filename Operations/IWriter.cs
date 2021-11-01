namespace Operations
{
    public interface IWriter<T>
    {
        public Result<T> Write();
    }
}
