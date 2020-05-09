using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using M.Repository.Entity;

namespace M.Repository.Context
{
    public partial class MovieDBContext : DbContext
    {
        public MovieDBContext()
        {
        }

        public MovieDBContext(DbContextOptions<MovieDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MovieAttributes> MovieAttributes { get; set; }
        public virtual DbSet<MovieBase> MovieBase { get; set; }
        public virtual DbSet<MovieComment> MovieComment { get; set; }
        public virtual DbSet<MovieImages> MovieImages { get; set; }
        public virtual DbSet<SystemConfigMenu> SystemConfigMenu { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserRoleRelation> UserRoleRelation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Movie;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieAttributes>(entity =>
            {
                entity.HasKey(e => e.AttributesId);

                entity.Property(e => e.Alias).HasMaxLength(64);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(64);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<MovieBase>(entity =>
            {
                entity.HasKey(e => e.MovieId)
                    .HasName("PK_Movie");

                entity.Property(e => e.MovieId).HasComment("id");

                entity.Property(e => e.Actor)
                    .HasMaxLength(512)
                    .HasComment("演员");

                entity.Property(e => e.AliasTitle)
                    .HasMaxLength(256)
                    .HasComment("别名");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.DoubanScore).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DownloadUri)
                    .HasColumnType("ntext")
                    .HasComment("下载地址");

                entity.Property(e => e.MovieUri)
                    .HasMaxLength(128)
                    .HasComment("电影原链接");

                entity.Property(e => e.OldId).HasComment("旧的ID");

                entity.Property(e => e.Region)
                    .HasMaxLength(64)
                    .HasComment("国家区域");

                entity.Property(e => e.RegionAttributes).HasMaxLength(64);

                entity.Property(e => e.Resolution)
                    .HasMaxLength(64)
                    .HasComment("分辨率");

                entity.Property(e => e.Score)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("得分");

                entity.Property(e => e.Source)
                    .HasMaxLength(255)
                    .HasComment("来源");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Summary)
                    .HasMaxLength(4000)
                    .HasComment("剧情描述");

                entity.Property(e => e.ThumbUri)
                    .HasMaxLength(128)
                    .HasComment("缩略图");

                entity.Property(e => e.Time).HasComment("年代");

                entity.Property(e => e.Title)
                    .HasMaxLength(256)
                    .HasComment("名称");

                entity.Property(e => e.Type)
                    .HasMaxLength(64)
                    .HasComment("类型");

                entity.Property(e => e.TypeAttributes).HasMaxLength(64);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.Views).HasComment("浏览量");
            });

            modelBuilder.Entity<MovieComment>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.Property(e => e.Content).HasMaxLength(512);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PcitureUrl).HasMaxLength(64);

                entity.Property(e => e.ToUserName).HasMaxLength(64);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UserIp).HasMaxLength(64);

                entity.Property(e => e.UserName).HasMaxLength(64);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieComment)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK_MovieComment_MovieBase");
            });

            modelBuilder.Entity<MovieImages>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Domain).HasMaxLength(64);

                entity.Property(e => e.Name).HasMaxLength(64);

                entity.Property(e => e.OriginalUrl).HasMaxLength(256);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.VirtualPath).HasMaxLength(256);
            });

            modelBuilder.Entity<SystemConfigMenu>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Icon).HasMaxLength(64);

                entity.Property(e => e.Link).HasMaxLength(64);

                entity.Property(e => e.Title).HasMaxLength(64);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(64);

                entity.Property(e => e.Mobile).HasMaxLength(64);

                entity.Property(e => e.Password).HasMaxLength(64);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(61);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK_Roles");

                entity.Property(e => e.RoleId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Decription).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(64);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserRoleRelation>(entity =>
            {
                entity.HasKey(e => e.UserRoleId)
                    .HasName("PK_UserRoles");

                entity.Property(e => e.UserRoleId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoleRelation)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserRole_Roles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoleRelation)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRoles_UserBases_UserId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
