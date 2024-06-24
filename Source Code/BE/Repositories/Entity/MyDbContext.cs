using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Entity;
public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<Design> Designs { get; set; }

    public virtual DbSet<DesignRule> DesignRules { get; set; }

    public virtual DbSet<Have> Haves { get; set; }

    public virtual DbSet<MasterGemstone> MasterGemstones { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Requirement> Requirements { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Stones> Stones { get; set; }

    public virtual DbSet<TypeOfJewellery> TypeOfJewelleries { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<WarrantyCard> WarrantyCards { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;uid=sa;pwd=12345;database=JewelleryOrder;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blog__54379E500BD944C0");

            entity.ToTable("Blog");

            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Image).HasMaxLength(200);
            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Manager).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Blog__ManagerID__2B3F6F97");
        });

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.ConversationId).HasName("PK__Conversa__C050D877F14CB8D6");

            entity.HasOne(d => d.User1).WithMany(p => p.ConversationUser1s)
                .HasForeignKey(d => d.User1Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User1");

            entity.HasOne(d => d.User2).WithMany(p => p.ConversationUser2s)
                .HasForeignKey(d => d.User2Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User2");
        });

        modelBuilder.Entity<Design>(entity =>
        {
            entity.HasKey(e => e.DesignId).HasName("PK__Design__32B8E17F5DE3CE4F");

            entity.ToTable("Design");

            entity.Property(e => e.DesignId).HasColumnName("DesignID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.DesignName).HasMaxLength(100);
            entity.Property(e => e.Image).HasMaxLength(200);
            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
            entity.Property(e => e.MasterGemstoneId).HasColumnName("MasterGemstoneID");
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.StonesId).HasColumnName("StoneID");
            entity.Property(e => e.TypeOfJewelleryId).HasColumnName("TypeOfJewelleryID");

            entity.HasOne(d => d.Manager).WithMany(p => p.Designs)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Design__ManagerI__38996AB5");

            entity.HasOne(d => d.MasterGemstone).WithMany(p => p.Designs)
                .HasForeignKey(d => d.MasterGemstoneId)
                .HasConstraintName("FK__Design__MasterGe__37A5467C");

            entity.HasOne(d => d.Material).WithMany(p => p.Designs)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("FK__Design__Material__398D8EEE");

            entity.HasOne(d => d.Stone).WithMany(p => p.Designs)
                .HasForeignKey(d => d.StonesId)
                .HasConstraintName("FK__Design__StoneID__36B12243");

            entity.HasOne(d => d.TypeOfJewellery).WithMany(p => p.Designs)
                .HasForeignKey(d => d.TypeOfJewelleryId)
                .HasConstraintName("FK__Design__TypeOfJe__3A81B327");
        });

        modelBuilder.Entity<DesignRule>(entity =>
        {
            entity.HasKey(e => e.DesignRuleId).HasName("PK__DesignRu__3850E3632F415A54");

            entity.ToTable("DesignRule");

            entity.Property(e => e.MaxSizeJewellery).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaxSizeMasterGemstone).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MinSizeJewellery).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MinSizeMasterGemstone).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TypeOfJewelleryId).HasColumnName("TypeOfJewelleryID");

            entity.HasOne(d => d.TypeOfJewellery).WithMany(p => p.DesignRules)
                .HasForeignKey(d => d.TypeOfJewelleryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DesignRul__TypeO__4D94879B");
        });

        modelBuilder.Entity<Have>(entity =>
        {
            entity.HasKey(e => new { e.WarrantyCardId, e.RequirementId }).HasName("PK__Have__FBE292CDF1A88E42");

            entity.ToTable("Have");

            entity.Property(e => e.WarrantyCardId).HasColumnName("WarrantyCardID");
            entity.Property(e => e.RequirementId).HasColumnName("RequirementID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

            entity.HasOne(d => d.Requirement).WithMany(p => p.Haves)
                .HasForeignKey(d => d.RequirementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Have__Requiremen__4AB81AF0");

            entity.HasOne(d => d.WarrantyCard).WithMany(p => p.Haves)
                .HasForeignKey(d => d.WarrantyCardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Have__WarrantyCa__49C3F6B7");
        });

        modelBuilder.Entity<MasterGemstone>(entity =>
        {
            entity.HasKey(e => e.MasterGemstoneId).HasName("PK__MasterGe__D4657CE37EE9171A");

            entity.ToTable("MasterGemstone");

            entity.Property(e => e.MasterGemstoneId).HasColumnName("MasterGemstoneID");
            entity.Property(e => e.Clarity).HasMaxLength(50);
            entity.Property(e => e.Cut).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(200);
            entity.Property(e => e.Kind).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Shape).HasMaxLength(50);
            entity.Property(e => e.Size).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__Material__C5061317C544B96F");

            entity.ToTable("Material");

            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
            entity.Property(e => e.Image).HasMaxLength(200);
            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Manager).WithMany(p => p.Materials)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Material__Manage__2E1BDC42");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C0C9CA6C5A525");

            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Conversation");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Receiver");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sender");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A5846F6072B");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CompletedAt).HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Method).HasMaxLength(50);
            entity.Property(e => e.RequirementsId).HasColumnName("RequirementsID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Payment__Custome__440B1D61");

            entity.HasOne(d => d.Requirements).WithMany(p => p.Payments)
                .HasForeignKey(d => d.RequirementsId)
                .HasConstraintName("FK__Payment__Require__44FF419A");
        });

        modelBuilder.Entity<Requirement>(entity =>
        {
            entity.HasKey(e => e.RequirementId).HasName("PK__Requirem__7DF11E7D94BF4243");

            entity.Property(e => e.RequirementId).HasColumnName("RequirementID");
            entity.Property(e => e.CustomerNote).HasColumnType("text");
            entity.Property(e => e.Design3D).HasMaxLength(200);
            entity.Property(e => e.DesignId).HasColumnName("DesignID");
            entity.Property(e => e.MachiningFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaterialPriceAtMoment).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Size).HasMaxLength(50);
            entity.Property(e => e.StaffNote).HasColumnType("text");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StonePriceAtMoment).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalMoney).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.WeightOfMaterial).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Design).WithMany(p => p.Requirements)
                .HasForeignKey(d => d.DesignId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requireme__Desig__3D5E1FD2");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3AD4B3F70F");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Stones>(entity =>
        {
            entity.HasKey(e => e.StonesId).HasName("PK__Stones__59F240A0DD21836A");

            entity.Property(e => e.StonesId).HasColumnName("StoneID");
            entity.Property(e => e.Kind).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Size).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TypeOfJewellery>(entity =>
        {
            entity.HasKey(e => e.TypeOfJewelleryId).HasName("PK__TypeOfJe__F1D25D481DBEFCA9");

            entity.ToTable("TypeOfJewellery");

            entity.Property(e => e.TypeOfJewelleryId).HasColumnName("TypeOfJewelleryID");
            entity.Property(e => e.Image).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.UsersId).HasName("PK__Users__A349B0422EF97578");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053424297754").IsUnique();

            entity.Property(e => e.UsersId).HasColumnName("UsersID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Image).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleID__286302EC");

        });
        modelBuilder.Entity<UserRequirement>(entity =>
        {
            entity.HasKey(e => new { e.UsersId, e.RequirementId }).HasName("PK__UsersReq__6496A1A5A896C4DF");

            entity.ToTable("UsersRequirements");

            entity.Property(e => e.UsersId).HasColumnName("UsersID");
            entity.Property(e => e.RequirementId).HasColumnName("RequirementID");

            entity.HasOne(d => d.Requirement).WithMany(p => p.UserRequirements)
                .HasForeignKey(d => d.RequirementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersRequ__Requi__412EB0B6");

            entity.HasOne(d => d.User).WithMany(p => p.UserRequirements)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersRequ__Users__403A8C7D");
        });
        modelBuilder.Entity<WarrantyCard>(entity =>
        {
            entity.HasKey(e => e.WarrantyCardId).HasName("PK__Warranty__3C3D832A09B3A35E");

            entity.ToTable("WarrantyCard");

            entity.Property(e => e.WarrantyCardId).HasColumnName("WarrantyCardID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
