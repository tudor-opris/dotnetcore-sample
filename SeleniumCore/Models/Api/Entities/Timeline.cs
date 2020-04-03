using SeleniumCore.Helpers;

namespace SeleniumCore.Models.Api.Entities
{
    public class Timeline
    {
        public string availableFrom { get; set; } = Constants.CURRENT_DATE;
        public string deadline { get; set; } 

    }
}