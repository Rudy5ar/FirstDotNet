using System;
using System.Collections.Generic;

namespace FirstDotNet.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? ResetToken { get; set; }

    public DateTime? TokenExpiration { get; set; }

    public virtual ICollection<Commentreply> Commentreplies { get; set; } = new List<Commentreply>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Following> FollowingIdUser1Navigations { get; set; } = new List<Following>();

    public virtual ICollection<Following> FollowingIdUser2Navigations { get; set; } = new List<Following>();

    public virtual ICollection<Likedcomment> Likedcomments { get; set; } = new List<Likedcomment>();

    public virtual ICollection<Likedreply> Likedreplies { get; set; } = new List<Likedreply>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
