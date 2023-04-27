using CI_Entities.Models;

namespace CI_Project.Models
{
    public class CmsPageViewModel
    {
        public List<CmsPage> Pages { get; set; }
        public string title { get; set; }

        public string? description { get; set; }

        public string? slug { get; set; }
        public int status { get; set; }
    }
}
