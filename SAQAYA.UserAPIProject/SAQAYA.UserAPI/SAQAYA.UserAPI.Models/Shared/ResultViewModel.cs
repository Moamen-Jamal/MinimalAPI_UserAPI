namespace ViewModels
{
    public class ResultViewModel<T>
    {
        public bool Successed { get; set; } = false;
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
