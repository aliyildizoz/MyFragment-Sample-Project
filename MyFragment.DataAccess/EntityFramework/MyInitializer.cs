using MyFragment.Entities.Entity;
using MyFragment.Entities.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.DataAccess.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            for (int k = 0; k < 100; k++)
            {
                context.Categories.Add(new Category() { CategoryState = CategoryState.ImdbPoint, Value = ((decimal)k / 10).ToString() });
            }
            for (int k = 1990; k < 2021; k++)
            {
                context.Categories.Add(new Category() { CategoryState = CategoryState.MovieYear, Value = k.ToString() });
            }
            context.Categories.Add(new Category() { CategoryState = CategoryState.MovieType, Value = "Aksiyon" });
            context.Categories.Add(new Category() { CategoryState = CategoryState.MovieType, Value = "Macera" });
            context.Categories.Add(new Category() { CategoryState = CategoryState.MovieType, Value = "Animasyon" });
            context.Categories.Add(new Category() { CategoryState = CategoryState.MovieType, Value = "Komedi" });
            context.Categories.Add(new Category() { CategoryState = CategoryState.MovieType, Value = "Suç" });
            context.Categories.Add(new Category() { CategoryState = CategoryState.MovieType, Value = "Polisye" });
            context.Categories.Add(new Category() { CategoryState = CategoryState.MovieType, Value = "Drama" });
            context.Categories.Add(new Category() { CategoryState = CategoryState.MovieType, Value = "Fantastik" });
            context.Categories.Add(new Category() { CategoryState = CategoryState.MovieType, Value = "Korku" });
            context.Categories.Add(new Category() { CategoryState = CategoryState.MovieType, Value = "Bilim Kurgu" });
            context.Categories.Add(new Category() { CategoryState = CategoryState.MovieType, Value = "Gerilim" });
            context.Categories.Add(new Category() { CategoryState = CategoryState.MovieType, Value = "Bilim Kurgu" });
            context.Categories.Add(new Category() { CategoryState = CategoryState.MovieType, Value = "Western" });
            context.Categories.Add(new Category() { CategoryState = CategoryState.MovieType, Value = "Savaş" });

            User user = new User() { ImagePath = "defaultPhoto.png", Email = "aliyildizoz909@gmail.com", Name = "Ali", Password = "123", Surname = "Yıldızöz", Username = "aliylzz", UserState = UserState.Admin };
            context.Users.Add(user);

            #region To create a sample database if the database does not exist
            //for (int k = 0; k < 10; k++)
            //{
            //    context.Categories.Add(new Category() { CategoryState = CategoryState.MovieType, Value = FakeData.PlaceData.GetCountry() });
            //}
            //for (int i = 0; i < 20; i++)
            //{
            //    Movie movie = new Movie()
            //    {
            //        EmbedKey = "k7IG_ICMrhk",
            //        ImagePath = "system.jpg",
            //        ImdbPoint = ((decimal)FakeData.NumberData.GetNumber(45, 85) / 10).ToString(),
            //        Lenght = new TimeSpan(FakeData.NumberData.GetNumber(1, 3), FakeData.NumberData.GetNumber(1, 60), FakeData.NumberData.GetNumber(1, 60)),
            //        Name = FakeData.PlaceData.GetCity(),
            //        MovieYear = FakeData.DateTimeData.GetDatetime(new DateTime(2020, 01, 01), new DateTime(1990, 01, 01)).Year,
            //        AddedDate = DateTime.Now,
            //        Summary = FakeData.TextData.GetSentences(4)
            //    };
            //    context.Movies.Add(movie);

            //    movie.Categories.Add(context.Categories.Where(I => I.CategoryState == CategoryState.MovieYear).FirstOrDefault(I => I.Value == movie.MovieYear.ToString()));
            //    movie.Categories.Add(context.Categories.Where(I => I.CategoryState == CategoryState.MovieType).ToList()[FakeData.NumberData.GetNumber(0, 10)]);
            //    movie.Categories.Add(context.Categories.Where(I => I.CategoryState == CategoryState.ImdbPoint).FirstOrDefault(I => I.Value == movie.ImdbPoint.ToString()));

            //    for (int k = 0; k < FakeData.NumberData.GetNumber(1, 3); k++)
            //    {
            //        Director director = new Director()
            //        {
            //            ImagePath = "defaultPhoto.png",
            //            Name = FakeData.NameData.GetFirstName(),
            //            Surname = FakeData.NameData.GetSurname(),
            //        };
            //        context.Directors.Add(director);
            //        movie.Directors.Add(director);
            //    }

            //    for (int k = 0; k < FakeData.NumberData.GetNumber(4, 8); k++)
            //    {
            //        Actor actor = new Actor()
            //        {
            //            ImagePath = "defaultPhoto.png",
            //            Name = FakeData.NameData.GetFirstName(),
            //            Surname = FakeData.NameData.GetSurname(),
            //        };
            //        context.Actors.Add(actor);
            //        movie.Actors.Add(actor);
            //    }
            //}
            #endregion

            context.SaveChanges();
        }
    }
}
