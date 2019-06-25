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
            var documents=GetConnectFilm.collection.Find(filter).Sort(sort).Limit(12).ToList();
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
            filter = builder.Or(builder.Regex("title", input),builder.Regex("casts.name",input)
            , builder.Regex("directors.name", input), builder.Regex("genres", input), builder.Regex("summary", input));
            var documents = GetConnectFilm.collection.Find(filter).Limit(24).ToList();
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
            //var document = collection.Find(filter).FirstAsync().Result;

            //Console.WriteLine(input + "Dao");
            //var builder = Builders<BsonDocument>.Filter;
            //FilterDefinition<BsonDocument> filter;
            //filter = builder.And(builder.All("_id", input));
            var documents = GetConnectFilm.collection.Find(filter).ToList();
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
    }
}