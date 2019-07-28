namespace Sammo.Sso.Application.ViewModels.Inputs
{
    public class PagedInput
    {
        public PagedInput()
        {
            PageIndex = 1;
            PageSize = 10;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
