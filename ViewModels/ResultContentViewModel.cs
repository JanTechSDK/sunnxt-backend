namespace SunNxtBackend.ViewModels
{
    public class ResultContentViewModel
    {
        public bool Status { get; set; }
        public object ResultContent { get; set; }
        public ResultContentViewModel(bool status, object resultContent)
        {
            this.Status = status;
            this.ResultContent = resultContent;
        }
    }
}
