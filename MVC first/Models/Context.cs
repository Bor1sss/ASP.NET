using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MVC_first
{
    public class Context : DbContext
    {
        public DbSet<Movie> Films { get; set; }
        public Context(DbContextOptions<Context> options): base(options)
        {
            if (Database.EnsureCreated())
            {




                Films!.Add(new Movie
                {
                    Title = "Джон Уик 4",
                    Director = "Чад Стахелски",
                    Genre = "Боевик",
                    Date = new DateTime(2023, 3, 24),
                    PosterPath = "/Images/1.jpg",
                    Description = "Джон Уик находит способ одержать победу над Правлением Кланов. Однако, прежде чем он сможет заслужить свою свободу, ему предстоит сразиться с новым врагом и его могущественными союзниками."
                });

                Films!.Add(new Movie
                {
                    Title = "Шазам 2",
                    Director = "Дэвид Ф. Сандберг",
                    Genre = "Боевик",
                    Date = new DateTime(2023, 12, 15),
                    PosterPath = "/Images/2.jpg",
                    Description = "Шазам должен объединить свою семью и отразить новую угрозу из прошлого."
                });

                Films!.Add(new Movie
                {
                    Title = "Восход",
                    Director = "Джордан Вогт-Робертс",
                    Genre = "Боевик",
                    Date = new DateTime(2023, 5, 17),
                    PosterPath = "/Images/3.jpg",
                    Description = "После катастрофической авиакатастрофы молодой человек просыпается на необитаемом острове, где ему приходится учиться выживать, используя силу и свой ум."
                });

                Films!.Add(new Movie
                {
                    Title = "История игрушек 5",
                    Director = "Джош Кун",
                    Genre = "Боевик",
                    Date = new DateTime(2023, 7, 14),
                    PosterPath = "/Images/4.jpg",
                    Description = "Новые приключения любимых игрушек, которые вновь отправляются в удивительное путешествие."
                });

                SaveChanges();


            }
        }
    }
}
