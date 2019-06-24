using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;

namespace NETFinalProj.Dao
{
    public class GetConnectFilm
    {
        private const string strConn = "mongodb://118.25.126.67:27017";
        private const string databaseName = "Movies";
        private const string collectionName = "films_all";
        private static IMongoClient client;
        private static IMongoDatabase database;
        public static IMongoCollection<BsonDocument> collection;
        static GetConnectFilm()
        {
            client = new MongoClient(strConn);
            database = client.GetDatabase(databaseName);
            collection = database.GetCollection<BsonDocument>(collectionName);
        }
    }
}