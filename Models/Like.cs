using System;
using System.Collections.Generic;

namespace FirstDotNet.Models;

public partial class Like
{
    public DateOnly DateLiked { get; set; }

    public int IdLike { get; set; }

    public int IdUser { get; set; }

    public int IdPost { get; set; }

    public virtual Post IdPostNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
