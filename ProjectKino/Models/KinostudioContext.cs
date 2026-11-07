using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectKino.Models;

public partial class KinoStudioContext : DbContext
{
    public KinoStudioContext()
    {
    }

    public KinoStudioContext(DbContextOptions<KinoStudioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ГрафикСъёмок> ГрафикСъёмокs { get; set; }

    public virtual DbSet<Жанр> Жанрs { get; set; }

    public virtual DbSet<Затраты> Затратыs { get; set; }

    public virtual DbSet<КонтактныеДанные> КонтактныеДанныеs { get; set; }

    public virtual DbSet<Проект> Проектs { get; set; }

    public virtual DbSet<Прокат> Прокатs { get; set; }

    public virtual DbSet<СоставСъёмочнойГруппы> СоставСъёмочнойГруппыs { get; set; }

    public virtual DbSet<Сотрудник> Сотрудникs { get; set; }

    public virtual DbSet<Специализация> Специализацияs { get; set; }

    public virtual DbSet<Статус> Статусs { get; set; }

    public virtual DbSet<СъёмочнаяГруппа> СъёмочнаяГруппаs { get; set; }

    public virtual DbSet<СъёмочнаяПлощадка> СъёмочнаяПлощадкаs { get; set; }

    public virtual DbSet<СъёмочнаяПлощадкаПроекта> СъёмочнаяПлощадкаПроектаs { get; set; }

    public virtual DbSet<ТипПлощадки> ТипПлощадкиs { get; set; }

    public virtual DbSet<ЦелеваяАудитория> ЦелеваяАудиторияs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=KinoStudio;Username=postgres;Password=phttsql");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ГрафикСъёмок>(entity =>
        {
            entity.HasKey(e => e.КодГрафикаСъёмок).HasName("График_съёмок_pkey");

            entity.ToTable("График_съёмок");

            entity.Property(e => e.КодГрафикаСъёмок).HasColumnName("Код_графика_съёмок");
            entity.Property(e => e.Сцена).HasColumnType("character varying");
            entity.Property(e => e.СъёмочнаяГруппа).HasColumnName("Съёмочная_группа");

            entity.HasOne(d => d.ПлощадкаNavigation).WithMany(p => p.ГрафикСъёмокs)
                .HasForeignKey(d => d.Площадка)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("График_Площадка_fkey");

            entity.HasOne(d => d.СъёмочнаяГруппаNavigation).WithMany(p => p.ГрафикСъёмокs)
                .HasForeignKey(d => d.СъёмочнаяГруппа)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("График_съёмок_Съёмочная_группа_fkey");
        });

        modelBuilder.Entity<Жанр>(entity =>
        {
            entity.HasKey(e => e.КодЖанра).HasName("Жанр_pkey");

            entity.ToTable("Жанр");

            entity.Property(e => e.КодЖанра).HasColumnName("Код_жанра");
            entity.Property(e => e.Наименование).HasMaxLength(199);
        });

        modelBuilder.Entity<Затраты>(entity =>
        {
            entity.HasKey(e => e.КодЗатраты).HasName("Затраты_pkey");

            entity.ToTable("Затраты");

            entity.Property(e => e.КодЗатраты).HasColumnName("Код_затраты");
        });

        modelBuilder.Entity<КонтактныеДанные>(entity =>
        {
            entity.HasKey(e => e.КодКонтактныхДанных).HasName("Контактные_данные_pkey");

            entity.ToTable("Контактные_данные");

            entity.Property(e => e.КодКонтактныхДанных).HasColumnName("Код_контактных_данных");
            entity.Property(e => e.Логин).HasMaxLength(100);
            entity.Property(e => e.Пароль).HasMaxLength(100);
            entity.Property(e => e.Почта).HasMaxLength(100);
            entity.Property(e => e.Телефон).HasMaxLength(11);
        });

        modelBuilder.Entity<Проект>(entity =>
        {
            entity.HasKey(e => e.КодПроекта).HasName("Проект_pkey");

            entity.ToTable("Проект");

            entity.Property(e => e.КодПроекта).HasColumnName("Код_проекта");
            entity.Property(e => e.ГрафикСъёмок).HasColumnName("График_съёмок");
            entity.Property(e => e.ДатаНачалаСъёмок).HasColumnName("Дата_начала_съёмок");
            entity.Property(e => e.ДатаОкончанияСъёмок).HasColumnName("Дата_окончания_съёмок");
            entity.Property(e => e.Название).HasMaxLength(100);
            entity.Property(e => e.ЦелеваяАудитория).HasColumnName("Целевая_аудитория");

            entity.HasOne(d => d.ГрафикСъёмокNavigation).WithMany(p => p.Проектs)
                .HasForeignKey(d => d.ГрафикСъёмок)
                .HasConstraintName("Проект_График_fkey");

            entity.HasOne(d => d.ЖанрNavigation).WithMany(p => p.Проектs)
                .HasForeignKey(d => d.Жанр)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Проект_Жанр_fkey");

            entity.HasOne(d => d.ЗатратыNavigation).WithMany(p => p.Проектs)
                .HasForeignKey(d => d.Затраты)
                .HasConstraintName("Проект_Затраты_fkey");

            entity.HasOne(d => d.ПлощадкаNavigation).WithMany(p => p.Проектs)
                .HasForeignKey(d => d.Площадка)
                .HasConstraintName("Проект_Площадка_fkey");

            entity.HasOne(d => d.СтатусNavigation).WithMany(p => p.Проектs)
                .HasForeignKey(d => d.Статус)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Проект_Статус_fkey");

            entity.HasOne(d => d.ЦелеваяАудиторияNavigation).WithMany(p => p.Проектs)
                .HasForeignKey(d => d.ЦелеваяАудитория)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Проект_Целевая_аудитория_fkey");
        });

        modelBuilder.Entity<Прокат>(entity =>
        {
            entity.HasKey(e => e.КодПроката).HasName("Прокат_pkey");

            entity.ToTable("Прокат");

            entity.Property(e => e.КодПроката).HasColumnName("Код_проката");
            entity.Property(e => e.ДатаВыхода).HasColumnName("Дата_выхода");
            entity.Property(e => e.КоличествоКинотеатров).HasColumnName("Количество_кинотеатров");
            entity.Property(e => e.РейтингЗрителей).HasColumnName("Рейтинг_зрителей");

            entity.HasOne(d => d.ПроектNavigation).WithMany(p => p.Прокатs)
                .HasForeignKey(d => d.Проект)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Прокат_Проект_fkey");
        });

        modelBuilder.Entity<СоставСъёмочнойГруппы>(entity =>
        {
            entity.HasKey(e => e.КодСоставаСъёмочнойГруппы).HasName("Состав_съёмочной_группы_pkey");

            entity.ToTable("Состав_съёмочной_группы");

            entity.Property(e => e.КодСоставаСъёмочнойГруппы).HasColumnName("Код_состава_съёмочной_группы");
            entity.Property(e => e.Роль).HasMaxLength(100);
            entity.Property(e => e.СёмочнаяГруппа).HasColumnName("Сёмочная_группа");

            entity.HasOne(d => d.СотрудникNavigation).WithMany(p => p.СоставСъёмочнойГруппыs)
                .HasForeignKey(d => d.Сотрудник)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Состав_съёмочной_гру_Сотрудник_fkey");

            entity.HasOne(d => d.СёмочнаяГруппаNavigation).WithMany(p => p.СоставСъёмочнойГруппыs)
                .HasForeignKey(d => d.СёмочнаяГруппа)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Состав_съёмочно_Сёмочная_групп_fkey");
        });

        modelBuilder.Entity<Сотрудник>(entity =>
        {
            entity.HasKey(e => e.ТабельныйНомер).HasName("Сотрудник_pkey");

            entity.ToTable("Сотрудник");

            entity.Property(e => e.ТабельныйНомер).HasColumnName("Табельный_номер");
            entity.Property(e => e.Имя).HasMaxLength(100);
            entity.Property(e => e.КонтактныеДанные).HasColumnName("Контактные_данные");
            entity.Property(e => e.Фамилия).HasMaxLength(100);
            entity.Property(e => e.Фото).HasMaxLength(255);

            entity.HasOne(d => d.КонтактныеДанныеNavigation).WithMany(p => p.Сотрудникs)
                .HasForeignKey(d => d.КонтактныеДанные)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Сотрудник_Контактные_данные_fkey");

            entity.HasOne(d => d.СпециализацияNavigation).WithMany(p => p.Сотрудникs)
                .HasForeignKey(d => d.Специализация)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Сотрудник_Специализация_fkey");
        });

        modelBuilder.Entity<Специализация>(entity =>
        {
            entity.HasKey(e => e.КодСпециализации).HasName("Специализация_pkey");

            entity.ToTable("Специализация");

            entity.Property(e => e.КодСпециализации).HasColumnName("Код_специализации");
            entity.Property(e => e.Наименование).HasMaxLength(100);
        });

        modelBuilder.Entity<Статус>(entity =>
        {
            entity.HasKey(e => e.КодСтатуса).HasName("Статус_pkey");

            entity.ToTable("Статус");

            entity.Property(e => e.КодСтатуса).HasColumnName("Код_статуса");
            entity.Property(e => e.Наименование).HasMaxLength(100);
        });

        modelBuilder.Entity<СъёмочнаяГруппа>(entity =>
        {
            entity.HasKey(e => e.КодСъёмочнойГруппы).HasName("Съёмочная_группа_pkey");

            entity.ToTable("Съёмочная_группа");

            entity.Property(e => e.КодСъёмочнойГруппы).HasColumnName("Код_съёмочной_группы");
            entity.Property(e => e.ПроектГруппы).HasColumnName("Проект_группы");

            entity.HasOne(d => d.ПроектГруппыNavigation).WithMany(p => p.СъёмочнаяГруппаs)
                .HasForeignKey(d => d.ПроектГруппы)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Съёмочная_группа_Проект_группы_fkey");
        });

        modelBuilder.Entity<СъёмочнаяПлощадка>(entity =>
        {
            entity.HasKey(e => e.КодСъёмочнойПлощадки).HasName("Съёмочная_площадка_pkey");

            entity.ToTable("Съёмочная_площадка");

            entity.Property(e => e.КодСъёмочнойПлощадки).HasColumnName("Код_съёмочной_площадки");
            entity.Property(e => e.Адрес).HasMaxLength(100);
            entity.Property(e => e.СтоимостьАрендыВДень).HasColumnName("Стоимость_аренды_в_день");
            entity.Property(e => e.ТипПлощадки).HasColumnName("Тип_площадки");

            entity.HasOne(d => d.ТипПлощадкиNavigation).WithMany(p => p.СъёмочнаяПлощадкаs)
                .HasForeignKey(d => d.ТипПлощадки)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Съёмочная_площадк_Тип_площадки_fkey");
        });

        modelBuilder.Entity<СъёмочнаяПлощадкаПроекта>(entity =>
        {
            entity.HasKey(e => e.КодСъёмочнойПлощадкиПроекта).HasName("Съёмочная_площадка_проекта_pkey");

            entity.ToTable("Съёмочная_площадка_проекта");

            entity.Property(e => e.КодСъёмочнойПлощадкиПроекта).HasColumnName("Код_съёмочной_площадки_проекта");
            entity.Property(e => e.СъёмочнаяПлощадка).HasColumnName("Съёмочная_площадка");

            entity.HasOne(d => d.ПроектNavigation).WithMany(p => p.СъёмочнаяПлощадкаПроектаs)
                .HasForeignKey(d => d.Проект)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Съёмочная_площадка_прое_Проект_fkey");

            entity.HasOne(d => d.СъёмочнаяПлощадкаNavigation).WithMany(p => p.СъёмочнаяПлощадкаПроектаs)
                .HasForeignKey(d => d.СъёмочнаяПлощадка)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Съёмочная_площа_Съёмочная_площ_fkey");
        });

        modelBuilder.Entity<ТипПлощадки>(entity =>
        {
            entity.HasKey(e => e.КодТипаПлощаки).HasName("Тип_площадки_pkey");

            entity.ToTable("Тип_площадки");

            entity.Property(e => e.КодТипаПлощаки).HasColumnName("Код_типа_площаки");
            entity.Property(e => e.Наименование).HasColumnType("character varying");
        });

        modelBuilder.Entity<ЦелеваяАудитория>(entity =>
        {
            entity.HasKey(e => e.КодЦелевойАудитории).HasName("Целевая_аудитория_pkey");

            entity.ToTable("Целевая_аудитория");

            entity.Property(e => e.КодЦелевойАудитории).HasColumnName("Код_целевой_аудитории");
            entity.Property(e => e.Наименование).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
