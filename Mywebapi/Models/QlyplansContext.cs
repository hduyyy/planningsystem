//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;

//namespace Mywebapi.Models1;

//public partial class QlyplansContext : DbContext
//{
//    public QlyplansContext()
//    {
//    }

//    public QlyplansContext(DbContextOptions<QlyplansContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Department> Departments { get; set; }

//    public virtual DbSet<EvaluationCriterion> EvaluationCriteria { get; set; }

//    public virtual DbSet<ManagerEvaluation> ManagerEvaluations { get; set; }

//    public virtual DbSet<PersonalAttachment> PersonalAttachments { get; set; }

//    public virtual DbSet<PersonalPlan> PersonalPlans { get; set; }

//    public virtual DbSet<PersonalPlanApproval> PersonalPlanApprovals { get; set; }

//    public virtual DbSet<PersonalPlanTask> PersonalPlanTasks { get; set; }

//    public virtual DbSet<SelfEvaluation> SelfEvaluations { get; set; }

//    public virtual DbSet<UnitAttachment> UnitAttachments { get; set; }

//    public virtual DbSet<UnitPlan> UnitPlans { get; set; }

//    public virtual DbSet<UnitPlanApproval> UnitPlanApprovals { get; set; }

//    public virtual DbSet<UnitPlanTask> UnitPlanTasks { get; set; }

//    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=HuuDuy\\SQLEXPRESS01;Database=qlyplans;User Id=sa;Password=123456;TrustServerCertificate=True;");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Department>(entity =>
//        {
//            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED4F4728BA");

//            entity.Property(e => e.CreateAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.DepartmentName).HasMaxLength(100);
//            entity.Property(e => e.Description).HasMaxLength(300);
//        });

//        modelBuilder.Entity<EvaluationCriterion>(entity =>
//        {
//            entity.HasKey(e => e.CriteriaId).HasName("PK__Evaluati__FE6ADBCD2C9DFB2B");

//            entity.Property(e => e.CriteriaName).HasMaxLength(100);
//            entity.Property(e => e.Description).HasMaxLength(300);
//        });

//        modelBuilder.Entity<ManagerEvaluation>(entity =>
//        {
//            entity.HasKey(e => e.ManagerEvalId).HasName("PK__ManagerE__79C2A027852201B7");

//            entity.ToTable("ManagerEvaluation");

//            entity.Property(e => e.EvaluationDate)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.FeedBack).HasMaxLength(500);

//            entity.HasOne(d => d.SelfEval).WithMany(p => p.ManagerEvaluations)
//                .HasForeignKey(d => d.SelfEvalId)
//                .HasConstraintName("FK__ManagerEv__SelfE__6754599E");
//        });

//        modelBuilder.Entity<PersonalAttachment>(entity =>
//        {
//            entity.HasKey(e => e.AttachmentId).HasName("PK__Personal__442C64BE543BD4FB");

//            entity.Property(e => e.FilePath).HasMaxLength(500);
//            entity.Property(e => e.UpLoadedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");

//            entity.HasOne(d => d.Task).WithMany(p => p.PersonalAttachments)
//                .HasForeignKey(d => d.TaskId)
//                .HasConstraintName("FK__PersonalA__TaskI__75A278F5");
//        });

//        modelBuilder.Entity<PersonalPlan>(entity =>
//        {
//            entity.HasKey(e => e.PlanId).HasName("PK__Personal__755C22B7382D8A53");

//            entity.ToTable("PersonalPlan");

//            entity.Property(e => e.CreateAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.Description).HasMaxLength(300);
//            entity.Property(e => e.EndDate).HasColumnType("datetime");
//            entity.Property(e => e.StartDate).HasColumnType("datetime");
//            entity.Property(e => e.Status).HasMaxLength(50);
//            entity.Property(e => e.Title).HasMaxLength(100);

//            entity.HasOne(d => d.User).WithMany(p => p.PersonalPlans)
//                .HasForeignKey(d => d.UserId)
//                .HasConstraintName("FK__PersonalP__UserI__6B24EA82");
//        });

//        modelBuilder.Entity<PersonalPlanApproval>(entity =>
//        {
//            entity.HasKey(e => e.ApprovalId).HasName("PK__Personal__328477F4FC59D350");

//            entity.ToTable("PersonalPlanApproval");

//            entity.Property(e => e.ApprovalDate)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.Remark).HasMaxLength(300);
//            entity.Property(e => e.Status).HasMaxLength(50);

//            entity.HasOne(d => d.Task).WithMany(p => p.PersonalPlanApprovals)
//                .HasForeignKey(d => d.TaskId)
//                .HasConstraintName("FK__PersonalP__TaskI__71D1E811");
//        });

//        modelBuilder.Entity<PersonalPlanTask>(entity =>
//        {
//            entity.HasKey(e => e.TaskId).HasName("PK__Personal__7C6949B19D09353D");

//            entity.ToTable("PersonalPlanTask");

//            entity.Property(e => e.Description).HasMaxLength(300);
//            entity.Property(e => e.Status).HasMaxLength(50);
//            entity.Property(e => e.Title).HasMaxLength(100);

//            entity.HasOne(d => d.Plan).WithMany(p => p.PersonalPlanTasks)
//                .HasForeignKey(d => d.PlanId)
//                .HasConstraintName("FK__PersonalP__PlanI__6EF57B66");
//        });

//        modelBuilder.Entity<SelfEvaluation>(entity =>
//        {
//            entity.HasKey(e => e.SelfEvalId).HasName("PK__SelfEval__38880EFD65ADA635");

//            entity.ToTable("SelfEvaluation");

//            entity.Property(e => e.Comment).HasMaxLength(500);
//            entity.Property(e => e.Status).HasMaxLength(50);
//            entity.Property(e => e.Submissondate).HasColumnType("datetime");

//            entity.HasOne(d => d.Criteria).WithMany(p => p.SelfEvaluations)
//                .HasForeignKey(d => d.CriteriaId)
//                .HasConstraintName("FK__SelfEvalu__Crite__6477ECF3");

//            entity.HasOne(d => d.User).WithMany(p => p.SelfEvaluations)
//                .HasForeignKey(d => d.UserId)
//                .HasConstraintName("FK__SelfEvalu__UserI__6383C8BA");
//        });

//        modelBuilder.Entity<UnitAttachment>(entity =>
//        {
//            entity.HasKey(e => e.AttachmentId).HasName("PK__UnitAtta__442C64BEBD85E54F");

//            entity.Property(e => e.FilePath).HasMaxLength(500);
//            entity.Property(e => e.UploadedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("uploadedAt");

//            entity.HasOne(d => d.UnitPlanTask).WithMany(p => p.UnitAttachments)
//                .HasForeignKey(d => d.UnitPlanTaskId)
//                .HasConstraintName("FK__UnitAttac__UnitP__08B54D69");
//        });

//        modelBuilder.Entity<UnitPlan>(entity =>
//        {
//            entity.HasKey(e => e.UnitPlanId).HasName("PK__UnitPlan__93CD939B21AB430C");

//            entity.ToTable("UnitPlan");

//            entity.Property(e => e.CreateAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.Description).HasMaxLength(300);
//            entity.Property(e => e.EndDate).HasColumnType("datetime");
//            entity.Property(e => e.StartDate).HasColumnType("datetime");
//            entity.Property(e => e.Status).HasMaxLength(50);
//            entity.Property(e => e.Title).HasMaxLength(100);

//            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.UnitPlanAssignedToNavigations)
//                .HasForeignKey(d => d.AssignedTo)
//                .HasConstraintName("FK__UnitPlan__Assign__52593CB8");

//            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UnitPlanCreatedByNavigations)
//                .HasForeignKey(d => d.CreatedBy)
//                .HasConstraintName("FK__UnitPlan__Create__5070F446");

//            entity.HasOne(d => d.Department).WithMany(p => p.UnitPlans)
//                .HasForeignKey(d => d.DepartmentId)
//                .HasConstraintName("FK__UnitPlan__Depart__5165187F");
//        });

//        modelBuilder.Entity<UnitPlanApproval>(entity =>
//        {
//            entity.HasKey(e => e.ApprovalId).HasName("PK__UnitPlan__328477F4101DD703");

//            entity.ToTable("UnitPlanApproval");

//            entity.Property(e => e.ApprovalDate).HasColumnType("datetime");
//            entity.Property(e => e.Remark).HasMaxLength(300);
//            entity.Property(e => e.Status).HasMaxLength(50);

//            entity.HasOne(d => d.UnitTask).WithMany(p => p.UnitPlanApprovals)
//                .HasForeignKey(d => d.UnitTaskId)
//                .HasConstraintName("FK__UnitPlanA__UnitT__59FA5E80");
//        });

//        modelBuilder.Entity<UnitPlanTask>(entity =>
//        {
//            entity.HasKey(e => e.UnitTaskId).HasName("PK__UnitPlan__078E43FC92722A3F");

//            entity.ToTable("UnitPlanTask");

//            entity.Property(e => e.Description).HasMaxLength(300);
//            entity.Property(e => e.Status).HasMaxLength(50);
//            entity.Property(e => e.Title).HasMaxLength(100);

//            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.UnitPlanTasks)
//                .HasForeignKey(d => d.AssignedTo)
//                .HasConstraintName("FK__UnitPlanT__Assig__571DF1D5");

//            entity.HasOne(d => d.UnitPlan).WithMany(p => p.UnitPlanTasks)
//                .HasForeignKey(d => d.UnitPlanId)
//                .HasConstraintName("FK__UnitPlanT__UnitP__5629CD9C");
//        });

//        modelBuilder.Entity<User>(entity =>
//        {
//            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C98073BAC");

//            entity.Property(e => e.Address).HasMaxLength(300);
//            entity.Property(e => e.CreateAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.Email).HasMaxLength(100);
//            entity.Property(e => e.Fullname).HasMaxLength(100);
//            entity.Property(e => e.PasswordHash).HasMaxLength(255);
//            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
//            entity.Property(e => e.Role).HasMaxLength(50);
//            entity.Property(e => e.Username).HasMaxLength(50);

//            entity.HasOne(d => d.Department).WithMany(p => p.Users)
//                .HasForeignKey(d => d.DepartmentId)
//                .HasConstraintName("FK__Users__Departmen__4CA06362");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
