using System;
using System.Collections.Generic;

namespace FirstDotNet.Models;

public partial class Likedreply
{
    public int IdLikedReply { get; set; }

    public int IdUser { get; set; }

    public int IdCommentReply { get; set; }

    public virtual Commentreply IdCommentReplyNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
