using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Identity;
public class CustomIdentityOptions : IdentityOptions, IOptions<CustomIdentityOptions>
{
    public bool RequiredActiveAccount { get; set; }

    public CustomIdentityOptions Value => this;
}
