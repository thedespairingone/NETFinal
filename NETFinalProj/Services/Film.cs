using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NETFinalProj.Dao;
using MongoDB.Driver;
using MongoDB.Bson;
using GetJSON;
using Newtonsoft.Json;
using GetConnectFilm;


namespace NETFinalProj.Services
{
    public class Film
    {
        public static String getSortedMovieListByRate()
        {
            var filter = Builders<BsonDocument>.Filter.Exists("rating");
            var sort = Builders<BsonDocument>.Sort.Descending("rating.average");
            var projection = Builders<BsonDocument>.Projection.Exclude("_id").Exclude("lastModified");
            var documents = GetConnectFilm.GetConnectFilm.collection.Find(filter).Sort(sort).Project(projection).Limit(12).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie =GetJSON.GetJSON.getJsonString(movies);
            return someMovie;
        }
        public static String getSortedMovieListByRate(String country)
        {

            var filter = Builders<BsonDocument>.Filter.Eq("countries", country);
            var sort = Builders<BsonDocument>.Sort.Descending("rating.average");
            var projection = Builders<BsonDocument>.Projection.Exclude("_id").Exclude("lastModified");
            var documents = GetConnectFilm.GetConnectFilm.collection.Find(filter).Sort(sort).Project(projection).Limit(12).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.GetJSON.getJsonString(movies);
            return someMovie;
        }
        public static String getSortedMovieListByHot()
        {
            var filter = Builders<BsonDocument>.Filter.Exists("rating");
            var sort = Builders<BsonDocument>.Sort.Descending("rating.rating_people");
            var projection = Builders<BsonDocument>.Projection.Exclude("_id").Exclude("lastModified");
            var documents = GetConnectFilm.GetConnectFilm.collection.Find(filter).Sort(sort).Project(projection).Limit(12).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.GetJSON.getJsonString(movies);
            return someMovie;
        }
        public static String getSortedMovieListByHot(String country)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("countries", country);
            var sort = Builders<BsonDocument>.Sort.Descending("rating.rating_people");
            var projection = Builders<BsonDocument>.Projection.Exclude("_id").Exclude("lastModified");
            var documents = GetConnectFilm.GetConnectFilm.collection.Find(filter).Sort(sort).Project(projection).Limit(12).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.GetJSON.getJsonString(movies);
            return someMovie;
        }
        public static String getMovies(int skip)
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var projection = Builders<BsonDocument>.Projection.Exclude("_id").Exclude("lastModified");
            var documents = GetConnectFilm.GetConnectFilm.collection.Find(filter).Skip(skip).Project(projection).Limit(20).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.GetJSON.getJsonString(movies);
            return someMovie;
        }

        public static String getMoviesDetail(int skip)
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var projection = Builders<BsonDocument>.Projection.Exclude("_id").Exclude("lastModified");
            var documents = GetConnectFilm.GetConnectFilm.collection.Find(filter).Skip(skip).Project(projection).Limit(1).ToList();

            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.GetJSON.getJsonString(movies);
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
            var documents = GetConnectFilm.GetConnectFilm.collection.Find(filter).Project(projection).Limit(24).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.GetJSON.getJsonString(movies);
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
            var documents = GetConnectFilm.GetConnectFilm.collection.Find(filter).Project(projection).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.GetJSON.getJsonString(movies);
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
            var projection = Builders<BsonDocument>.Projection.Exclude("_id").Exclude("lastModified");
            var documents = GetConnectFilm.GetConnectFilm.collection.Find(filter).Sort(sort).Skip(skip).Project(projection).Limit(120).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.GetJSON.getJsonString(movies);
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
            var projection = Builders<BsonDocument>.Projection.Exclude("_id").Exclude("lastModified");
            var documents = GetConnectFilm.GetConnectFilm.collection.Find(filter).Sort(sort).Skip(skip).Project(projection).Limit(120).ToList();
            List<string> movies = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                movies.Add(documents[i].ToJson());
            }
            String someMovie = GetJSON.GetJSON.getJsonString(movies);
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
            GetConnectFilm.GetConnectFilm.collection.InsertOne(document);
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
            var result = GetConnectFilm.GetConnectFilm.collection.UpdateOne(filter, update);
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
            var result = GetConnectFilm.GetConnectFilm.collection.DeleteMany(filter);
            return "DeleteOK";
        }


        //提交评分
        public static String SubmitRate( String title,String Rate)
        {
            //GetConnectFilm.collection.InsertOne(document);
            var filter = Builders<BsonDocument>.Filter.Eq("title", title);
            var projection = Builders<BsonDocument>.Projection.Exclude("_id")
            .Include("rating.rating_people").Include("rating.average");
            var documents = GetConnectFilm.GetConnectFilm.collection.Find(filter).Project(projection).ToList();
            String thisMovie = documents[0].ToJson();
            Newtonsoft.Json.Linq.JObject jo = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(thisMovie);
            //String rate = jo["rating.rating_people"].ToString();
            //String peopleNum = jo["rating.average"].ToString();
            String rateTemp = jo["rating"]["average"].ToString();
            String peopleNumTemp = jo["rating"]["rating_people"].ToString();
            float rate = Convert.ToSingle(rateTemp);
            float myRate = Convert.ToSingle(Rate);
            float peopleNum = Convert.ToSingle(peopleNumTemp);
            peopleNum = peopleNum + 1;
            // jo = (JObject)JsonConvert.DeserializeObject(thisMovie);
            //string zone = jo["rating.rating_people"].ToString();
            //string zone_en = jo["rating.average"].ToString();

            //List<string> movies = new List<string>();
            //for (int i = 0; i < documents.Count; i++)
            //{
            //    movies.Add(documents[i].ToJson());
            //}
            //String someMovie = GetJSON.getJsonString(movies);
            //return someMovie;

            //GetAverageLib.ForNet classForAver = new GetAverageLib.ForNet();
            //float newAverageTemp= (float)Math.Round((double)classForAver.CalculateAverage(peopleNum, rate, myRate), 2); 
            //String newAverage = newAverageTemp.ToString();

            String newAverage = Film.CalculateAverage(peopleNum, rate, myRate).ToString();
            String newPeopleNum = Convert.ToInt32(peopleNum).ToString();

            //将计算好的新平均分和人数更新数据库
            var filter2 = Builders<BsonDocument>.Filter.Eq("title", title);
            var update = Builders<BsonDocument>.Update.Set("rating.rating_people", newPeopleNum).Set("rating.average", newAverage).CurrentDate("lastModified");
            GetConnectFilm.GetConnectFilm.collection.UpdateMany(filter, update);
            return "RateOK";
        }

        //计算新的平均分
        public static float CalculateAverage(float peopleNum,float rate,float myRate)
        {
            float newAverage = (rate * (peopleNum - 1) + myRate) / peopleNum;
            newAverage = (float)Math.Round((double)newAverage, 2);
            return newAverage;
        }


    }
}