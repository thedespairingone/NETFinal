using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NETFinalProj.Dao;
using MongoDB.Driver;
using MongoDB.Bson;

namespace NETFinalProj.Services
{
    public class Film
    {
        public static String getSortedMovieListByRate()
        {
            var filter = Builders<BsonDocument>.Filter.Exists("rating");
            var sort = Builders<BsonDocument>.Sort.Descending("rating.average");
            var documents = GetConnectFilm.collection.Find(filter).Sort(sort).Limit(12).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.getJsonString(movies);
            return someMovie;
        }
        public static String getSortedMovieListByRate(String country)
        {

            var filter = Builders<BsonDocument>.Filter.Eq("countries", country);
            var sort = Builders<BsonDocument>.Sort.Descending("rating.average");
            var documents = GetConnectFilm.collection.Find(filter).Sort(sort).Limit(12).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.getJsonString(movies);
            return someMovie;
        }
        public static String getSortedMovieListByHot()
        {
            var filter = Builders<BsonDocument>.Filter.Exists("rating");
            var sort = Builders<BsonDocument>.Sort.Descending("rating.rating_people");
            var documents = GetConnectFilm.collection.Find(filter).Sort(sort).Limit(12).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.getJsonString(movies);
            return someMovie;
        }
        public static String getSortedMovieListByHot(String country)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("countries", country);
            var sort = Builders<BsonDocument>.Sort.Descending("rating.rating_people");
            var documents = GetConnectFilm.collection.Find(filter).Sort(sort).Limit(12).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.getJsonString(movies);
            return someMovie;
        }
        public static String getMovies(int skip)
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var documents = GetConnectFilm.collection.Find(filter).Skip(skip).Limit(20).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.getJsonString(movies);
            return someMovie;
        }

        public static String getMoviesDetail(int skip)
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var documents = GetConnectFilm.collection.Find(filter).Skip(skip).Limit(1).ToList();

            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.getJsonString(movies);
            return someMovie;
        }

        public static String SearchAll(String input)
        {
            //var filter = Builders<BsonDocument>.Filter.Gt("title", "/金刚狼/");
            //var document = collection.Find(filter).FirstAsync().Result;

            //Console.WriteLine(input + "Dao");
            var builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter;
            //filter = builder.And(builder.All("_id", input));
            var projection = Builders<BsonDocument>.Projection.Exclude("_id").Exclude("lastModified");
            filter = builder.Or(builder.Regex("title", input), builder.Regex("casts.name", input)
            , builder.Regex("directors.name", input), builder.Regex("genres", input), builder.Regex("summary", input));
            var documents = GetConnectFilm.collection.Find(filter).Project(projection).Limit(24).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.getJsonString(movies);
            return someMovie;
        }

        public static String Search(String input)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("title", input);
            var projection = Builders<BsonDocument>.Projection.Exclude("_id").Exclude("lastModified");
            //var document = collection.Find(filter).FirstAsync().Result;

            //Console.WriteLine(input + "Dao");
            //var builder = Builders<BsonDocument>.Filter;
            //FilterDefinition<BsonDocument> filter;
            //filter = builder.And(builder.All("_id", input));
            var documents = GetConnectFilm.collection.Find(filter).Project(projection).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.getJsonString(movies);
            return someMovie;
        }

        //FilmAllPage
        public static String getAllFilmSortByHot(String country, String language, String genre, int skip)
        {
            var builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter;
            if (country == "全部" && language == "全部" && genre == "全部")
            {
                filter = Builders<BsonDocument>.Filter.Empty;
            }
            else if (country != "全部" && language == "全部" && genre == "全部")
            { filter = builder.And(builder.Eq("countries", country)); }
            else if (country == "全部" && language != "全部" && genre == "全部")
            { filter = builder.And(builder.Eq("languages", language)); }
            else if (country == "全部" && language == "全部" && genre != "全部")
            { filter = builder.And(builder.Eq("genres", genre)); }
            else if (country != "全部" && language != "全部" && genre == "全部")
            { filter = builder.And(builder.Eq("countries", country), builder.Eq("languages", language)); }
            else if (country == "全部" && language != "全部" && genre != "全部")
            { filter = builder.And(builder.Eq("languages", language), builder.Eq("genres", genre)); }
            else if (country != "全部" && language == "全部" && genre != "全部")
            { filter = builder.And(builder.Eq("countries", country), builder.Eq("genres", genre)); }
            else
            { filter = builder.And(builder.Eq("countries", country), builder.Eq("languages", language), builder.Eq("genres", genre)); }

            //var filter = Builders<BsonDocument>.Filter.Eq("countries", country);
            var sort = Builders<BsonDocument>.Sort.Descending("rating.average");
            var documents = GetConnectFilm.collection.Find(filter).Sort(sort).Skip(skip).Limit(120).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.getJsonString(movies);
            return someMovie;
        }
        //FilmAllPage
        public static String getAllFilmSortByRate(String country, String language, String genre, int skip)
        {
            var builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter;
            if (country == "全部" && language == "全部" && genre == "全部")
            { filter = Builders<BsonDocument>.Filter.Empty; }
            else if (country != "全部" && language == "全部" && genre == "全部")
            { filter = builder.And(builder.Eq("countries", country)); }
            else if (country == "全部" && language != "全部" && genre == "全部")
            { filter = builder.And(builder.Eq("languages", language)); }
            else if (country == "全部" && language == "全部" && genre != "全部")
            { filter = builder.And(builder.Eq("genres", genre)); }
            else if (country != "全部" && language != "全部" && genre == "全部")
            { filter = builder.And(builder.Eq("countries", country), builder.Eq("languages", language)); }
            else if (country == "全部" && language != "全部" && genre != "全部")
            { filter = builder.And(builder.Eq("languages", language), builder.Eq("genres", genre)); }
            else if (country != "全部" && language == "全部" && genre != "全部")
            { filter = builder.And(builder.Eq("countries", country), builder.Eq("genres", genre)); }
            else
            { filter = builder.And(builder.Eq("countries", country), builder.Eq("languages", language), builder.Eq("genres", genre)); }
            //filter = builder.And(builder.Eq("countries", country), builder.Eq("languages", language)
            //, builder.Eq("genres", genre));
            //var filter = Builders<BsonDocument>.Filter.Eq("countries", country);
            var sort = Builders<BsonDocument>.Sort.Descending("rating.rating_people");
            var documents = GetConnectFilm.collection.Find(filter).Sort(sort).Skip(skip).Limit(120).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.getJsonString(movies);
            return someMovie;
        }

        //InsertAFilm
        public static String InsertFilm(String title, String poster, String directors, String writers, String casts, String genres, String countries,
        String languages, String pubdate, String length, String summary)
        {
            var document = new BsonDocument
            {
                {"rating", new BsonDocument { { "rating_people", "0" }, { "average", "0"}, { "stars", new BsonArray {"5"} } }},
                {"genres", new BsonArray {genres }},
                {"pubdate", new BsonArray { pubdate }},
                {"title",title },
                { "poster", poster },
                {"countries", new BsonArray { countries }},
                {"summary", summary},
                {"languages", new BsonArray { languages }},
                {"duration",length},
                {"directors", new BsonArray { new BsonDocument{ { "name", directors } } } },
                {"casts", new BsonArray { new BsonDocument{ { "name", casts } } } },
                {"writers",new BsonArray { writers }}
            };
            GetConnectFilm.collection.InsertOne(document);
            return title;
        }

        //UpdateFilm
        //InsertAFilm
        public static String UpdateFilm(String title, String poster, String directors, String writers, String casts, String genres, String countries,
        String languages, String pubdate, String length, String summary)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("title", title);
            var update = Builders<BsonDocument>.Update.Set("genres", new BsonArray { genres }).Set("pubdate", new BsonArray { pubdate })
            .Set("title", title).Set("poster", poster).Set("writers", new BsonArray { writers })
            .Set("countries", new BsonArray { countries }).Set("summary", summary)
            .Set("languages", new BsonArray { languages }).Set("duration", length)
            .Set("directors", new BsonArray { new BsonDocument { { "name", directors } } }).Set("casts", new BsonArray { new BsonDocument { { "name", casts } } })
            .CurrentDate("lastModified");
            var result = GetConnectFilm.collection.UpdateOne(filter, update);
            //var document = new BsonDocument
            //{
            //    {"rating", new BsonDocument { { "rating_people", "0" }, { "average", "0"}, { "stars", new BsonArray {5} } }},
            //    {"genres", new BsonArray {genres }},
            //    {"pubdate", new BsonArray { pubdate }},
            //    {"title",title },
            //    { "poster", poster },
            //    {"countries", new BsonArray { countries }},
            //    {"summary", summary},
            //    {"languages", new BsonArray { languages }},
            //    {"duration",length},
            //    {"directors", new BsonArray { new BsonDocument{ { "name", directors } } } },
            //    {"casts", new BsonArray { new BsonDocument{ { "name", casts } } } },
            //    {"writers",new BsonArray { writers }}
            //};
            //GetConnectFilm.collection.InsertOne(document);
            return title;
        }

        //DeleteFilm
        public static String DeleteFilm(String title)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("title", title);
            var result = GetConnectFilm.collection.DeleteMany(filter);
            return "DeleteOK";
        }
    }
}