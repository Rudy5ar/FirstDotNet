using System;
using System.Collections.Generic;

namespace FirstDotNet.Models;

public partial class Commentreply
{
    public int IdCommentReply { get; set; }

    public string Text { get; set; } = null!;

    public int NumOfLikes { get; set; }

    public int IdComment { get; set; }

    public int IdUser { get; set; }

    public virtual Comment IdCommentNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<Likedreply> Likedreplies { get; set; } = new List<Likedreply>();
}
