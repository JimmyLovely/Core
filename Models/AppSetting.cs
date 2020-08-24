using Microsoft.Extensions.Options;

namespace NetCore.Models
{
    public class AppSetting: IOptions<AppSetting>
    {
        public AppSetting Value => this;
        public Logging logging { get; set; }

    }
}