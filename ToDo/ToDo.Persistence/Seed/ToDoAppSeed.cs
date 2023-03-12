using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application;
using ToDo.Domain.Subtasks;
using ToDo.Domain.ToDos;
using ToDo.Domain.Users;
using ToDo.Persistence.Context;

namespace ToDoApp.Persistence.Seed
{
    public static class ToDoAppSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var database = scope.ServiceProvider.GetRequiredService<ToDoContext>();

            Migrate(database);
            SeedEverything(database);
        }

        private static void Migrate(ToDoContext context)
        {
            context.Database.Migrate();
        }

        private static void SeedEverything(ToDoContext context)
        {
            var seeded = false;

            SeedUsers(context, ref seeded);
            SeedToDos(context, ref seeded);
            SeedSubtasks(context, ref seeded);

            if (seeded)
                context.SaveChanges();
        }

        private static void SeedToDos(ToDoContext context, ref bool seeded)
        {
            var toDos = new List<ToDoEntity>()
            {
                new ToDoEntity
                {
                    Status = EntityStatuses.Active,
                    TargetCompletionDate = DateTime.Now.AddDays(2),
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    OwnerId = context.Users.First(x=> x.Username == "TestUser1").Id,
                    ToDoStatus = ToDoStatuses.Active,
                    Title = "ToDo1 for User 1"
                },
                new ToDoEntity
                {
                    Status = EntityStatuses.Active,
                    TargetCompletionDate = DateTime.Now.AddDays(2),
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    OwnerId = context.Users.First(x=> x.Username == "TestUser1").Id,
                    ToDoStatus = ToDoStatuses.Active,
                    Title = "ToDo2 for User 1"
                },
                new ToDoEntity
                {
                    Status = EntityStatuses.Active,
                    TargetCompletionDate = DateTime.Now.AddDays(2),
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    OwnerId = context.Users.First(x=> x.Username == "TestUser1").Id,
                    ToDoStatus = ToDoStatuses.Done,
                    Title = "ToDo3 for User 1"
                },
                new ToDoEntity
                {
                    Status = EntityStatuses.Active,
                    TargetCompletionDate = DateTime.Now.AddDays(2),
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    OwnerId = context.Users.First(x=> x.Username == "TestUser2").Id,
                    ToDoStatus = ToDoStatuses.Active,
                    Title = "ToDo1 for User 2",
                    Subtasks = new List<Subtask>()
                }
            };

            foreach (var todo in toDos)
            {
                if (context.ToDos.Any(x => x.Title == todo.Title)) continue;

                context.ToDos.Add(todo);

                seeded = true;
            }
            context.SaveChanges();
        }

        private static void SeedSubtasks(ToDoContext context, ref bool seeded)
        {
            var subtasks = new List<Subtask>()
            {
                new Subtask
                {
                   ToDoItemId = context.ToDos.SingleOrDefault(x=>x.Title == "ToDo1 for User 1").Id,
                   CreatedAt = DateTime.Now,
                   ModifiedAt = DateTime.Now,
                   Status = EntityStatuses.Active,
                   Title = "Subtask1 For ToDo1"
                },
                new Subtask
                {
                   ToDoItemId = context.ToDos.SingleOrDefault(x=>x.Title == "ToDo1 for User 1").Id,
                   CreatedAt = DateTime.Now,
                   ModifiedAt = DateTime.Now,
                   Status = EntityStatuses.Active,
                   Title = "Subtask2 For ToDo1"
                },
                new Subtask
                {
                   ToDoItemId = context.ToDos.SingleOrDefault(x=>x.Title == "ToDo2 for User 1").Id,
                   CreatedAt = DateTime.Now,
                   ModifiedAt = DateTime.Now,
                   Status = EntityStatuses.Active,
                   Title = "Subtask withId 3 For ToDo2"
                },
                new Subtask
                {
                    ToDoItemId = context.ToDos.SingleOrDefault(x=>x.Title == "ToDo3 for User 1").Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = EntityStatuses.Active,
                    Title = "Subtask with Id 5 for  For ToDo3"
                }
            };
            foreach (var subtask in subtasks)
            {
                if (context.Subtasks.Any(x => x.Title == subtask.Title)) continue;

                context.Subtasks.Add(subtask);

                seeded = true;
            }
            context.SaveChanges();
        }

        private static string GenerateHash(string input)
        {
            const string SECRET_KEY = "lfherffg324";
            using (SHA512 sha = SHA512.Create())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input + SECRET_KEY);
                byte[] hashBytes = sha.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }

        private static void SeedUsers(ToDoContext context, ref bool seeded)
        {
            var users = new List<User>()
            {
                new User
                {
                    Username = "TestUser1",
                    Password = GenerateHash("TestPassword1"),
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = EntityStatuses.Active
                },
                new User
                {
                    Username = "TestUser2",
                    Password = GenerateHash("TestPassword2"),
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Status = EntityStatuses.Active
                }

            };

            foreach (var user in users)
            {
                if (context.Users.Any(x => x.Username == user.Username)) continue;

                context.Users.Add(user);

                seeded = true;
            }
            context.SaveChanges();
        }

    }
}
