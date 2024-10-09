using System;
using System.Collections.Generic;

namespace FirstDotNet.Models;

public partial class Comment
{
    public int IdComment { get; set; }

    public string Text { get; set; } = null!;

    public int NumOfLikes { get; set; }

    public int IdPost { get; set; }

    public int IdUser { get; set; }

    public virtual ICollection<Commentreply> Commentreplies { get; set; } = new List<Commentreply>();

    public virtual Post IdPostNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<Likedcomment> Likedcomments { get; set; } = new List<Likedcomment>();
}
