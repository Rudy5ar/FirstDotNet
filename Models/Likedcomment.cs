using System;
using System.Collections.Generic;

namespace FirstDotNet.Models;

public partial class Likedcomment
{
    public int IdLikedComment { get; set; }

    public int IdUser { get; set; }

    public int IdComment { get; set; }

    public virtual Comment IdCommentNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
