using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace NETFinalProj.Dao
{
    public class GetConnectUser
    {
            private const string strConn = "mongodb://118.25.126.67:27017";
            private const string databaseName = "Movies";
            private const string collectionName = "users";
            private static IMongoClient client;
            private static IMongoDatabase database;
            public static IMongoCollection<BsonDocument> collection;
            static GetConnectUser()
            {
                client = new MongoClient(strConn);
                database = client.GetDatabase(databaseName);
                collection = database.GetCollection<BsonDocument>(collectionName);
            }
    }
}