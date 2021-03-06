namespace cleangap.api.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using cleangap.api.Models.Tables;

    public partial class CleanGapDataContext : DbContext
    {
        public CleanGapDataContext()
            : base("cleangap")
        {
        }

        public virtual DbSet<answers> answers { get; set; }
        public virtual DbSet<customers> customers { get; set; }
        public virtual DbSet<project_follow_up> project_follow_up { get; set; }
        public virtual DbSet<question_options> question_options { get; set; }
        public virtual DbSet<question_sections> question_sections { get; set; }
        public virtual DbSet<questions> questions { get; set; }
        public virtual DbSet<staff> staff { get; set; }
        public virtual DbSet<projects> projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<answers>()
                .Property(e => e.answers_value)
                .IsUnicode(false);

            modelBuilder.Entity<customers>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<customers>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<customers>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<customers>()
                .Property(e => e.token_forgot_pass)
                .IsUnicode(false);

            modelBuilder.Entity<customers>()
                .HasMany(e => e.answers)
                .WithOptional(e => e.customers)
                .HasForeignKey(e => e.id_customer);

            modelBuilder.Entity<customers>()
                .HasMany(e => e.projects)
                .WithOptional(e => e.customers)
                .HasForeignKey(e => e.id_customer);

            modelBuilder.Entity<project_follow_up>()
                .Property(e => e.staff_comments)
                .IsUnicode(false);

            modelBuilder.Entity<project_follow_up>()
                .Property(e => e.customer_comments)
                .IsUnicode(false);

            modelBuilder.Entity<project_follow_up>()
                .Property(e => e.url_attachment)
                .IsUnicode(false);

            modelBuilder.Entity<question_options>()
                .Property(e => e.option_text)
                .IsUnicode(false);

            modelBuilder.Entity<question_options>()
                .Property(e => e.input_type)
                .IsFixedLength();

            modelBuilder.Entity<question_options>()
                .HasMany(e => e.answers)
                .WithOptional(e => e.question_options)
                .HasForeignKey(e => e.id_question_option);

            modelBuilder.Entity<question_sections>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<question_sections>()
                .HasMany(e => e.subsection_questions)
                .WithOptional(e => e.questions_subsection)
                .HasForeignKey(e => e.id_subsection);

            modelBuilder.Entity<question_sections>()
                .HasMany(e => e.questions)
                .WithOptional(e => e.question_sections)
                .HasForeignKey(e => e.id_section);            

            modelBuilder.Entity<questions>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<questions>()
                .HasMany(e => e.question_options)
                .WithOptional(e => e.questions)
                .HasForeignKey(e => e.id_question);

            modelBuilder.Entity<questions>()
                .HasMany(e => e.children_question)
                .WithOptional(e => e.parent_question)
                .HasForeignKey(e => e.parent_question_id);

            modelBuilder.Entity<staff>()
                .Property(e => e.fullname)
                .IsUnicode(false);

            modelBuilder.Entity<staff>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<staff>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<staff>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<staff>()
                .HasMany(e => e.project_follow_up)
                .WithOptional(e => e.staff)
                .HasForeignKey(e => e.id_staff);

            modelBuilder.Entity<projects>()
                .HasMany(e => e.project_follow_up)
                .WithOptional(e => e.project)
                .HasForeignKey(e => e.id_project);

            modelBuilder.Entity<projects>()
                .HasMany(e => e.answers)
                .WithOptional(e => e.projects)
                .HasForeignKey(e => e.id_project);
        }
    }
}
