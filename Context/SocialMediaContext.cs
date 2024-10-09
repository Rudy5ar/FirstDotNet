using System;
using System.Collections.Generic;
using FirstDotNet.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FirstDotNet.Context;

public partial class SocialMediaContext : DbContext
{
    public SocialMediaContext()
    {
    }

    public SocialMediaContext(DbContextOptions<SocialMediaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Commentreply> Commentreplies { get; set; }

    public virtual DbSet<FlywaySchemaHistory> FlywaySchemaHistories { get; set; }

    public virtual DbSet<Following> Followings { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Likedcomment> Likedcomments { get; set; }

    public virtual DbSet<Likedreply> Likedreplies { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;database=social_media;user=root;password=1234", ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.IdComment).HasName("PRIMARY");

            entity.ToTable("comment");

            entity.HasIndex(e => e.IdPost, "idPost");

            entity.HasIndex(e => e.IdUser, "idUser");

            entity.Property(e => e.IdComment).HasColumnName("idComment");
            entity.Property(e => e.IdPost).HasColumnName("idPost");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.NumOfLikes).HasColumnName("numOfLikes");
            entity.Property(e => e.Text)
                .HasMaxLength(45)
                .HasColumnName("text");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_ibfk_1");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_ibfk_2");
        });

        modelBuilder.Entity<Commentreply>(entity =>
        {
            entity.HasKey(e => e.IdCommentReply).HasName("PRIMARY");

            entity.ToTable("commentreply");

            entity.HasIndex(e => e.IdComment, "idComment");

            entity.HasIndex(e => e.IdUser, "idUser");

            entity.Property(e => e.IdCommentReply).HasColumnName("idCommentReply");
            entity.Property(e => e.IdComment).HasColumnName("idComment");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.NumOfLikes).HasColumnName("numOfLikes");
            entity.Property(e => e.Text)
                .HasMaxLength(45)
                .HasColumnName("text");

            entity.HasOne(d => d.IdCommentNavigation).WithMany(p => p.Commentreplies)
                .HasForeignKey(d => d.IdComment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("commentreply_ibfk_1");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Commentreplies)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("commentreply_ibfk_2");
        });

        modelBuilder.Entity<FlywaySchemaHistory>(entity =>
        {
            entity.HasKey(e => e.InstalledRank).HasName("PRIMARY");

            entity.ToTable("flyway_schema_history");

            entity.HasIndex(e => e.Success, "flyway_schema_history_s_idx");

            entity.Property(e => e.InstalledRank)
                .ValueGeneratedNever()
                .HasColumnName("installed_rank");
            entity.Property(e => e.Checksum).HasColumnName("checksum");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.ExecutionTime).HasColumnName("execution_time");
            entity.Property(e => e.InstalledBy)
                .HasMaxLength(100)
                .HasColumnName("installed_by");
            entity.Property(e => e.InstalledOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("installed_on");
            entity.Property(e => e.Script)
                .HasMaxLength(1000)
                .HasColumnName("script");
            entity.Property(e => e.Success).HasColumnName("success");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
            entity.Property(e => e.Version)
                .HasMaxLength(50)
                .HasColumnName("version");
        });

        modelBuilder.Entity<Following>(entity =>
        {
            entity.HasKey(e => e.IdFollowing).HasName("PRIMARY");

            entity.ToTable("following");

            entity.HasIndex(e => e.IdUser2, "idUser2");

            entity.HasIndex(e => new { e.IdUser1, e.IdUser2 }, "unique_follow").IsUnique();

            entity.Property(e => e.IdFollowing).HasColumnName("idFollowing");
            entity.Property(e => e.IdUser1).HasColumnName("idUser1");
            entity.Property(e => e.IdUser2).HasColumnName("idUser2");
            entity.Property(e => e.Pending)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("pending");

            entity.HasOne(d => d.IdUser1Navigation).WithMany(p => p.FollowingIdUser1Navigations)
                .HasForeignKey(d => d.IdUser1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("following_ibfk_1");

            entity.HasOne(d => d.IdUser2Navigation).WithMany(p => p.FollowingIdUser2Navigations)
                .HasForeignKey(d => d.IdUser2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("following_ibfk_2");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => e.IdLike).HasName("PRIMARY");

            entity.ToTable("like");

            entity.HasIndex(e => e.IdPost, "idPost");

            entity.HasIndex(e => new { e.IdUser, e.IdPost }, "unique_liked_post").IsUnique();

            entity.Property(e => e.IdLike).HasColumnName("idLike");
            entity.Property(e => e.DateLiked).HasColumnName("dateLiked");
            entity.Property(e => e.IdPost).HasColumnName("idPost");
            entity.Property(e => e.IdUser).HasColumnName("idUser");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Likes)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("like_ibfk_2");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Likes)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("like_ibfk_1");
        });

        modelBuilder.Entity<Likedcomment>(entity =>
        {
            entity.HasKey(e => e.IdLikedComment).HasName("PRIMARY");

            entity.ToTable("likedcomment");

            entity.HasIndex(e => e.IdComment, "idComment");

            entity.HasIndex(e => new { e.IdUser, e.IdComment }, "unique_liked_comment").IsUnique();

            entity.Property(e => e.IdLikedComment).HasColumnName("idLikedComment");
            entity.Property(e => e.IdComment).HasColumnName("idComment");
            entity.Property(e => e.IdUser).HasColumnName("idUser");

            entity.HasOne(d => d.IdCommentNavigation).WithMany(p => p.Likedcomments)
                .HasForeignKey(d => d.IdComment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("likedcomment_ibfk_2");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Likedcomments)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("likedcomment_ibfk_1");
        });

        modelBuilder.Entity<Likedreply>(entity =>
        {
            entity.HasKey(e => e.IdLikedReply).HasName("PRIMARY");

            entity.ToTable("likedreply");

            entity.HasIndex(e => e.IdCommentReply, "idCommentReply");

            entity.HasIndex(e => new { e.IdUser, e.IdCommentReply }, "unique_liked_reply").IsUnique();

            entity.Property(e => e.IdLikedReply).HasColumnName("idLikedReply");
            entity.Property(e => e.IdCommentReply).HasColumnName("idCommentReply");
            entity.Property(e => e.IdUser).HasColumnName("idUser");

            entity.HasOne(d => d.IdCommentReplyNavigation).WithMany(p => p.Likedreplies)
                .HasForeignKey(d => d.IdCommentReply)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("likedreply_ibfk_2");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Likedreplies)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("likedreply_ibfk_1");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.IdPost).HasName("PRIMARY");

            entity.ToTable("post");

            entity.HasIndex(e => e.IdUser, "idUser");

            entity.Property(e => e.IdPost).HasColumnName("idPost");
            entity.Property(e => e.DateCreated).HasColumnName("dateCreated");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .HasColumnName("description");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Image)
                .HasColumnType("blob")
                .HasColumnName("image");
            entity.Property(e => e.TotalLikes).HasColumnName("totalLikes");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.ResetToken)
                .HasMaxLength(255)
                .HasColumnName("resetToken");
            entity.Property(e => e.TokenExpiration)
                .HasColumnType("datetime")
                .HasColumnName("tokenExpiration");
            entity.Property(e => e.Username)
                .HasMaxLength(45)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
