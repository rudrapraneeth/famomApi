using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HomeMade.Core.Entities
{
    public partial class FamomContext : DbContext
    {
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Apartment> Apartment { get; set; }
        public virtual DbSet<ApartmentPromotion> ApartmentPromotion { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<AvailabilityType> AvailabilityType { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Chef> Chef { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<DeliveryType> DeliveryType { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<NotificationType> NotificationType { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostImage> PostImage { get; set; }
        public virtual DbSet<PostSide> PostSide { get; set; }
        public virtual DbSet<Promotion> Promotion { get; set; }
        public virtual DbSet<PushNotification> PushNotification { get; set; }
        public virtual DbSet<QuantityType> QuantityType { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Side> Side { get; set; }
        public virtual DbSet<SubOrder> SubOrder { get; set; }
        public virtual DbSet<SubOrderPromotion> SubOrderPromotion { get; set; }
        public virtual DbSet<UsageData> UsageData { get; set; }
        public virtual DbSet<UserApartment> UserApartment { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }

        public FamomContext(DbContextOptions<FamomContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Scaffolding:ConnectionString", "Data Source=(local);Initial Catalog=Famom;Integrated Security=true");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.HasIndex(e => e.AddressId);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Apartment)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Apartment__Addre__17C286CF");
            });

            modelBuilder.Entity<ApartmentPromotion>(entity =>
            {
                entity.HasIndex(e => e.ApartmentId);

                entity.HasIndex(e => e.PromotionId);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.ApartmentPromotion)
                    .HasForeignKey(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ApartmentPromotion__Apartment");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.ApartmentPromotion)
                    .HasForeignKey(d => d.PromotionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ApartmentPromotion__Promotion");
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasIndex(e => new { e.ApplicationUserId, e.MobileNumber, e.UserTypeId });

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(650)
                    .IsUnicode(false);

                entity.Property(e => e.ExpoPushToken)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.ApplicationUser)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Applicati__UserT__151B244E");
            });

            modelBuilder.Entity<AvailabilityType>(entity =>
            {
                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Chef>(entity =>
            {
                entity.HasIndex(e => e.ApplicationUserId)
                    .IsUnique();

                entity.HasIndex(e => new { e.ChefId, e.ApplicationUserId });

                entity.Property(e => e.AboutMe)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ApplicationUser)
                    .WithOne(p => p.Chef)
                    .HasForeignKey<Chef>(d => d.ApplicationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Chef__Applicatio__1AD3FDA4");

                entity.HasOne(d => d.ProfileImage)
                    .WithMany(p => p.Chef)
                    .HasForeignKey(d => d.ProfileImageId)
                    .HasConstraintName("FK__ChefImage__Image");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.ApplicationUserId)
                    .IsUnique();

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ApplicationUser)
                    .WithOne(p => p.Customer)
                    .HasForeignKey<Customer>(d => d.ApplicationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customer__Applic__17F790F9");
            });

            modelBuilder.Entity<DeliveryType>(entity =>
            {
                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FileName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Metadata)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Categories)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.Property(e => e.Body)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NotificationTypeCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_OrderId");

                entity.HasIndex(e => new { e.CustomerId, e.OrderId });

                entity.HasIndex(e => e.StatusId);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDateTime).HasColumnType("datetime");

                entity.Property(e => e.TotalCost).HasColumnType("smallmoney");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__Customer__3CF40B7E");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__StatusId__3DE82FB7");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasIndex(e => e.ChefId);

                entity.HasIndex(e => e.DeliveryTypeId);

                entity.HasIndex(e => e.MenuId);

                entity.HasIndex(e => e.PostId);

                entity.Property(e => e.AvailabilityTypeId).HasDefaultValueSql("2");

                entity.Property(e => e.AvailableFrom).HasColumnType("datetime");

                entity.Property(e => e.AvailableTo).HasColumnType("datetime");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryCharge).HasColumnType("smallmoney");

                entity.Property(e => e.InactiveDateTime).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.AvailabilityType)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.AvailabilityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__AvailabilityTypeId");

                entity.HasOne(d => d.Chef)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.ChefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__ChefId__236943A5");

                entity.HasOne(d => d.DeliveryType)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.DeliveryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__DeliveryTy__2645B050");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__MenuId__245D67DE");

                entity.HasOne(d => d.QuantityType)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.QuantityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__QuantityTy__25518C17");
            });

            modelBuilder.Entity<PostImage>(entity =>
            {
                entity.HasIndex(e => e.ImageId);

                entity.HasIndex(e => e.PostId);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.PostImage)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostImage__Image__24285DB4");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostImage)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostImage__PostI__251C81ED");
            });

            modelBuilder.Entity<PostSide>(entity =>
            {
                entity.HasKey(e => e.SideId);

                entity.HasIndex(e => e.PostId);

                entity.Property(e => e.SideId).ValueGeneratedOnAdd();

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostSide)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostSide__PostId__2BFE89A6");

                entity.HasOne(d => d.Side)
                    .WithOne(p => p.PostSide)
                    .HasForeignKey<PostSide>(d => d.SideId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostSide__SideId__2B0A656D");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountAmount).HasColumnType("smallmoney");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.PromoCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PromoType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<PushNotification>(entity =>
            {
                entity.HasIndex(e => e.NotificationTypeId);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceValue)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ScheduleDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.PushNotification)
                    .HasForeignKey(d => d.NotificationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PushNotif__Notif__4E1E9780");
            });

            modelBuilder.Entity<QuantityType>(entity =>
            {
                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasIndex(e => e.ChefId);

                entity.HasIndex(e => e.SubOrderId);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rating1).HasColumnName("Rating");

                entity.Property(e => e.Review).HasMaxLength(1000);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Chef)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.ChefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rating__ChefId__55BFB948");

                entity.HasOne(d => d.SubOrder)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.SubOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rating__SubOrder__54CB950F");
            });

            modelBuilder.Entity<Side>(entity =>
            {
                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<SubOrder>(entity =>
            {
                entity.HasIndex(e => e.OrderId);

                entity.HasIndex(e => e.PostId);

                entity.HasIndex(e => e.StatusId);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryDateTime).HasColumnType("datetime");

                entity.Property(e => e.Instructions).HasMaxLength(255);

                entity.Property(e => e.TotalDiscountPrice).HasColumnType("smallmoney");

                entity.Property(e => e.TotalPrice).HasColumnType("smallmoney");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.SubOrder)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SubOrder__OrderI__40C49C62");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.SubOrder)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SubOrder__PostId__41B8C09B");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.SubOrder)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SubOrder__Status__42ACE4D4");
            });

            modelBuilder.Entity<SubOrderPromotion>(entity =>
            {
                entity.HasIndex(e => e.PromotionId);

                entity.HasIndex(e => e.SubOrderId);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.SubOrderPromotion)
                    .HasForeignKey(d => d.PromotionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SubOrderPromotion__PromotionId");

                entity.HasOne(d => d.SubOrder)
                    .WithMany(p => p.SubOrderPromotion)
                    .HasForeignKey(d => d.SubOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SubOrderPromotion__SubOrderId");
            });

            modelBuilder.Entity<UsageData>(entity =>
            {
                entity.Property(e => e.ActionName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastActionDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.UsageData)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsageData_ApplicationUserId");
            });

            modelBuilder.Entity<UserApartment>(entity =>
            {
                entity.HasIndex(e => e.ApplicationUserId);

                entity.HasIndex(e => new { e.ApplicationUserId, e.ApartmentId, e.UserApartmentId });

                entity.Property(e => e.ApartmentNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Block)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.UserApartment)
                    .HasForeignKey(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserApart__Apart__1F63A897");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.UserApartment)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserApart__Appli__1E6F845E");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
