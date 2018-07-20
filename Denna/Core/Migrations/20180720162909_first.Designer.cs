using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Core.Data;
using Core.Domain;

namespace Core.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180720162909_first")]
    partial class first
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.6");

            modelBuilder.Entity("Core.Domain.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Detail");

                    b.Property<DateTime>("EndTime");

                    b.Property<int>("Imprtance");

                    b.Property<int>("Notify");

                    b.Property<DateTime>("StartTime");

                    b.Property<int>("Status");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.ToTable("Tasks");
                });
        }
    }
}
