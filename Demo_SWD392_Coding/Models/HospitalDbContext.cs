using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Demo_SWD392_Coding.Models;

public partial class HospitalDbContext : DbContext
{
    public HospitalDbContext()
    {
    }

    public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<MedicalRecordDetail> MedicalRecordDetails { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<MedicineType> MedicineTypes { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Receptionist> Receptionists { get; set; }

    public virtual DbSet<ScheduleDetail> ScheduleDetails { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =(local); database = HospitalDB;uid=sa;pwd=123456;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminCode).HasName("PK__Admin__E3C99C7BC7BAC5A1");

            entity.ToTable("Admin");

            entity.HasIndex(e => e.UserId, "UQ__Admin__CB9A1CFEF9796FA9").IsUnique();

            entity.Property(e => e.AdminCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("adminCode");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithOne(p => p.Admin)
                .HasForeignKey<Admin>(d => d.UserId)
                .HasConstraintName("FK__Admin__userId__5070F446");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentCode).HasName("PK__Appointm__6BAB9A5BC4A74F78");

            entity.ToTable("Appointment");

            entity.Property(e => e.AppointmentCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("appointmentCode");
            entity.Property(e => e.DoctorCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("doctorCode");
            entity.Property(e => e.PatientCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("patientCode");
            entity.Property(e => e.ScheduleCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("scheduleCode");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.DoctorCodeNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorCode)
                .HasConstraintName("FK__Appointme__docto__693CA210");

            entity.HasOne(d => d.PatientCodeNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientCode)
                .HasConstraintName("FK__Appointme__patie__6A30C649");

            entity.HasOne(d => d.ScheduleCodeNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ScheduleCode)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Appointme__sched__6B24EA82");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentCode).HasName("PK__Departme__7BF423AC275E0907");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("departmentCode");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorCode).HasName("PK__Doctor__4B36E45E75D5F51E");

            entity.ToTable("Doctor");

            entity.HasIndex(e => e.UserId, "UQ__Doctor__CB9A1CFE6FC994AA").IsUnique();

            entity.Property(e => e.DoctorCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("doctorCode");
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("departmentCode");
            entity.Property(e => e.SymptomSupport)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("symptomSupport");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.DepartmentCodeNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.DepartmentCode)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Doctor__departme__5535A963");

            entity.HasOne(d => d.User).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.UserId)
                .HasConstraintName("FK__Doctor__userId__5441852A");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.RecordCode).HasName("PK__MedicalR__BC170460AB017032");

            entity.ToTable("MedicalRecord");

            entity.Property(e => e.RecordCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("recordCode");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.PatientCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("patientCode");

            entity.HasOne(d => d.PatientCodeNavigation).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.PatientCode)
                .HasConstraintName("FK__MedicalRe__patie__6383C8BA");
        });

        modelBuilder.Entity<MedicalRecordDetail>(entity =>
        {
            entity.HasKey(e => e.RecordDetailCode).HasName("PK__MedicalR__DBD2F7C29059739E");

            entity.ToTable("MedicalRecordDetail");

            entity.Property(e => e.RecordDetailCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("recordDetailCode");
            entity.Property(e => e.AppointmentCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("appointmentCode");
            entity.Property(e => e.DoctorCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("doctorCode");
            entity.Property(e => e.RecordCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("recordCode");
            entity.Property(e => e.Result)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("result");

            entity.HasOne(d => d.AppointmentCodeNavigation).WithMany(p => p.MedicalRecordDetails)
                .HasForeignKey(d => d.AppointmentCode)
                .HasConstraintName("FK__MedicalRe__appoi__6E01572D");

            entity.HasOne(d => d.DoctorCodeNavigation).WithMany(p => p.MedicalRecordDetails)
                .HasForeignKey(d => d.DoctorCode)
                .HasConstraintName("FK__MedicalRe__docto__6FE99F9F");

            entity.HasOne(d => d.RecordCodeNavigation).WithMany(p => p.MedicalRecordDetails)
                .HasForeignKey(d => d.RecordCode)
                .HasConstraintName("FK__MedicalRe__recor__6EF57B66");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedicineCode).HasName("PK__Medicine__B9362A22D9E1F540");

            entity.ToTable("Medicine");

            entity.Property(e => e.MedicineCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("medicineCode");
            entity.Property(e => e.MedicineTypeCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("medicineTypeCode");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Stock)
                .HasDefaultValue(0)
                .HasColumnName("stock");

            entity.HasOne(d => d.MedicineTypeCodeNavigation).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.MedicineTypeCode)
                .HasConstraintName("FK__Medicine__medici__778AC167");
        });

        modelBuilder.Entity<MedicineType>(entity =>
        {
            entity.HasKey(e => e.MedicineTypeCode).HasName("PK__Medicine__54D89542FE8DBA4A");

            entity.ToTable("MedicineType");

            entity.Property(e => e.MedicineTypeCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("medicineTypeCode");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientCode).HasName("PK__Patient__A1A7B02753752133");

            entity.ToTable("Patient");

            entity.HasIndex(e => e.UserId, "UQ__Patient__CB9A1CFEA5C9659C").IsUnique();

            entity.Property(e => e.PatientCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("patientCode");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasColumnName("fullname");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithOne(p => p.Patient)
                .HasForeignKey<Patient>(d => d.UserId)
                .HasConstraintName("FK__Patient__userId__5CD6CB2B");
        });

        modelBuilder.Entity<Receptionist>(entity =>
        {
            entity.HasKey(e => e.ReceptionistCode).HasName("PK__Receptio__848E0F11D299C6A0");

            entity.ToTable("Receptionist");

            entity.HasIndex(e => e.UserId, "UQ__Receptio__CB9A1CFEB05AEC37").IsUnique();

            entity.Property(e => e.ReceptionistCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("receptionistCode");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithOne(p => p.Receptionist)
                .HasForeignKey<Receptionist>(d => d.UserId)
                .HasConstraintName("FK__Reception__userI__59063A47");
        });

        modelBuilder.Entity<ScheduleDetail>(entity =>
        {
            entity.HasKey(e => e.ScheduleCode).HasName("PK__Schedule__2D9C9A60B363BC46");

            entity.ToTable("ScheduleDetail");

            entity.Property(e => e.ScheduleCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("scheduleCode");
            entity.Property(e => e.AppointmentDate).HasColumnName("appointmentDate");
            entity.Property(e => e.AppointmentTime).HasColumnName("appointmentTime");
            entity.Property(e => e.DoctorCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("doctorCode");

            entity.HasOne(d => d.DoctorCodeNavigation).WithMany(p => p.ScheduleDetails)
                .HasForeignKey(d => d.DoctorCode)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__ScheduleD__docto__66603565");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceCode).HasName("PK__Service__E5ABEC53EE821431");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("serviceCode");
            entity.Property(e => e.Cost)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cost");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83FA946E95C");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164523CE609").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC5720B90104C").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasColumnName("fullname");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.WarehouseCode).HasName("PK__Warehous__CFC9B62C4FDC7EF9");

            entity.ToTable("Warehouse");

            entity.Property(e => e.WarehouseCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("warehouseCode");
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("departmentCode");

            entity.HasOne(d => d.DepartmentCodeNavigation).WithMany(p => p.Warehouses)
                .HasForeignKey(d => d.DepartmentCode)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Warehouse__depar__5FB337D6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
