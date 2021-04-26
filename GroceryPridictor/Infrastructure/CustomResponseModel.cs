namespace GroceryPridictor.Infrastructure
{
    public class CustomResponseModel
    {
        public bool Status { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
    public class CustomResponseModel<T>
    {
        public bool Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
