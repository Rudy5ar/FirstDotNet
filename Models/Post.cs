using System;
using System.Collections.Generic;

namespace FirstDotNet.Models;

public partial class Post
{
    public int IdPost { get; set; }

    public int TotalLikes { get; set; }

    public string? Description { get; set; }

    public DateOnly DateCreated { get; set; }

    public byte[]? Image { get; set; }

    public int IdUser { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
}
