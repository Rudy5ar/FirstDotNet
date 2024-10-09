using System;
using System.Collections.Generic;

namespace FirstDotNet.Models;

public partial class Following
{
    public int IdFollowing { get; set; }

    public int IdUser1 { get; set; }

    public int IdUser2 { get; set; }

    public bool? Pending { get; set; }

    public virtual User IdUser1Navigation { get; set; } = null!;

    public virtual User IdUser2Navigation { get; set; } = null!;
}
